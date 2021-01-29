using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    { 
        transform.parent.gameObject.GetComponent<Controller>().grounded = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        transform.parent.gameObject.GetComponent<Controller>().grounded = false;
    }

    void Update()
    {
    }
}
