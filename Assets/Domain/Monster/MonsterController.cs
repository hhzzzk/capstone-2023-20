using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class MonsterController : MonoBehaviour
{
    [SerializeField] string playerTag = "Taichi";
    public enum CurrentState { idle, trace, attack, patrol, dead };
    public CurrentState curState = CurrentState.idle;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;



    // ��� ���� + �߰��ʿ� �÷��̾� ���� ��
    private bool isDead = false;
    //
    private bool flagIdle = true;
    [SerializeField]
    private float IdleTIme = 5f;
    private float chkTime = 0f;
    [SerializeField]
    private float traceSpeed = 3f;
    [SerializeField]
    private float patrolSpeed = 2f;

    //���� �þ�
    public AiSensor Sensor;
    bool inSight = false;
    bool isLost = false;

    [SerializeField] Transform[] m_ptPoints = null; // ���� ��ġ���� ���� �迭

    //debug
    public GameObject alarm;

    private GameObject wayPoint;
    //

    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag(playerTag).GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();
        Sensor = GameObject.Find("MonsterSensor").GetComponent<AiSensor>();
        wayPoint = GameObject.Find("WayPoints");

        m_ptPoints = wayPoint.gameObject.GetComponentsInChildren<Transform>();

        // ���� ����� ��ġ�� �����ϸ� �ٷ� ���� ����
        // nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 5f); // ������ġ ����� �ִ�Ÿ�

    }
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.5f);

            //float dist = Vector3.Distance(playerTransform.position, _transform.position);
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < 0.5f)
            {
                PlayerDeath();
            }
            inSight = Sensor.isInSight;
            rayChkDoor(); // ������
            if (inSight)
            {
                isLost = true;
                Debug.Log("trace");
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.patrol;
            }

        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.trace: // ���� ����
                    nvAgent.speed = traceSpeed;
                    nvAgent.destination = playerTransform.position;
                    _animator.SetBool("isRun", true);
                    break;
                case CurrentState.patrol:
                    if (isLost) // �÷��̾� ã�Ҵµ� ��ģ ���
                    {
                        isLost = false;
                        nvAgent.ResetPath(); // ��� �ʱ�ȭ
                    }
                    nvAgent.speed = patrolSpeed; // ���� �ִ� �̵� �ӵ�
                    Patroling();
                    break;
            }

            yield return null;
        }
    }
    void Patroling()
    {
        if (nvAgent.remainingDistance < 1f && chkTime < IdleTIme && flagIdle) // �����ð����� idle
        {
            //Debug.Log("patrolIdle remainingDist > " + nvAgent.remainingDistance);
            //alarm.SetActive(true);
            //Debug.Log("patrol_idle");
            chkTime += Time.deltaTime;
            //nvAgent.ResetPath();
        }
        if (chkTime > IdleTIme) // idle ����, ���� �̵�
        {
            flagIdle = false;
            //alarm.SetActive(false);
            Debug.Log("chkTime : " + chkTime);
            chkTime = 0;

            if (nvAgent.remainingDistance < 1f)
            {
                _animator.SetBool("isWalk", true);

                //��������Ʈ ����
                //Vector3 RandomPos;
                //RandomPoint(out RandomPos);
                //Vector3 randomDir = (_transform.position - RandomPos).normalized;
                //float dirMagnitude = (_transform.position - RandomPos).magnitude;
                //Debug.DrawRay(_transform.position, randomDir * dirMagnitude, Color.red, 10.0f);
                // ������ġ���� ������ġ���� ���̸� �׸��� 10.0�� ���� ������
                //nvAgent.SetDestination(RandomPos);

                //��������Ʈ ������� ����
                int pt = Random.Range(0, m_ptPoints.Length);
                Debug.Log("pt >"+ pt);

                nvAgent.SetDestination(m_ptPoints[pt].position);
                //���⼭ ���߳�?
                if (!nvAgent.pathPending)
                {
                    Debug.Log("pp");
                    if (nvAgent.remainingDistance <= nvAgent.stoppingDistance)
                    {
                        Debug.Log("rm");
                        if (!nvAgent.hasPath || nvAgent.velocity.sqrMagnitude == 0f) // ����
                        {
                            curState = CurrentState.patrol;
                            _animator.SetBool("isIdle", true);
                            flagIdle = true;
                        }
                    }
                }
            }
        }
    }
    bool RandomPoint(out Vector3 randomPosResult)
    {
        randomPosResult = _transform.position; // ���ڸ�

        float range = 5f;
        Vector3 randomPoint = _transform.position + Random.insideUnitSphere * range; //random point in a sphere 
        float maxDist = 3.0f; // �Ž��� �� �� �� �ִ� ����߿��� ��������Ʈ�� ����� �Ÿ� ã���� ���� �ִ�Ÿ�
        NavMeshHit hit;
        //samplepos �̿��ؼ� ��ֹ� �ִ� ��ġ���� ����Ʈ ���� ����, bake �� ��ġ��
        //NavMeshPermalink : AI ������Ʈ�� �ɾ�ٴ� �� �ִ� ǥ��. �׺���̼� ��θ� ����� �� �ִ� ǥ���� �ȴ�.
        //navmesh.allareas�� �ش��ϴ� navmesh �� maxDist �ݰ� ������ randomPoint���� ���� ����� ��ġ hit�� ����
        if (NavMesh.SamplePosition(randomPoint, out hit, maxDist, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            randomPosResult = hit.position;
            return true;
        }
        return false;
    }
    

    void rayChkDoor()
    {
        RaycastHit hit;
        float MaxDistance = 2f;// ������ ����
        if (Physics.Raycast(_transform.position, _transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.CompareTag("EventObj") && hit.distance < 2.0f)
            {
                Debug.DrawRay(_transform.position, _transform.forward * MaxDistance, Color.blue, 3.0f);
                GameObject.Find(hit.collider.name).GetComponent<ObjectManager>().Activate();
            }

        }
    }
    void PlayerDeath()
    {
        //�÷��̾� ���
    }
}
