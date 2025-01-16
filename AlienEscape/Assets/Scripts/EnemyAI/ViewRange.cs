using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class ViewRange : MonoBehaviour
{
    [SerializeField]
    private EnemyAI ai;
    private GameObject pc;
    private GameObject tilemap;

    [SerializeField]
    private List<GameObject> maps = new List<GameObject>();

    private CapsuleCollider2D range;

    [SerializeField]
    private float offsetYLM = 2.979516f;
    [SerializeField]
    private float sizeYLM = 8f;
    [SerializeField]
    private float offsetYDM = 2.125477f;
    [SerializeField]
    private float sizeYDM = 6f;

    [SerializeField]
    private List<GameObject> list = new List<GameObject>();

    private void Start()
    {
        #region Refs.
        pc = FindObjectOfType<CharacterMovement>().gameObject;
        tilemap = FindObjectOfType<Tilemap>().gameObject;
        range = GetComponent<CapsuleCollider2D>();

        var m = FindObjectsOfType<Tilemap>();
        foreach(Tilemap t in m)
        {
            maps.Add(t.gameObject);
        }
        #endregion
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == pc)
        {
            RaycastHit2D[] results = Physics2D.RaycastAll(transform.position, (pc.transform.position - transform.position).normalized, Vector2.Distance(pc.transform.position, transform.position));
            Debug.DrawRay(transform.position, pc.transform.position - transform.position, Color.yellow, 0.1f);

            foreach (RaycastHit2D result in results)
            {
                if (list.Contains(result.collider.gameObject) == false)
                {
                    list.Add(result.collider.gameObject);
                }
            }
            if (list.Contains(maps[0]) == false &&
                list.Contains(maps[1]) == false &&
                list.Contains(maps[2]) == false &&
                list.Contains(maps[3]) == false &&
                list.Contains(maps[4]) == false && list.Count != 0)
            {
                ai.SeePlayerAt(pc.GetComponent<CharacterMovement>().GetPlayerTransform());
                ai.pcIsInView = true;
            }
            else
            {
                ai.SeePlayerAt(null);
                ai.pcIsInView = false;
            }
            list.Clear();//Can be optimized.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ai.SeePlayerAt(null);
        ai.pcIsInView = false;
    }

    [ContextMenu("Set view range to L mode")]
    public void SetToLightMode()
    {
        range.offset = new Vector2(0.04632092f, offsetYLM);
        range.size = new Vector2(5, sizeYLM);
        //print("Lm");
    }

    [ContextMenu("Set view range to D mode")]
    public void SetToDarkMode()
    {
        range.offset = new Vector2(0.04632092f, offsetYDM);
        range.size = new Vector2(5, sizeYDM);
        //print("Dm");
    }
}
