//Author:	Scott Larkin
//Date: 	Oct 26 2015
//Desc:		Player movement script

//Requirements:
// 	-Players must be tagged "P_n", where n is the player number, starting from 1... eg: "P_1"

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private PlayerInfo pi;
	private float accelerationRate;
	private float decelerationRate;
	private Vector3 accelerationGravity; //gravity on earth = 9.802m/s squared
	private float maxSpeed;
	private float jumpHeight;
	private int numberOfJumps;
	private float terminalVelocity;

	Rigidbody rb;

	float directionIntensity;
	int jumpCount = 0;
	Vector3 gravityDirection;
	float rayLength;

	void Start () {

		pi = gameObject.GetComponent<PlayerInfo>();

		accelerationRate = pi.accelerationRate;
		decelerationRate = pi.decelerationRate;
		accelerationGravity = pi.accelerationGravity;
		maxSpeed = pi.maxSpeed;
		jumpHeight = pi.jumpHeight;
		numberOfJumps = pi.numberOfJumps;
		terminalVelocity = pi.terminalVelocity;

		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0,0,0);

		gravityDirection = Vector3.Normalize(accelerationGravity);
		rayLength = 0.9f;

		//stop acceleration rate from being more than max speed
		accelerationRate = accelerationRate > maxSpeed ? maxSpeed : accelerationRate;

	}

	// Update is called once per frame
	void Update () {

		float thumbstickDeadZone = 0.55f;
		float dt = Time.deltaTime;
		float absXVel = Mathf.Abs(rb.velocity.x);

		if (Physics.Raycast(transform.position, gravityDirection, rayLength)) {
			jumpCount = 0;
		}

		directionIntensity = Input.GetAxis ("P_" + pi.playerNumber + " LH"); //float between -1 and 1...how far the thumb stick is pushed

		//set direction of thunmbstick to 0 if its near the middle
		if(Mathf.Abs(directionIntensity) <= thumbstickDeadZone){ 
			directionIntensity = 0;
		}

		//set the boolean direction; true for right, false for left
		if (directionIntensity != 0) {
			pi.SetDirection (Mathf.Sign (directionIntensity));
		}
		
		//horiztal movement
		if(absXVel < maxSpeed){
			float acc = accelerationRate * directionIntensity * dt;

			if((acc == 0 || Mathf.Sign (rb.velocity.x) !=  Mathf.Sign(acc) ) && absXVel >= 1){ //should we accelerate or deccelerate?
				//deccerate
				float deccel = decelerationRate * dt;

				//prevent decelleration from accelerating in the other direction
				deccel = deccel > absXVel ? absXVel : deccel;

				//apply deceleration
				rb.velocity += new Vector3(-Mathf.Sign(rb.velocity.x) * deccel, 0,0);
			}else{
				//accelerate
				rb.velocity += new Vector3(acc, 0, 0);
			}
		}
		
		//jumping
		if (Input.GetKeyDown("joystick "+pi.playerNumber+" button 0") && jumpCount < numberOfJumps){//jump pressed
			//reset y velocity before jump, otherwise -y velocity built up from gravity will negate 2nd jump, or +y vel from prev jump will make next jump huge
			rb.velocity = new Vector3(rb.velocity.x, 0 ,rb.velocity.y);
			rb.velocity += new Vector3(0,jumpHeight,0);
			jumpCount++;
		}

		rb.velocity += accelerationGravity * dt;

		if(rb.velocity.y < -terminalVelocity){
			rb.velocity = new Vector3(rb.velocity.x, -terminalVelocity, 0);
		}
	
		//visualise direction since im just using a box
		Debug.DrawLine (transform.position, transform.position + new Vector3(pi.GetDirection(),0,0)  * 3, Color.red);
	}
}
