using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField]
	private Vector2 velocity; //players x, y velocity
	public bool isGrounded; //is grounded
	public bool isJumping; // isJumping
	public int time; //time of jump (in frames 1 jump = 60 frames)
	public int timePerJump; //this is the time per a jump. out of 60
	public int jumpState = 1;
	void Update()
	{
		Vector2 playerLocation = GetComponent<Transform>().position;
		if (Input.GetKey(KeyCode.A)) //left
		{
			playerLocation.x -= velocity.x * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;

		}

		if (Input.GetKey(KeyCode.D)) //right
		{
			playerLocation.x += velocity.x * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;
		}
		if (Input.GetKey(KeyCode.S)) //down 
		{
			if (isJumping && time > 3)
			{

				Vector2 v = GetComponent<Rigidbody2D>().velocity;
				v.y -= velocity.y * Time.deltaTime;
				jumpState = 2;
				GetComponent<Rigidbody2D>().velocity = v;
			}

		}
		if (Input.GetKey(KeyCode.Space)) //jump
		{
			if (timePerJump < 60)
			{
				timePerJump += 4;
			}
			if (!isJumping)
			{
				jumpState = 1;
				isJumping = true;
				isGrounded = false;
			}
		}
		if (isJumping)
		{
			HandleJump(timePerJump, playerLocation);
		}

	}
	int t = 0;
	/// <summary>
	/// handles the jump of the player
	/// uses state machine. 
	/// 1st state is jumping 2nd state is falling. 
	/// we can add a 3rd state so it can make the jumps more like a "Sine wave" rather than sharp. 
	/// </summary>
	/// <param name="estimateFrames"> estimated time of the Jump. either 60 </param>
	/// <param name="playerLocation"></param> location of the player<summary>
	private void HandleJump(int estimateFrames, Vector2 playerLocation)
	{
		Vector2 v = GetComponent<Rigidbody2D>().velocity; //RB velocity

		if (jumpState == 1)
		{
			if (time >= estimateFrames / 2) //change to the falling state
			{
				jumpState = 2; //commit from neo-vim
				return;
			}
			//jumping
			playerLocation.y +=  velocity.y * Time.deltaTime + ((9.8f*(Time.deltaTime*Time.deltaTime))/2);
			v.y += velocity.y * Time.deltaTime + ((9.8f*(Time.deltaTime*Time.deltaTime))/2);
			GetComponent<Transform>().position = playerLocation;
			GetComponent<Rigidbody2D>().velocity = v;
		}
		//else if(jumpState == 2)
		//{
		//	
		//	t++;
		//	if(t == 10){
		//		jumpState = 3;
		//		t = 0;
		//	}
		//}
		else if (jumpState == 2)
		{
			//falling
			playerLocation.y -= velocity.y * Time.deltaTime + ((9.8f*(Time.deltaTime*Time.deltaTime))/2);

			v.y = (velocity.y - v.y) * Time.deltaTime + ((9.8f*(Time.deltaTime*Time.deltaTime))/2);
			GetComponent<Rigidbody2D>().velocity = v;
			GetComponent<Transform>().position = playerLocation;
		}
		//this makes sure its valid
		if (time > estimateFrames && isGrounded)
		{
			ResetJumps();
			return;
		}
		time++;

	}
	/// <summary>
	/// resets jump states.
	/// </summary>
	private void ResetJumps()
	{
		isJumping = false;
		time = 0;
		jumpState = 1;
		timePerJump = 0;
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "floor"){

			//calculates angle so you only get it iff its on the floor
			Vector3 hit = other.contacts[0].normal;
			float angle = Vector3.Angle(hit, Vector3.up);
			if (Mathf.Approximately(angle, 0))  //if floor collsion
			{
				ResetJumps();
				isGrounded = true;
			}
			else if (Mathf.Approximately(angle, 180))  //if roof collison
			{
				//Up
				if (isJumping)
				{
					jumpState = 2; //modify to falling state if jumping active

				}
			}
		}
	}
	
}
