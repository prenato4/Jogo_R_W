
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{

    public float SPEED;
    public float walkTime;

    public float timer;
    public bool walkRight;

    public int health;
    public int damage;

    private Rigidbody2D RIG;
    // Start is called before the first frame update
    void Start()
    {
        RIG = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= walkTime)
        {
            walkRight = !walkRight;
            timer = 0f;
        }

        if (walkRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            RIG.velocity = Vector2.left * SPEED;
        }

        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            RIG.velocity = Vector2.right * SPEED;
        }
    }

    public void Damage(int D)
    {
        health -= D;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Player")
        {
            co.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
}
