using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float h_speed = 3.0f;
    public float jump_speed = 100.0f;

    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    Rigidbody2D rigidbody;
    void Start()
    {
        grounded = false;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Jump()
    {
        rigidbody.AddForce(transform.up * jump_speed);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 t = new Vector3(0.0f, 0.0f, 0.0f);
        if(Input.GetKey(KeyCode.D)) 
        {
            t.x = h_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            t.x = -h_speed * Time.deltaTime;
        }
        if (grounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        transform.position += t;
    }
}
