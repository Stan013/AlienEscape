using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    private GameObject tilemap;
    private GameObject pc;

    [SerializeField]
    private ViewRange vr;
    [SerializeField]
    private HearRange hr;

    public bool pcIsInView = false;

    private Transform viewPos;
    private Transform hearPos;

    [SerializeField]
    private Transform lastSeenPos;
    private bool Stunned = false;
    private int StunnedSeconds;
    private float timer = 0.0f;

    [SerializeField]
    private int Hitpoint;
    private int HitpointMax = 10;

    public bool WantToSwitchLight = false;


    [SerializeField]
    private bool StaticGuard = true;
    [SerializeField]
    private Transform guardingPos;
    [SerializeField]
    private List<Transform> wayPoints = new List<Transform>();

    public bool AchievedDes = false;

    private AIDestinationSetter setter;

    private LightSystem ls;

    private void Start()
    {
        #region Refs.
        pc = FindObjectOfType<CharacterMovement>().gameObject;
        tilemap = FindObjectOfType<Tilemap>().gameObject;
        setter = GetComponent<AIDestinationSetter>();
        #endregion
        Hitpoint = HitpointMax;
    }

    private void Update()
    {
        if(viewPos != null)
        {
            pcIsInView = true;
        }
        else
        {
            pcIsInView = false;
        }
        IsStunned();
        CheckHP();
        //ReturnGuardingPos();
    }

    public void SeePlayerAt(Transform pos)
    {
        if(hearPos) Destroy(hearPos.gameObject);
        viewPos = pos;
        if (pcIsInView) ChasePlayerMode();
        setter.target = viewPos;

    }

    public void HearSmtAt(Transform pos)
    {
        hearPos = pos;
        SearchMode();
        if (!pcIsInView) setter.target = hearPos;
    }

    public void ShootPlayer()
    {

    }

    private void ChasePlayerMode()
    {
        GetComponent<AIPath>().maxSpeed = 6;
    }

    private void SearchMode()
    {
        GetComponent<AIPath>().maxSpeed = 2;
    }

    public void DamageHP(int value)
    {
        Hitpoint -= value;
    }

    public void StunEnemy(int stunSeconds)
    {
        Debug.Log("stun enemy, view to null");
        Stunned = true;
        StunnedSeconds = stunSeconds;
        timer = 0.0f;


    }
    private void IsStunned()
    {
        if (Stunned)
        {
            GetComponent<AIPath>().isStopped = true; //cant walk while stunned

            timer += Time.deltaTime;
            if (timer > StunnedSeconds)
            {
                timer -= StunnedSeconds;
                Stunned = false;
                GetComponent<AIPath>().isStopped = false;
            }
        }
    }
    private void CheckHP()
    {
        if(Hitpoint <= 0)
        {
            Die();
        }
    }

    [ContextMenu("KillThisGuy")]
    private void Die()
    {
        print(this.gameObject + " is dead.");
        LightSystem[] lss = FindObjectsOfType<LightSystem>();
        foreach(LightSystem ls in lss)
        {
            ls.RemoveRequst(this.gameObject);
        }

        this.gameObject.SetActive(false);
    }

    public ViewRange GetViewRange()
    {
        return vr;
    }

    public HearRange GetHearRange()
    {
        return hr;
    }

    public void InformLight(Transform pos)
    {
        if(!pcIsInView)
        {
            WantToSwitchLight = true;
            ChasePlayerMode();
            setter.target = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<LightSwitch>())
        {
            if(WantToSwitchLight)
            {
                collision.GetComponent<LightSwitch>().switchLight();
                WantToSwitchLight = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterMovement>())
        {
            collision.gameObject.GetComponent<CharacterMovement>().GetKilled();
        }
    }

    private void ReturnGuardingPos()
    {

    }
}