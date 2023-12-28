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
	public Animator animator;
	void Update()
	{

		
		animator.SetFloat("walk",Mathf.Abs(Input.GetAxisRaw("Horizontal") * 1));
		Vector2 playerLocation = GetComponent<Transform>().position;
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("falling")){
			if (Input.GetKey(KeyCode.A)) //left
			{
				playerLocation.x -= velocity.x * Time.deltaTime;
				animator.SetFloat("walk",Mathf.Abs(Input.GetAxisRaw("Horizontal") * 1));
				GetComponent<SpriteRenderer>().flipX = true;	

				GetComponent<Transform>().position = playerLocation;

			}
			if (Input.GetKey(KeyCode.D)) //right
			{

				GetComponent<SpriteRenderer>().flipX = false;	
				playerLocation.x += velocity.x * Time.deltaTime;
				GetComponent<Transform>().position = playerLocation;
			}
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
		if(Input.GetKeyDown(KeyCode.Space)){
			
			if (!isJumping)
			{
				jumpState = 1;
				isJumping = true;
				isGrounded = false;
			}
		}
		if (Input.GetKey(KeyCode.Space)) //jump
		{
			if(isJumping){
				
				if (timePerJump < 120)
				{
					timePerJump += 4;
				}
			}
		}
		if (isJumping)
		{
			HandleJump(timePerJump, playerLocation);
		}
	Vector2 e = GetComponent<Rigidbody2D>().velocity; 
	animator.SetBool("isJumping", isJumping);

	}
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
			playerLocation.y +=  velocity.y * Time.deltaTime ;
			v.y += velocity.y * Time.deltaTime;
			GetComponent<Transform>().position = playerLocation;
			GetComponent<Rigidbody2D>().velocity = v;
		}
		else if (jumpState == 2)
		{
			//falling
			playerLocation.y -= velocity.y * Time.deltaTime;

			v.y = (velocity.y - v.y) * Time.deltaTime;  //velocity*time + ((G * time^2)/2) good formula for jumps
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
				Debug.Log("test");
				animator.SetBool("isJumping", isJumping);
			}
			else if (Mathf.Approximately(angle, 180) || Mathf.Approximately(angle, 90))  //if roof collison
			{
				//Up
				if (isJumping)
				{
					Debug.Log("floor collsion");
					jumpState = 2; //modify to falling state if jumping active

				}
			}
		}
	}
	
}
