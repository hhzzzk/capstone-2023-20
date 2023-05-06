using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryitemController : MonoBehaviour
{
    Items item;

    public Button RemoveButton;
    public GameObject ItemView;
    public GameObject ItemTooltip;
    public GameObject SelectedItem;
    public GameObject TempView;
    private string ItemName = "";
    private string ItemDetails = "";
    public GameObject ItemModel;
    private GameObject itemPrefab;
    
    // ������(�κ��丮)�� ��Ȱ��ȭ���� ��� ItemView�� ItemTooltip ��� ��Ȱ��ȭ
    private void Update()
    {
        if (gameObject.activeSelf == false)
        {
            ItemView.SetActive(false);
            ItemTooltip.SetActive(false); 
        }
    }

    public GameObject getItemModel()
    {
        return ItemModel;
    }

    public string getItemName()
    {
        ItemName = gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text;
        return ItemName;
    }

    public string getItemDetails()
    {
        ItemDetails = gameObject.transform.Find("ItemDetails").GetComponent<TextMeshProUGUI>().text;
        return ItemDetails;
    }


    // ������ ��� �� ItemView�� View�� ������ ����.
    public void RemoveItem()
    {
        ItemName = gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text;

        if (ItemName == "Phone" || ItemName=="HP" )
        {
            InventoryManager.Instance.removeItem(item);
            Destroy(gameObject);
        }
        else
        {
            if (ItemName == "Phone") {
                ItemModel = GameObject.Find("phoneView");
                itemPrefab = Instantiate(ItemModel, new Vector3(1500, 1500, 1500), Quaternion.Euler(new Vector3(0,0,180)));
            }
            else if (ItemName == "HP")
            {
                ItemModel = GameObject.Find("syringeView");
                itemPrefab = Instantiate(ItemModel, new Vector3(1500, 1500, 1500), Quaternion.Euler(new Vector3(90,20,0)));
            }
            else if (ItemName == "W.C. Key")
            {
                Debug.Log("ItemModel: " + ItemModel);
                itemPrefab = Instantiate(ItemModel, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                Debug.Log(itemPrefab);
            }

            Debug.Log("ItemModel: " + ItemModel);
            SelectedItem = itemPrefab;
            Debug.Log("SelectedItem:"+InventoryManager.Instance.SelectedItem);
            Debug.Log("itemPrefab:"+itemPrefab);
            InventoryManager.Instance.SelectedItem = SelectedItem;
            InventoryManager.Instance.viewItem();
            ItemView.SetActive(true);
           
        }
    }

    // SelectedItem�� Nullȭ ����.
    public void CloseItemview()
    {
        GameObject DestroyItemview = SelectedItem;
        SelectedItem = TempView;
        Destroy(DestroyItemview);
    }
}


