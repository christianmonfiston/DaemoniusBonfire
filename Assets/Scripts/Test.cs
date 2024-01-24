using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	// Start is called before the first frame update
	public float startTime = 0f;
	public float i = 1;
	public float holdTime = 5.0f; // 5 seconds
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			startTime = Time.time;
			Debug.Log("a: " + startTime + holdTime);
			Debug.Log("aa: " + Time.time);
			Debug.Log("i: " + i);
			i = i + 1;
			if (startTime + holdTime >= Time.time)

			{
				Debug.Log("it worked");

			}
		}
	}
}
