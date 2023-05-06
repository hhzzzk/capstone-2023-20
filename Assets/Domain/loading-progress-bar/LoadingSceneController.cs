using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private TMP_Text tip;

    static int nextLevel;

    
    static string[] tips = { "tip1", "tip2", "tip3", "tip4", "tip5" };
    public static void LoadScene(int level)
    {
        nextLevel = level;
        SceneManager.LoadScene("LoadingScene");

    }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
        tip.text = "Tips : " + tips[nextLevel + 1];
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextLevel);
        op.allowSceneActivation = false; //�ε� �ð��� �ʹ� ª���� ����(�Ƹ� ������� Ŀ�� �ʿ���� ���� ����

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer = Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}
