using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightSystem : MonoBehaviour
{
    [SerializeField]
    private bool LightOn;

    [SerializeField]
    private GameObject lightSwitch;

    [SerializeField]
    private SpriteRenderer pcSr;

    [SerializeField]
    private TilemapRenderer mpR;

    [SerializeField]
    private Material darkM;
    [SerializeField]
    private Material normM;

    [SerializeField]
    private List<GameObject> objectList = new List<GameObject>();

    private void Start()
    {
        #region Refs.        
        mpR = transform.parent.GetComponent<TilemapRenderer>();
        pcSr = FindObjectOfType<CharacterMovement>().transform.GetChild(0).GetComponent<SpriteRenderer>();
        #endregion
    }

    private void Update()
    {
        /*
        if(LightOn)
        {
            TurnOnLight();
        }
        else
        {
            TurnOffLight();
        }*/
    }
    [ContextMenu("Turn off the light")]
    public void TurnOffLight()
    {
        LightOn = false;
        mpR.material = darkM;
        foreach(GameObject go in objectList)
        {
            if(go.GetComponent<CharacterMovement>())
            {
                pcSr.material = darkM;
            }
            if(go.GetComponent<EnemyAI>())
            {
                go.transform.GetChild(0).GetComponent<SpriteRenderer>().material = darkM;
                go.GetComponent<EnemyAI>().GetViewRange().SetToDarkMode();
            }
            if (go.GetComponent<PickUp>())
            {
                go.GetComponent<SpriteRenderer>().material = darkM;
            }
        }

        InformEnemies();
        /*
        enemiesList = FindObjectOfType<EnemiesManager>().GetList();
        pcSr.material = darkM;
        mpR.material = darkM;
        foreach(EnemyAI e in enemiesList)
        {
            e.transform.GetChild(0).GetComponent<SpriteRenderer>().material = darkM;
            e.GetViewRange().SetToDarkMode();
        }*/
    }

    [ContextMenu("Turn on the light")]
    public void TurnOnLight()
    {
        LightOn = true;
        mpR.material = normM;
        foreach (GameObject go in objectList)
        {
            if (go.GetComponent<CharacterMovement>())
            {
                pcSr.material = normM;
            }
            if (go.GetComponent<EnemyAI>())
            {
                go.transform.GetChild(0).GetComponent<SpriteRenderer>().material = normM;
                go.GetComponent<EnemyAI>().GetViewRange().SetToLightMode();
            }
            if (go.GetComponent<PickUp>())
            {
                go.GetComponent<SpriteRenderer>().material = normM;
            }
        }
        /*
        enemiesList = FindObjectOfType<EnemiesManager>().GetList();
        pcSr.material = normM;
        mpR.material = normM;
        foreach (EnemyAI e in enemiesList)
        {
            e.transform.GetChild(0).GetComponent<SpriteRenderer>().material = normM;
            e.GetViewRange().SetToLightMode();
        }*/
    }

    public bool GetLightOn()
    {
        return LightOn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyAI>() || collision.GetComponent<CharacterMovement>())
        {
            if (!objectList.Contains(collision.gameObject))
            {
                if (!LightOn)
                {
                    if (collision.gameObject.GetComponent<CharacterMovement>())
                    {
                        pcSr.material = darkM;
                    }
                    if (collision.gameObject.GetComponent<EnemyAI>())
                    {
                        collision.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = darkM;
                        collision.gameObject.GetComponent<EnemyAI>().GetViewRange().SetToDarkMode();
                    }
                    if (collision.GetComponent<PickUp>())
                    {
                        collision.GetComponent<SpriteRenderer>().material = darkM;
                    }
                }
                else
                {
                    if (collision.gameObject.GetComponent<CharacterMovement>())
                    {
                        pcSr.material = normM;
                    }
                    if (collision.gameObject.GetComponent<EnemyAI>())
                    {
                        collision.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = normM;
                        collision.gameObject.GetComponent<EnemyAI>().GetViewRange().SetToLightMode();
                    }
                    if (collision.GetComponent<PickUp>())
                    {
                        collision.GetComponent<SpriteRenderer>().material = normM;
                    }
                }

            }
        }

        objectList.Add(collision.gameObject);

        if (!LightOn)
        {
            InformEnemies();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectList.Remove(collision.gameObject);
    }

    public void RemoveRequst(GameObject go)
    {
        print("Remove Request received");
        if (GetObjList().Contains(this.gameObject))
        {
            objectList.Remove(go);
            print("Gameobject removed");
        }
        else
        {
            print("Couldn't find the gameobject in light system of " + this.transform.parent);
        }
    }

    public void SetMaterial(GameObject go)
    {
        if(go.GetComponent<Renderer>())
        {
            if(LightOn)
            {
                go.GetComponent<Renderer>().material = normM;
            }
            else
            {
                go.GetComponent<Renderer>().material = darkM;
            }
        }
    }

    private void InformEnemies()
    {
        List<EnemyAI> enmL = new List<EnemyAI>();

        foreach(GameObject go in objectList)
        {
            if(go.GetComponent<EnemyAI>() && go.activeSelf != false)
            {
                enmL.Add(go.GetComponent<EnemyAI>());
            }
        }

        float distance = 100000000;
        EnemyAI theOne = null;
        foreach(EnemyAI ai in enmL)
        {
            if(Vector2.Distance(ai.transform.position, lightSwitch.transform.position) < distance)
            {
                distance = Vector2.Distance(ai.transform.position, lightSwitch.transform.position);
                theOne = ai;
            }
        }

        print(theOne);
        theOne.InformLight(lightSwitch.transform);
    }

    public List<GameObject> GetObjList()
    {
        return objectList;
    }
}
