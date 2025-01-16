using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    private GameObject pc;
    private GameObject tp2;
    public float intp1 = 1;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<CharacterMovement>().gameObject;
        tp2 = FindObjectOfType<tp2>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log(intp1);
                if(intp1 == 2){
                    pc.transform.position = tp2.transform.position;
                    intp1 = 1;
                }else{};  
            };
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<CharacterMovement>())
        {
            intp1 = 2;
        }
        }

    private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.GetComponent<CharacterMovement>())
        {
            intp1 = 1;
        }
        }

}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    private GameObject pc;
    private GameObject tp2;
    public bool intp;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<CharacterMovement>().gameObject;
        tp2 = FindObjectOfType<tp2>().gameObject;
        intp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            intp = true;
        }

    private void OnTriggerExit2D(Collider2D collision)
        {
            intp = false;
        }

    private void OnTriggerStay2D(Collider2D collision)
        {
            if(Input.GetKeyDown(KeyCode.E)){
                if(intp = true){
                    teleportplayer(collision);
                };  
            };
            
        }

    void teleportplayer(Collider2D collision)
    {
        if(collision.gameObject == pc){
            collision.transform.position = tp2.transform.position;
        }
    }
}*/