using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearRange : MonoBehaviour
{
    [SerializeField]
    private GameObject lastHearPos;
    [SerializeField]
    private EnemyAI ai;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!lastHearPos)
        {
            if(collision.gameObject.GetComponent<SoundSource>())
            {
                lastHearPos = collision.gameObject;
                ai.HearSmtAt(lastHearPos.transform);
            }
        }
    }
}
