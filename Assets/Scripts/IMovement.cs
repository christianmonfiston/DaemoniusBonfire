using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
	float speedX
	{
		get; set;
	}
	// float 
	float speedY
	{
		get; set;
	}
	Rigidbody2D rb
	{
		get; set;
	}
	// Rigidbody2D rb;
	bool isGrounded
	{
		get;
		set;
	}
	//C# T~T
}
