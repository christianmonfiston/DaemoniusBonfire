using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField]
	private Vector2 velocity; //players x, y velocity
	public bool isGrounded; //is grounded
	public bool isJumping; // isJumping
	public bool isFalling; //if falling
	public int time; //time of jump (in frames 1 jump = 60 frames)

	void Update()
	{
		Vector2 playerLocation = GetComponent<Transform>().position;
		isGrounded = GetComponent<Rigidbody2D>().velocity.y == 0;
		if (Input.GetKey(KeyCode.A)) //left
		{
			playerLocation.x -= velocity.x * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;

		}
		if (Input.GetKey(KeyCode.S)) //down 
		{
			if (isJumping)
			{

				Vector2 v = GetComponent<Rigidbody2D>().velocity;
				v.y -= velocity.y * Time.deltaTime;
				isFalling = true;
				GetComponent<Rigidbody2D>().velocity = v;
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
		}
		if (isJumping)
		{
			HandleJump(60, playerLocation);
		}

	}
	/// <summary>
	/// handles the jump of the player
	/// </summary>
	/// <param name="estimateFrames"> estimated time of the Jump. either 60 </param>
	/// <param name="playerLocation"></param> location of the player<summary>
	private void HandleJump(int estimateFrames, Vector2 playerLocation)
	{
		if (time <= estimateFrames / 2 && !isFalling)
		{
			playerLocation.y += velocity.y * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;
			Vector2 v = GetComponent<Rigidbody2D>().velocity;
			v.y += velocity.y * Time.deltaTime;
			Debug.Log(v.y);
			GetComponent<Rigidbody2D>().velocity = v;

		}
		else //ifFalling
		{
			isFalling = true;
			playerLocation.y -= velocity.y * Time.deltaTime;
			Vector2 v = GetComponent<Rigidbody2D>().velocity;
			v.y = (velocity.y - v.y) * Time.deltaTime;
			GetComponent<Rigidbody2D>().velocity = v;
			GetComponent<Transform>().position = playerLocation;
		}
		if (time > estimateFrames || isGrounded)
		{
			isJumping = false;
			time = 0;
			isFalling = false;
			return;
		}
		time++;

	}
	private void OnCollisionEnter2D(Collision2D other)
	{


		if (other.gameObject.tag == "floor")
		{
			Debug.Log("test");
			isGrounded = true;
			isJumping = false;
			time = 0;
			isFalling = false;
		}
	}
}
