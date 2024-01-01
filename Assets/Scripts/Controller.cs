using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField]
	private Vector2 velocity; //players x, y velocity
	public bool isGrounded; //is grounded
	public bool isJumping; // isJumping
	public float time = 1; //time of jump (in frames 1 jump = 60 frames)
	public float timePerJump; //this is the time per a jump. out of 60
	public int jumpState = 1;
	public Animator animator;
	void Update()
	{

		
		animator.SetFloat("walk",Mathf.Abs(Input.GetAxisRaw("Horizontal") * 1)); //I couldnt help myself T~T
		Vector2 playerLocation = GetComponent<Transform>().position;
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("falling")){
			if (Input.GetKey(KeyCode.A)) //left
			{
				
				GetComponent<SpriteRenderer>().flipX = true;	//flips the X so you can get forward and backwards
				playerLocation.x -= velocity.x * Time.deltaTime;
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
			if (isJumping && time > 1)
			{

				Vector2 v = GetComponent<Rigidbody2D>().velocity;
				v.y -= velocity.y * Time.deltaTime;
				jumpState = 2;
				GetComponent<Rigidbody2D>().velocity = v;
			}

		}
		if(Input.GetKeyDown(KeyCode.Space)){
			
			if (!isJumping && isGrounded)
			{
				jumpState = 1;
				isJumping = true;
				isGrounded = false;
			}
		}
		if (Input.GetKey(KeyCode.Space)) //jump
		{
			if(isJumping){
				
				if (timePerJump < 400)
				{
					timePerJump += 4;
					timePerJump += timePerJump * Time.deltaTime;
				}
			}
		}
		if (isJumping)
		{
			HandleJump(timePerJump, playerLocation);
		}

		//this handles the jump animation
		Vector2 e = GetComponent<Rigidbody2D>().velocity; //RB velocity
		if(!isJumping){
			isGrounded = (int)e.y > -1; //if jumping. is grounded is set by a collison or if the jump is complete	
		}
		animator.SetBool("isJumping", isJumping || (int)e.y < -1); // is Jumping or is Falling

	}
	/// <summary>
	/// handles the jump of the player
	/// uses state machine. 
	/// 1st state is jumping 2nd state is falling. 
	/// we can add a 3rd state so it can make the jumps more like a "Sine wave" rather than sharp. 
	/// </summary>
	/// <param name="estimateFrames"> estimated time of the Jump. either 60 </param>
	/// <param name="playerLocation"></param> location of the player<summary>
	
	private void HandleJump(float estimateFrames, Vector2 playerLocation)
	{
		Vector2 v = GetComponent<Rigidbody2D>().velocity; //RB velocity

		if (jumpState == 1)
		{
			if((int)estimateFrames > 1){
				
				if (time > (int)estimateFrames / 2) //change to the falling state
				{
					jumpState = 2; //commit from neo-vim
					return;
				}
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

		
		Debug.Log(time);
		time += 1;
		time +=  time* Time.deltaTime;
	}
	/// <summary>
	/// resets jump states.
	/// </summary>
	
	private void ResetJumps()
	{
		isJumping = false;
		time = 1;
		jumpState = 1;
		timePerJump = 1;
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
				animator.SetBool("isJumping", isJumping);
			}
			else if (Mathf.Approximately(angle, 180) || Mathf.Approximately(angle, 90))  //if roof collison
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
