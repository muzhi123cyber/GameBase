/****************************************************
 *  文件：PlayerContoller.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/31 12:55:52
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject[] guns;

    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D body;

    private Vector2 moveDir;
    private Vector3 mousePos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        Move();
    }


    private void Move()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        moveDir = moveDir.normalized;
        body.velocity = moveDir * moveSpeed;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ( mousePos.x > this.transform.position.x )
            sr.flipX = false;
        else if(mousePos.x < this.transform.position.x)
            sr.flipX = true;

        if (moveDir != Vector2.zero)
            anim.SetBool("isMoving",true);
        else
            anim.SetBool("isMoving", false);
    }



    private void SwitchGun()
    { 


    }


}
