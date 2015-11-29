using UnityEngine;
using System.Collections;

public class DamageReceiver : MonoBehaviour {

	private float mass; // defines the character mass

	private Vector3 push = Vector3.zero;
	private CharacterController cc;
	private PlayerInfo pi;

	// Use this for initialization
	void Start () 
	{
		cc = GetComponent<CharacterController>();
		pi = GetComponent<PlayerInfo>();

		mass = pi.mass;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// apply the impact force:
		if (push.magnitude > 0.2F)
			cc.Move(push * Time.deltaTime);

		// Slows the impact on each update.
		push = Vector3.Lerp(push, Vector3.zero, 5 * Time.deltaTime);
	}

	// call this function to add an impact force:
	public void ApplyForce(ArrayList DirForce)
	{

		float force = (float)DirForce[0];
		Vector3 dir = (Vector3)DirForce[1];
		dir.y = 0.5f;
		dir.Normalize();

		push += (dir.normalized * (force / mass));
	}
}
