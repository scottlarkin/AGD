//simple linear projectile which will stun any player it comes into contact with

//Author: Scott Larkin
//Date:   Jan 2016

using UnityEngine;
using System.Collections;

public class LinearProjectile : MonoBehaviour {

	public float stunDuration = 15;
	public float speed = 5;
	private Vector3 direction;
	private float range = 0;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += direction * speed * Time.deltaTime;

		//destroy when the object goes out of range
		if((startPos - transform.position).magnitude > range)
			GameObject.Destroy(gameObject);
	}

	public void setDir(Vector3 dir){
		direction = dir;
	}

	public void setRange(float r){
		range = r;
	}

	void OnTriggerEnter(Collider hit)
	{

		PlayerInfo pi = hit.gameObject.GetComponent<PlayerInfo>();

		if(pi != null){

			//if hits a player, stun, and destroy object
			hit.gameObject.GetComponent<PlayerInfo>().stunPlayer(stunDuration);		
			GameObject.Destroy(gameObject);
		}
		
	}
}
