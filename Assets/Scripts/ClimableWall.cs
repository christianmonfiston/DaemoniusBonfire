using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class ClimableWall : MonoBehaviour
{
	// Start is called before the first frame update
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	void Update()
	{
		
	}
	private void OnCollisionStay2D(Collision2D other)
	{
		// Debug.Log(other.gameObject.tag);

		if (other.gameObject.tag == "climeBeam")
		{
			Vector3 hit = other.contacts[0].normal;
			float angle = Vector3.Angle(hit, Vector3.up);
			// Debug.Log(angle);
			if (Mathf.Approximately(angle, 90) && Input.GetKey(KeyCode.F))
			{

				rb.velocity = new Vector2(rb.velocity.x, 2.0f);
				if (Input.GetKey(KeyCode.RightShift))
					rb.velocity = new Vector2(rb.velocity.x, 0.0f);
				// Debug.Log("can climb");
			}

		}
	}
}
