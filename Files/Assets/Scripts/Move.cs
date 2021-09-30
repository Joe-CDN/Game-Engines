using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Camera cam;
    public GameObject Respawn;
    public GameObject Finish;

    public float movespeed = 10f;
    public float jumpHeight = 250f;

    private Rigidbody rb;

    private float dirX;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * movespeed;

        if (Input.GetButtonDown("Jump") && jump == false)
        {
            jump = true;
            rb.AddForce(Vector3.up * jumpHeight);
        }

        
    }
    private void FixedUpdate()
    {
        if (jump == false)
        {
            rb.AddForce(dirX, rb.velocity.y, 0f);
        }
        cam.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -10f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            jump = false;
        }
        if (collision.collider.tag.Equals("Death"))
        {
            rb.transform.position = Respawn.transform.position;
        }
        if (collision.collider.tag.Equals("End"))
        {
            rb.transform.position = Finish.transform.position;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            jump = true;
        }
    }
}
