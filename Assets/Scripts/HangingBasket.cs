//Hanging Basket
//Environmental hazard
//When a player walks underneath this hazard it will drop after a short delay, stunning any player hit.

using UnityEngine;
using System.Collections;

public class HangingBasket : MonoBehaviour {

	public float delayTime = 3;
	public float respawnTime = 15;
	public float stunDuration = 2;
	public float range = 5;

	CooldownTimer dropDelay;
	CooldownTimer respawnTimer;
	RaycastHit hitInfo;
	bool dropDelayStarted = false;
	bool active = true;

	// Use this for initialization
	void Start () {
	
		dropDelay = new CooldownTimer(delayTime, false);
		respawnTimer = new CooldownTimer(respawnTime, false);
		hitInfo = new RaycastHit();

	}
	
	// Update is called once per frame
	void Update () {

		if(dropDelay.checkCooldownOver()){
			dropBasket();
			dropDelayStarted = false;
			respawnTimer.startCooldown();
			active = false;
			this.renderer.enabled = active;
		}

		if(respawnTimer.checkCooldownOver()){
			active = true;
			this.renderer.enabled = active;
		}

		if(active){
			detectPlayer();
		}


		transform.Rotate( new Vector3(0,0,Mathf.Sin(Time.deltaTime) * 500));

		//visualise the drop trigger in debug mode
		Debug.DrawLine(transform.position, transform.position + new Vector3(0,-range, 0));

	}

	void dropBasket(){

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
}

