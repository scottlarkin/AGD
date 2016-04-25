//Player Info
//Script for storing generic player information
//variables are public so they can be easily altered in the inspector to make balancing the game easier

//Authors: Scott Larkin / Greg Needham

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour {


	[HideInInspector]public enum DIRECTION
	{
		LEFT = -1,
		RIGHT = 1
	}

	public float maxHealth = 100.0f;
	public float accelerationRate = 10.0f;
	public float decelerationRate = 20.0f;
	public Vector3 accelerationGravity = new Vector3(0,-9.807f, 0); //gravity on earth = 9.802m/s squared
	public float maxSpeed = 100.0f;
	public float jumpHeight = 10.0f;
	public int numberOfJumps = 2;
	public float terminalVelocity = 20.0f;
	public bool isBlocking = false;
	public float mass;
	public float damageReceived = 0;

	private float stunned  = 0;
	[HideInInspector]public DIRECTION direction;
	[HideInInspector]public bool alive = true;
	[HideInInspector]public int score = 0;

	private Animator animator;
	private float health;
	public string playerNumber;

	AudioSource waterSplashSound;

	public float GetDirection()
	{
		return direction == DIRECTION.LEFT ? -1.0f : 1.0f;
	}

	public void SetDirection(float d)
	{
		direction = d == -1.0f ? DIRECTION.LEFT : DIRECTION.RIGHT;
	}
	
	// Use this for initialization
	void Start () {

		waterSplashSound = gameObject.transform.Find("character").GetComponent<AudioSource>();
		animator = gameObject.transform.FindChild("character").GetComponent<Animator>();


		waterSplashSound.volume = 10000;
		waterSplashSound.Stop();

		playerNumber = gameObject.tag.Split('_')[1];
		health = maxHealth;

		foreach(GameObject p in PlayerManager.getPlayers()){

			if(p != gameObject){
				Physics.IgnoreCollision(p.GetComponent<CharacterController>(), GetComponent<CharacterController>());
			}
		}
	}

	public AudioSource WaterSplash(){
		waterSplashSound.Play();

		return waterSplashSound;

	}

	public bool isStunned(){
		return stunned > 0;
	}

	public void stunPlayer(float duration){
		stunned += duration;
	}

	// Update is called once per frame
	void Update () {
		if(stunned > 0){
			stunned -= Time.deltaTime;
			animator.SetFloat("stunned", stunned);
		}

		//Debug.Log (playerNumber + ": = " + GetDirection()); 
	}
}
