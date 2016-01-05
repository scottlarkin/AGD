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
	[HideInInspector]public DIRECTION direction;
	[HideInInspector]public bool alive = true;

	private float health;
	public string playerNumber;

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
		playerNumber = gameObject.tag.Split('_')[1];

		health = maxHealth;

		GameObject o = null;
		for (int i = 1; i < 5; i++) {

			try{
				o = GameObject.FindGameObjectWithTag("P_" + i.ToString());
				if(o == gameObject)
					o = null;
			}
			catch(UnityException e){

			}

			if(o != null)
				Physics.IgnoreCollision(o.GetComponent<CharacterController>(), GetComponent<CharacterController>());
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
