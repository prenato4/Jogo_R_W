using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float speed;
    public float jumpForce;

    private bool IsJumPing;
    private Rigidbody2D RIG;
    private Animator AN;
    
    // Start is called before the first frame update
    void Start()
    {
        RIG = GetComponent<Rigidbody2D>();

        AN = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float M = Input.GetAxis("Horizontal");

        RIG.velocity = new Vector2(M * speed, RIG.velocity.y);

        if (M > 0)
        {
            if (!IsJumPing)
            {
                AN.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        if (M < 0)
        {
            if (!IsJumPing)
            {
                AN.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (M == 0 && !IsJumPing)
        {
            AN.SetInteger("transition", 0);
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!IsJumPing)
            {
                AN.SetInteger("transition", 2);
                RIG.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                IsJumPing = true;
            }
        }   
    }

    private void OnCollisionEnter2D(Collision2D CO)
    {
        if (CO.gameObject.layer == 6)
        {
            IsJumPing = false;
        }
    }
}
