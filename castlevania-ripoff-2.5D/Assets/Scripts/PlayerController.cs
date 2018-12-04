using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    public float maxSpeed;

    public float jumpForce;

    public bool jump;

    //https://www.youtube.com/watch?v=7KiK0Aqtmzc
    public float fallMultiplier;
    public float lowJumpMultiplier;

    //Rigidbody reference
    Rigidbody rb;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    void Update()
    {
        //jump code comes from the following video https://www.youtube.com/watch?v=7KiK0Aqtmzc
        //allows player to fall faster
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate ()
    {
        //Determines if any horizontal input is given, value of 0.0f means none
        float h = Input.GetAxis("Horizontal");

        //The following two conditionals create a speed cap
        if (h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * moveSpeed);
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            //Mathf.Sign returns -1 or 1 depending on the sign of the input
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        //Old jump code
        if (Input.GetButtonDown("Jump"))
            rb.AddForce(Vector3.up * jumpForce);
    }
}
