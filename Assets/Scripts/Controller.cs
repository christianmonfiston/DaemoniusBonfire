using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField]
	private Vector2 velocity; //players x, y velocity
	public bool isGrounded; //is grounded
	public bool isJumping; // isJumping
	public int time;

	public const float FLOORY = -1.68f; //temporary. we are going to have to add platforms. so this will change if on platform
	void Update()
	{
		Vector2 playerLocation = GetComponent<Transform>().position;
		//temporary. 
		// we also set the is grounded location in the onPlayerCollide. or if player isnt jumping

		if (GetComponent<Rigidbody2D>().velocity.y == 0)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}

		if (Input.GetKey(KeyCode.A)) //left
		{
			playerLocation.x -= velocity.x * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;

		}
		if (Input.GetKey(KeyCode.S)) //down 
		{

			if (isJumping)
			{
				if (isGrounded)
				{
					return;
				}
				playerLocation.y -= velocity.y * Time.deltaTime;
				GetComponent<Transform>().position = playerLocation;
			}


		}
		if (Input.GetKey(KeyCode.D)) //right
		{
			playerLocation.x += velocity.x * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;
		}
		if (Input.GetKey(KeyCode.Space)) //jump
		{
			if (!isJumping)
			{
				isJumping = true;
			}
			//isIfJumping

		}
		if (isJumping)
		{
			HandleJump(60, playerLocation);
		}

	}
	private void HandleJump(int estimateFrames, Vector2 playerLocation)
	{
		if (time <= estimateFrames / 2)
		{
			playerLocation.y += velocity.y * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;
			Vector2 v = GetComponent<Rigidbody2D>().velocity;
			v.y += velocity.y * Time.deltaTime;
			GetComponent<Rigidbody2D>().velocity = v;

		}
		else
		{
			playerLocation.y -= velocity.y * Time.deltaTime;
			Vector2 v = GetComponent<Rigidbody2D>().velocity;
			v.y -= velocity.y * Time.deltaTime;
			GetComponent<Rigidbody2D>().velocity = v;
			GetComponent<Transform>().position = playerLocation;
		}
		if (time > estimateFrames || isGrounded)
		{
			isJumping = false;
			time = 0;
			return;
		}
		time++;

	}
	private void OnCollisionEnter2D(Collision2D other)
	{

		if (other.gameObject.tag == "floor")
		{
			isGrounded = true;
			Debug.Log("isGrounded");
		}
		else
		{
			isGrounded = false;
		}
	}
}
