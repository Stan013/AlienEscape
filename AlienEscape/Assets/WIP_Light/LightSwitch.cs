using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField]
    private LightSystem ls;

    [SerializeField]
    private Sprite onSprite, offSprite;

    private bool playerNear = false;
    private bool switchOn = true;

    private void Start()
    {
        if(switchOn)
        {
            GetComponent<SpriteRenderer>().sprite = onSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = offSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>())
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterMovement>())
        {
            playerNear = false;
        }
    }

    private void Update()
    {
        if(playerNear)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                switchLight();
            }
        }
    }

    public void switchLight()
    {
        if(switchOn)
        {
            switchOn = false;
        }
        else
        {
            switchOn = true;
        }

        if(switchOn)
        {
            ls.TurnOnLight();
        }
        else
        {
            ls.TurnOffLight();
        }

        if (switchOn)
        {
            GetComponent<SpriteRenderer>().sprite = onSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = offSprite;
        }
    }
}
