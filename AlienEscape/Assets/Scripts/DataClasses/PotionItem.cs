using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PotionItem : ThrowableItem
{
    public uint potionSort;
    private int targetTime = 420;
    public static bool effectDown = false;
    public static bool thrown = false;

    public void effectTimer(GameObject gameObject)
    {
        if(effectDown)
        {
            if(thrown)
            {
                effectCollider(gameObject);
            }
        }
    }

    private void effectCollider(GameObject gameObject)
    {
        gameObject.AddComponent<AreaEffect>();
        gameObject.GetComponent<CircleCollider2D>().radius = 2.0f;
        gameObject.AddComponent<ParticleSystem>();
        thrown = false;
    }
}
