using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float acceleration = 1.0f;
    [SerializeField] public float maxSpeed = 10.0f;
    [SerializeField] public float rotSpeed = 300.0f;
    [SerializeField] public float bulletSpeed = 100.0f;
    public GameObject projectile;

    private Rigidbody2D rigidbody2d;
    private Vector2 vel;
    private float angle;
    private float heading;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SendInputToServer();
    }
    private void SendInputToServer()
    {
        bool[] inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D)
        };
        ClientSend.PlayerMovement(inputs);
    }

    void Update()
    {
        Move();
        //Shoot();
        rigidbody2d.rotation = angle;
        //rigidbody2d.velocity = vel * acceleration * Time.deltaTime;
    }

    private void Move()
    {
        //heading = Mathf.Deg2Rad * (angle + 90.0f); 
        //if (Input.GetKey(KeyCode.W))
        //{
        //    // accelerate
        //    vel.x += Mathf.Cos(heading);
        //    vel.y += Mathf.Sin(heading);
        //}
        if (Input.GetKey(KeyCode.A))
        {
            // rotate player
            angle += rotSpeed * 0.03333f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // rotate player
            angle -= rotSpeed * 0.03333f;
        }
    }

    private void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().rotation = heading;
            Vector2 bulletVel;
            bulletVel.x = Mathf.Cos(heading);
            bulletVel.y = Mathf.Sin(heading);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletVel * bulletSpeed; 
        }
    }
}
