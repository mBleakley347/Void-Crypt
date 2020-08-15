using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls player
/// 
/// movement
/// rotation
/// item collection and tracking
/// </summary>
public class SCR_PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        rb.velocity = Vector3.Lerp(rb.velocity,(transform.right * Input.GetAxis("Vertical") * speed),1f);
    }
    public void Rotate()
    {
        rb.angularVelocity = (transform.up * Input.GetAxis("Mouse X") * rotationSpeed);
    }
}
