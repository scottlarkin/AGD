using UnityEngine;
using System.Collections;

public class BouncyPlatform : MonoBehaviour {


	public float bouncePower = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		other.rigidbody.AddForce(Vector2.up * bouncePower, ForceMode.Impulse);
		//Vector2(-other.relativeVelocity.x, other.relativeVelocity.y)
		// set x value of new force vector to 0.0f, if you need the jump was straight up
	}
}
