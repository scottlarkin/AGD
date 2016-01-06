//Hanging Basket
//Environmental hazard
//When a player walks underneath this hazard it will drop after a short delay, stunning any player hit.

using UnityEngine;
using System.Collections;

public class HangingBasket : MonoBehaviour {

	public float delayTime = 3;
	public float respawnTime = 5;
	public float stunDuration = 2;
	public float range = 5;
	public float dropSpeed = 20.0f;

	Vector3 startPosition;
	CooldownTimer dropDelay;
	CooldownTimer respawnTimer;
	RaycastHit hitInfo;
	bool dropDelayStarted = false;
	bool active = false;

	// Use this for initialization
	void Start () {
	
		startPosition = transform.position;
		dropDelay = new CooldownTimer(delayTime, false);
		respawnTimer = new CooldownTimer(respawnTime, false);
		hitInfo = new RaycastHit();

	}
	
	// Update is called once per frame
	void Update () {

		if(dropDelay.checkCooldownOver() && !active){
			dropDelay.stop ();
			dropDelayStarted = false;
			respawnTimer.startCooldown();
			active = true;
		}

		if(respawnTimer.checkCooldownOver()){
			respawnTimer.stop ();
			active = false;
			transform.position = startPosition;
		}

		if(!active && !dropDelayStarted){
			detectPlayer();
		}
		else{
			if(active){
				transform.position = transform.position + new Vector3(0, -Time.deltaTime * dropSpeed, 0);
			}
		}

		if(dropDelayStarted){
			transform.Rotate( new Vector3(0,0,Mathf.PingPong(Time.time, 0.5f)-0.25f));
		}

		//visualise the drop trigger in debug mode
		Debug.DrawLine(transform.position, transform.position + new Vector3(0,-range, 0));

	}
	
	void detectPlayer(){

		if(Physics.Raycast(transform.position, new Vector3(0,-1,0), out hitInfo, range)){

			PlayerInfo pi = hitInfo.transform.gameObject.GetComponent<PlayerInfo>();
			
			if(pi != null){
				dropDelayStarted = true;
				dropDelay.startCooldown();
			}
		}
	}

	void OnTriggerEnter(Collider hit)
	{
		
		PlayerInfo pi = hit.gameObject.GetComponent<PlayerInfo>();
		
		if(pi != null){
			pi.stunPlayer(stunDuration);
		}
		
	}

}

