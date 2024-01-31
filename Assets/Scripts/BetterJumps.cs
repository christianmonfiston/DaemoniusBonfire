using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumps : MonoBehaviour
{
	// Start is called before the first frame update
	private Rigidbody2D rb;
	public float velocityY;
	private float targetVelocity;
	public float jumpForce = 7.0f;
	public float jumpTime = 7.0f;
	public bool isJumping;
	public bool isGrounded = false;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}


	// Update is called once per frame
	void Update()
	{
		rb = GetComponent<Rigidbody2D>();

		// targetVelocity = rb.velocity.y * velocityY;
		// rb.velocity = new Vector2(rb.velocity.x, targetVelocity);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (isGrounded)
			{
				isJumping = true;

			}
		}
		if (Input.GetKey(KeyCode.S))
		{
			rb.velocity = new Vector2(rb.velocity.x, -1.0f);
		}
		if (Input.GetKey(KeyCode.Space))
		{
			if (isJumping)
			{

				if (jumpTime > 8.0f)
				{
					// jumpTime = 9.0f;
					// rb.velocity = new Vector2(rb.velocity.x, -jumpTime);
					isJumping = false;
					jumpTime = 7.0f;
					return;
				}
				jumpTime += Time.deltaTime;
				rb.velocity = new Vector2(rb.velocity.x, jumpTime);
			}
		}
		else
		{
			isJumping = false;
			jumpTime = 7.0f;

		}
		// else
		// {
		// 	jumpTime = 7.0f;
		// }
		isGrounded = (int)rb.velocity.y < 2.0f && (int)rb.velocity.y > -2.0f;

	}

}
