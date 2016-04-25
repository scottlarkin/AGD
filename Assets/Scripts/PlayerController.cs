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
	private Transform mesh;
	Vector3 velocity;

	CharacterController cc;
	Animator animator;

	float directionIntensity;
	int jumpCount = 0;
	Vector3 gravityDirection;
	float rayLength;
	float defaultYPos;

	public AudioClip jumpSound;
	public AudioClip moveSound;
	

	void Start () {

		pi = gameObject.GetComponent<PlayerInfo>();

		accelerationRate = pi.accelerationRate;
		decelerationRate = pi.decelerationRate;
		accelerationGravity = pi.accelerationGravity;
		maxSpeed = pi.maxSpeed;
		jumpHeight = pi.jumpHeight;
		numberOfJumps = pi.numberOfJumps;
		terminalVelocity = pi.terminalVelocity;

		cc = GetComponent<CharacterController>();

		velocity = new Vector3(0,0,0);

		gravityDirection = Vector3.Normalize(accelerationGravity);
		rayLength = 0.9f;

		//stop acceleration rate from being more than max speed
		accelerationRate = accelerationRate > maxSpeed ? maxSpeed : accelerationRate;

		animator = gameObject.transform.FindChild("character").GetComponent<Animator>();
		mesh = transform;

		gameObject.GetComponent<AudioSource>().Pause();
		gameObject.GetComponent<AudioSource>().loop = false;
		gameObject.GetComponent<AudioSource>().volume = 1000.0f;

	}

	// Update is called once per frame
	void Update () {


		
		float thumbstickDeadZone = 0.55f;
		float dt = Time.deltaTime;
		float absXVel = Mathf.Abs (velocity.x);

		if (cc.isGrounded) {
			jumpCount = 0;
			animator.SetBool ("jumping", false);
			animator.SetBool ("landed", true);
			animator.SetBool ("falling", false);
			//Debug.Log ("I've landed");
		}
		else{
			animator.SetBool ("falling", true);
		}
		
		directionIntensity = Input.GetAxis ("P_" + pi.playerNumber + " LH"); //float between -1 and 1...how far the thumb stick is pushed

		//set direction of thunmbstick to 0 if its near the middle or player is stunned
		if (Mathf.Abs (directionIntensity) <= thumbstickDeadZone || pi.isStunned()) { 
			directionIntensity = 0;
		}

		//set the boolean direction; true for right, false for left
		if (directionIntensity != 0) {
			pi.SetDirection (Mathf.Sign (directionIntensity));
			mesh.rotation = Quaternion.Euler (0, 180 * (Mathf.Sign(directionIntensity) == 1 ? 0 : 1) , 0);
		}

		//horiztal movement
		if (absXVel < maxSpeed) {
			float acc = accelerationRate * directionIntensity * dt;

			if ((acc == 0 || Mathf.Sign (velocity.x) != Mathf.Sign (acc)) && absXVel >= 1) { //should we accelerate or deccelerate?
				//deccerate
				float deccel = decelerationRate * dt;

				//prevent decelleration from accelerating in the other direction
				deccel = deccel > absXVel ? absXVel : deccel;

				//apply deceleration
				velocity += new Vector3 (-Mathf.Sign (velocity.x) * deccel, 0, 0);
			} else {
				//accelerate
				velocity += new Vector3 (acc, 0, 0);
			}
		}
		
		//jumping
		if (Input.GetKeyDown ("joystick " + pi.playerNumber + " button 0")
		    && jumpCount < numberOfJumps 
		    && !pi.isBlocking
		    && !pi.isStunned()
		    && ((jumpCount == 0 && cc.isGrounded) || jumpCount > 0)) {//jump pressed
			//reset y velocity before jump, otherwise -y velocity built up from gravity will negate 2nd jump, or +y vel from prev jump will make next jump huge
			velocity = new Vector3 (velocity.x, 0, velocity.z);
			velocity += new Vector3 (0, jumpHeight, 0);
			jumpCount++;
			animator.SetBool ("jumping", true);
			animator.SetBool ("landed", false);


			gameObject.GetComponent<AudioSource>().clip = jumpSound;
			gameObject.GetComponent<AudioSource>().Play();

		}

		velocity += accelerationGravity * dt;

		if (velocity.y < -terminalVelocity) {
			velocity = new Vector3 (velocity.x, -terminalVelocity, 0);
		}

		//prevent movement while blocking
		if (pi.isBlocking && cc.isGrounded) {
			velocity = new Vector3(0, velocity.y, 0);
		}

		//TODO: make this the speed in the direction of the surface, instead of just x, otherwise it will not produce correct results on a slope.
		animator.SetFloat ("speed", Mathf.Abs (velocity.x));

		if(Mathf.Abs(velocity.x) > 0.5f && cc.isGrounded){
			gameObject.GetComponent<AudioSource>().clip = moveSound;

			if(!gameObject.GetComponent<AudioSource>().isPlaying)
				gameObject.GetComponent<AudioSource>().Play();
		
		}
		else{
			if(!animator.GetBool("jumping"))
				gameObject.GetComponent<AudioSource>().Pause();
		}

		cc.Move (velocity * dt);

		cc.transform.position = new Vector3(cc.transform.position.x, cc.transform.position.y, -1.5f);

		//visualise direction since im just using a box
		Debug.DrawLine (transform.position, transform.position + new Vector3(pi.GetDirection(),0,0)  * 3, Color.red);
	}
}
