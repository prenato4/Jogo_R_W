using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 5;
    public float speed;
    public float jumpForce;
    
    public GameObject power;
    public Transform spawn;

    private bool isfire;
    private bool IsJumPing;
    private Rigidbody2D RIG;
    private Animator AN;
    private float M;

    
    
    // Start is called before the first frame update
    void Start()
    {
        RIG = GetComponent<Rigidbody2D>();

        AN = GetComponent<Animator>();
        
        gamecontroller.instance.UpdateLives(health);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        AT();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        M = Input.GetAxis("Horizontal");

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

        if (M == 0 && !IsJumPing && !isfire)
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

    void AT()
    {
        StartCoroutine("ATA");
    }

    IEnumerator ATA()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isfire = true;
            AN.SetInteger("transition", 3);
            GameObject Power = Instantiate(power, spawn.position, spawn.rotation);

            if (transform.rotation.y == 0 )
            {
                Power.GetComponent<ATACK>().Isright = true;
            }

            if (transform.rotation.y == 180)
            {
                Power.GetComponent<ATACK>().Isright = true;
            }
            yield return new WaitForSeconds(0.6f);
            isfire = false;
            AN.SetInteger("transition", 0);
        }
    }

    public void Damage(int DM)
    {
        health -= DM;
        gamecontroller.instance.UpdateLives(health);
        AN.SetTrigger("hit");
        
        
        if (transform.rotation.y == 0 )
        {
            transform.position += new Vector3(-1f, 0, 0);
        }

        if (transform.rotation.y == 180)
        {
            transform.position += new Vector3(1f, 0, 0);
        }
        
        if (health <= 0)
        {
            gamecontroller.instance.GameOver();
        }
    }

    public void IncreaseLife(int value)
    {
        health += value;
        gamecontroller.instance.UpdateLives(health);
    }
    
    

    private void OnCollisionEnter2D(Collision2D CO)
    {
        if (CO.gameObject.layer == 6)
        {
            IsJumPing = false;
        }
        
        if (CO.gameObject.layer == 8)
        {
            gamecontroller.instance.GameOver();
        }
    }
    
}
