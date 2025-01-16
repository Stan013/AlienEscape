using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp2 : MonoBehaviour
{
    private GameObject pc;
    private GameObject tp;
    public float intp2 = 1;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<CharacterMovement>().gameObject;
        tp = FindObjectOfType<tp>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log(intp2);
                if(intp2 == 2){
                    pc.transform.position = tp.transform.position;
                    intp2 = 1;
                }else{};  
            };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterMovement>())
        {
            intp2 = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterMovement>())
        {
            intp2 = 1;
        }
    }

}