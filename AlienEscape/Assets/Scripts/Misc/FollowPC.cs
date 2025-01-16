using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPC : MonoBehaviour
{
    public GameObject PlayerCharacter;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = PlayerCharacter.transform.position + new Vector3(0,0,-1);
    }
}
