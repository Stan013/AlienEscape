using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ThrowableGameObj : MonoBehaviour
{
    private GameObject tilemap;
    private bool madeSound = false;

    private Item item;
    private PotionItem pItem;

    [SerializeField]
    private GameObject soundsrc;

    private List<GameObject> hearingList = new List<GameObject>();

    private void Start()
    {
        #region Refs.
        tilemap = FindObjectOfType<Tilemap>().gameObject;
        item = this.GetComponent<PickUp>().pickup;
        if(item.GetType() == typeof(PotionItem))
        {
            pItem = (PotionItem)this.GetComponent<PickUp>().pickup;
        }
        #endregion
    }

    private void Update()
    {
        MakeStoppedObj();
        if(item.GetType() == typeof(PotionItem))
        {
            pItem.effectTimer(this.gameObject);
        }
    }

    //Temp method for testing object.
    private void MakeStoppedObj()
    {
        if(this.GetComponent<Rigidbody2D>().velocity.sqrMagnitude < 0.2f)
        {
            if(GetComponent<CircleCollider2D>().isTrigger == false)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<CircleCollider2D>().isTrigger = true;
                MakeSound();
                if(item.GetType() == typeof(PotionItem) && PotionItem.thrown == true)
                {
                    PotionItem.effectDown = true;
                } 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == tilemap)
        {
            if (GetComponent<CircleCollider2D>().isTrigger == false)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<CircleCollider2D>().isTrigger = true;
                MakeSound();
            }
        }
        if(collision.gameObject.GetComponent<EnemyAI>())
        {
            if(item.GetType() == typeof(ThrowableItem))
            {
                
                ThrowableItem tItem = (ThrowableItem)item;
                collision.gameObject.GetComponent<EnemyAI>().StunEnemy(tItem.weight);
                Hurt(collision.gameObject.GetComponent<EnemyAI>(), (int)tItem.damage);
                print(collision.gameObject + " is hurt by " + tItem.name + ", the dmg is " + tItem.damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hearingList.Contains(collision.gameObject) && collision.gameObject.GetComponent<HearRange>())
        {
            hearingList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(hearingList.Contains(collision.gameObject) && collision.gameObject.GetComponent<HearRange>())
        {
            hearingList.Remove(collision.gameObject);
        }
    }

    private void MakeSound()
    {
        if(!madeSound)
        {
            foreach (GameObject go in hearingList)
            {
                var sSrc = Instantiate(soundsrc, this.transform.position, Quaternion.identity);

                print(go.transform.parent.transform.parent + " is attracted by " + item.name);
                //go.GetComponent<HearRange>
            }
        }
        madeSound = true;
    }

    private void Hurt(EnemyAI ai, int damage)
    {
        ai.DamageHP(damage);
        //Destroy(this.gameObject);//Temp method.
        gameObject.SetActive(false);
    }

    public Item GetItem()
    {
        return item;
    }
}
