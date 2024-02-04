using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumps : MonoBehaviour
{
	// Start is called before the first frame update
	private Rigidbody2D rb;
	public float velocityY;
	public Vector2 velocity;
	private float targetVelocity;
	public float jumpForce = 7.0f;
	public float jumpTime = 7.0f;
	public bool isJumping;
	public bool isGrounded = false;
	public Animator animator;
	// private float TargetVelocity;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}


	// Update is called once per frame
	void Update()
	{
		animator.SetBool("isJumping", isJumping || !isGrounded); // is Jumping or is Falling
		animator.SetFloat("walk", Mathf.Abs(rb.velocity.x)); //I couldnt help myself T~T

		rb = GetComponent<Rigidbody2D>();
		CalcTargetVelocity();
		if (Input.GetKey(KeyCode.S))
		{
			if (rb.velocity.y > -0.00001f)
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1.0f);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (isGrounded && !isJumping)
			{
				isJumping = true;
				jumpTime = 7.0f;

			}
		}
		if (Input.GetKey(KeyCode.Space))
		{
			if (isJumping)
			{
				HandleJump();
			}
		}
		else
		{
			isJumping = false;
		}
		isGrounded = (int)rb.velocity.y < 2.0f && (int)rb.velocity.y > -2.0f;

	}
	public void HandleJump()
	{
		if (jumpTime > 7.5f)
		{
			// jumpTime = 9.0f;
			// rb.velocity = new Vector2(rb.velocity.x, -jumpTime);
			isJumping = false;
			jumpTime = 7.0f;
			// rb.velocity = new Vector2(rb.velocity.x, -jumpTime);
			return;
		}
		jumpTime += Time.deltaTime;
		rb.velocity = new Vector2(rb.velocity.x, jumpTime);
	}
	public void HandleDash()
	{

	}
	public void CalcTargetVelocity()
	{

		targetVelocity = 8.0f * Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(targetVelocity, rb.velocity.y);
		if (targetVelocity > 1) GetComponent<SpriteRenderer>().flipX = false;
		if (targetVelocity < -1) GetComponent<SpriteRenderer>().flipX = true;
		// GetComponent<SpriteRenderer>().flipX = targetVelocity > 0;

	}
}
