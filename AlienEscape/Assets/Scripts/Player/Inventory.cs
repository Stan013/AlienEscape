using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    [SerializeField]
    private int maxInventorySize = 3;

    //The data type of this variable is int, the name should match the data type to avoid misunderstanding. So I renamed it with word "index".
    [SerializeField]
    private int selectedItemIndex;

    [SerializeField]
    private GameObject pickupPrefab;

    [SerializeField]
    private GameObject pickupParent;

    //The variable name should reveal itself, in this case I name it "pc" stand for player character.
    private GameObject pc;

    private InventoryUI ivui;

    #region Ryan's code.

    /*
    private void Start()
    {
        pc = FindObjectOfType<CharacterMovement>().gameObject;
    }

    
    private void AddItem(Item item)
    {
        if (itemList.Count < maxInventorySize)
        {
            itemList.Add(item);
        }
        else
        {
            DropSelectedItem();
            itemList.Insert(selectedItemIndex, item);
        }
    }

    private void DropSelectedItem()
    {
        Instantiate(itemList[selectedItemIndex].parentObject, pc.transform.position, pc.transform.rotation);
        RemoveItem(itemList[selectedItemIndex]);
    }

    private void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }


    private void Update()//Note: Update and other build-in methods are basically private method.
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItemIndex = 1;
            Debug.Log(selectedItemIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (itemList.Count >= 2)
            {
                selectedItemIndex = 2;
                Debug.Log(selectedItemIndex);
            }
            else
            {
                selectedItemIndex = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (itemList.Count == 3)
            {
                selectedItemIndex = 3;
                Debug.Log(selectedItemIndex);
            }
            else
            {
                selectedItemIndex = itemList.Count;
            }
        }
        if (itemList.Count > 0)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropSelectedItem();
                if (selectedItemIndex > itemList.Count)
                {
                    selectedItemIndex = itemList.Count;
                }
            }
        if(selectedItemIndex == 0 && itemList.Count > 0)
        {
            selectedItemIndex = 1;
        }
        //if (itemList[selectedItem] == Throwable){ //throwItem
        //if (Input.GetMouseButtonDown(1)) //right click
        //{
            //DropSelectedItem & rigidBody addforce (25/weight)
        //}
        //}
    }
    */

    #endregion

    private void Start()
    {
        #region Refs.
        pc = FindObjectOfType<CharacterMovement>().gameObject;
        pickupParent = FindObjectOfType<PickupParent>().gameObject;
        ivui = FindObjectOfType<InventoryUI>();
        #endregion
    }

    private void Update()
    {
        SelectItem();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(selectedItemIndex);
        }
    }

    public void PickUpItem(Item i)
    {
        if(itemList.Count < maxInventorySize)
        {
            itemList.Add(i);
        }
        else
        {
            DropItem(selectedItemIndex);
            itemList.Insert(selectedItemIndex, i);
        }

        if(selectedItemIndex == -1)
        {
            selectedItemIndex = 0;//When picking something up, change seleted item index to a available value.
        }

        ivui.UpdateIventoryIn();
        ivui.UpdateIventorySelect(selectedItemIndex);
    }

    private GameObject DropItem(int index)
    {
        if(itemList.Count > 0)
        {
            var pickupObj = Instantiate(pickupPrefab, pc.transform.position, Quaternion.identity, pickupParent.transform);
            pickupObj.GetComponent<PickUp>().pickup = itemList[index];
            pickupObj.GetComponent<PickUp>().RefreshPickup();
            itemList.Remove(itemList[index]);
            if (selectedItemIndex >= itemList.Count)
            {
                selectedItemIndex -= 1;
            }

            ivui.UpdateIventoryIn();

            ivui.UpdateIventoryOut();
            ivui.UpdateIventorySelect(selectedItemIndex);
            return pickupObj;
        }
        else
        {
            Debug.LogWarning("There's nothing in the inventory.");
            return null;
        }
    }

    private void SelectItem()
    {
        //Note: Once the max size of inventory changed, this should also be edited.
        #region By using number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GoToItemIndex(0);
        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GoToItemIndex(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GoToItemIndex(2);
        }
        #endregion
    }

    private void GoToItemIndex(int index)
    {
        if(itemList.Count == 0)
        {
            Debug.LogWarning("No items in the inventory");
            selectedItemIndex = -1;
            ivui.UpdateIventorySelect(selectedItemIndex);
        }
        if (index >= itemList.Count)
        {
            selectedItemIndex = itemList.Count - 1;
            ivui.UpdateIventorySelect(selectedItemIndex);
        }
        else
        {
            selectedItemIndex = index;
            ivui.UpdateIventorySelect(selectedItemIndex);
        }
        //Debug.Log(itemList.Count);
    }

    public void UseItem()
    {
        if(selectedItemIndex != -1)
        {
            if (itemList[selectedItemIndex].GetType() == typeof(ThrowableItem) || itemList[selectedItemIndex].GetType() == typeof(PotionItem))
            {
                #region Throwable Object
                print(itemList[selectedItemIndex].name + " has been used.");
                ThrowItem(selectedItemIndex);
                #endregion
            }
            else
            {
                Debug.LogWarning("This item is not usable or the method is not implemented yet.");
            }
        }
        else
        {
            Debug.LogWarning("Not selecting any item");
        }

        ivui.UpdateIventoryIn();

        ivui.UpdateIventoryOut();
        ivui.UpdateIventorySelect(selectedItemIndex);
    }

    private void ThrowItem(int index)
    {
        var dir = pc.GetComponent<CharacterMovement>().GetPCMouseDir();

        var pickupObj = Instantiate(pickupPrefab, (Vector2)pc.transform.position + dir * 0.7f, Quaternion.identity, pickupParent.transform);
        pickupObj.GetComponent<PickUp>().pickup = itemList[index];
        pickupObj.GetComponent<PickUp>().RefreshPickup();
        itemList.Remove(itemList[index]);
        if (selectedItemIndex >= itemList.Count)
        {
            selectedItemIndex -= 1;
        }

        pickupObj.GetComponent<CircleCollider2D>().isTrigger = false;
        pickupObj.GetComponent<Rigidbody2D>().AddForce(dir * 3, ForceMode2D.Impulse);
        PotionItem.thrown = true;
    }
}
