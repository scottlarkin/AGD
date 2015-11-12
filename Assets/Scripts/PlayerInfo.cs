using UnityEngine;
using System.Collections;

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
	[HideInInspector]public DIRECTION direction;

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
