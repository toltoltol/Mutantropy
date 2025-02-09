using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBob : MonoBehaviour
{
	// Rate of the 'bob' movement
	public float bobRate;

	// Scale of the 'bob' movement
	public float bobScale;

	public bool horizontal;

	// Update is called once per frame
	void Update()
	{
		if (horizontal)
		{
			// Change in horizontal distance 
			float dx = bobScale * Mathf.Cos(bobRate * Time.time);

			// Move the game object on the horizontal axis
			transform.Translate(new Vector3(dx, 0, 0));
		}
		else
		{
			float dy = bobScale * Mathf.Sin(bobRate * Time.time);
			transform.Translate(new Vector3(0, dy, 0));
		}
	}
}