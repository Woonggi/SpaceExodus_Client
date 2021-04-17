using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float moveSpeed;
    public float rotSpeed;
    public bool moveLock = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    private void Update()
    {
        float heading = transform.rotation.eulerAngles.z + 90.0f;
        Vector3 inputDirection = Vector3.zero;
        Vector3 inputAngle = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputDirection = new Vector3(Mathf.Cos(heading * Mathf.Deg2Rad), Mathf.Sin(heading * Mathf.Deg2Rad), 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputAngle.z -= 3.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputAngle.z += 3.0f;
        }
        if (moveLock == false)
        {
            Move(inputDirection, inputAngle);
        }
        Shoot();
    }
    private void Move(Vector3 inputDirection, Vector3 inputAngle)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        bool onScreen = screenPoint.x > 0 && screenPoint.x < Screen.width && screenPoint.y > 0 && screenPoint.y < Screen.height;

        if (onScreen == false)
        {
            Vector3 newScreenPoint = new Vector3(Screen.width - screenPoint.x, Screen.height - screenPoint.y, 10.0f);
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(newScreenPoint);
            transform.position = screenToWorld;
        }
        transform.position += inputDirection * moveSpeed * Time.deltaTime;
        transform.Rotate(inputAngle * rotSpeed * Time.deltaTime);
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Shoot
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            float heading = transform.rotation.eulerAngles.z + 90.0f;
            Vector3 direction = new Vector3(Mathf.Cos(heading * Mathf.Deg2Rad), Mathf.Sin(heading * Mathf.Deg2Rad));
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }
    }
}
