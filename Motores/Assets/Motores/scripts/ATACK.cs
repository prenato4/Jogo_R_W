using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATACK : MonoBehaviour
{

    private Rigidbody2D RIG;
    public float SPEED;

    public bool Isright;
    
    // Start is called before the first frame update
    void Start()
    {
        RIG = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Isright)
        {
            RIG.velocity = Vector2.right * SPEED;
        }

        else
        {
            RIG.velocity = Vector2.left * SPEED;
        }
        
    }
}
