using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    //SerializeField can visualize the variable below in the inspector
    [SerializeField]
    private float MoveSpeed = 5;

    [SerializeField]
    private GameObject characterSprite;

    private Inventory inv;

    private Rigidbody2D rb;

    //The direction that the character is facing.
    private Vector2 dir;

    [SerializeField]
    private bool TestOnly;

    [SerializeField]
    private GameObject DeathScreen;

    private void Start()
    {
        #region Refs.
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        inv = FindObjectOfType<Inventory>();
        #endregion
        AdjustRb();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #region Set Rigidbody properties
    private void AdjustRb()
    {
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    #endregion

    #region Movement
    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.MovePosition((Vector2)this.transform.position + new Vector2(x, y) * MoveSpeed * Time.deltaTime);

        if(x<=0.25&&x>=-0.25&&y<=0.25&&y>=-0.25){
            characterSprite.GetComponent<Animator>().SetBool("Moving", false);
        }else{
            characterSprite.GetComponent<Animator>().SetBool("Moving", true);
        };

    }
    #endregion

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        dir = (GetMousePosition() - (Vector2)transform.position).normalized;
        characterSprite.transform.up = dir;
        if(Input.GetMouseButtonDown(0))
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        inv.UseItem();
    }

    public Transform GetPlayerTransform()
    {
        return transform;
    }

    public Vector2 GetPCMouseDir()
    {
        return (GetMousePosition() - (Vector2)transform.position).normalized;
    }

    public void GetKilled()
    {
        //SceneManager.LoadScene(0);
        DeathScreen.SetActive(true);
    }
}
