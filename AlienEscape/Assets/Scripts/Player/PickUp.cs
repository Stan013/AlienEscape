using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PickUp : MonoBehaviour
{

    public Item pickup;
    private Sprite icon;
    private SpriteRenderer sr;
    private Inventory inv;
    private Rigidbody2D rb;
    private CircleCollider2D cldr;

    private LightSystem ls;

    private bool pcInside = false;

    private void Start()
    {
        #region Refs.
        inv = FindObjectOfType<Inventory>();
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.material = FindObjectOfType<CharacterMovement>().transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        #endregion
        #region Make the pickup better looking.
        RefreshPickup();
        #endregion
    }

    public void RefreshPickup()
    {
        sr = GetComponent<SpriteRenderer>();
        if (pickup != null)
        {
            if (pickup.sprite != null)
            {
                sr.sprite = pickup.sprite;
            }
            if (pickup.name != null)
            {
                gameObject.name = pickup.name + "Pickup";
            }
            else
            {
                gameObject.name = "UnnamedPickup";
            }
        }
    }

    private void Update()
    {
        GetPickedUp();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterMovement>())
        {
            pcInside = true;
        }

        if(collision.GetComponent<LightSystem>())
        {
            ls = collision.GetComponent<LightSystem>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterMovement>())
        {
            pcInside = false;
        }
    }

    private void GetPickedUp()
    {
        if (pcInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inv.PickUpItem(pickup);
                // ls.RemoveRequst(this.gameObject);
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
