using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory iv;

    [SerializeField]
    private List<Image> slots = new List<Image>(3);

    [SerializeField]
    private Image select;

    [SerializeField]
    private Sprite missing;

    private void Start()
    {
        iv = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        //UpdateIventory();
    }

    public void UpdateIventoryIn()
    {
        if(iv.itemList.Count > 0)
        {
            int index = 0;
            foreach(Item i in iv.itemList)
            {
                if(i.sprite)
                {
                    slots[index].sprite = i.sprite;
                    slots[index].color = new Color(255f, 255f, 255f, 1f);
                }
                else
                {
                    slots[index].sprite = missing;
                    slots[index].color = new Color(255f, 255f, 255f, 1f);
                }
                index++;
            }
        }

        if (iv.itemList.Count < 3)
        {
            for (int i = iv.itemList.Count; i < 3; i++)
            {
                slots[i].sprite = null;
                slots[i].color = new Color(0f, 0f, 0f, 0f);
            }
        }

    }

    public void UpdateIventoryOut()
    {
        if(iv.itemList.Count < 3)
        {
            for(int i = iv.itemList.Count; i < 3; i++)
            {
                slots[i].sprite = null;
                slots[i].color = new Color(0f, 0f, 0f, 0f);
            }
        }
    }

    public void UpdateIventorySelect(int i)
    {
        if(i == -1)
        {
            select.color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            select.color = new Color(255f, 255f, 255f, 1f);
            select.GetComponent<RectTransform>().position = slots[i].GetComponent<RectTransform>().position;

        }
    }
}
