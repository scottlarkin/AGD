//Hanging Basket / Falling Object
//Environmental hazard
//When a player walks underneath or above this hazard it will drop after a short delay, stunning any player hit.

using UnityEngine;
using System.Collections;

public class HangingBasket : MonoBehaviour {

	public float delayTime = 3;
	public float respawnTime = 5;
	public bool stun = true;
	public float stunDuration = 2;
	public float range = 5;
	public float dropSpeed = 20.0f;
	public bool detectDown = false;

	Vector3 startPosition;
	CooldownTimer dropDelay;
	CooldownTimer respawnTimer;
	RaycastHit hitInfo;
	bool dropDelayStarted = false;
	bool active = false;
	Vector3 objPos;
	Vector3 objWidth;
	int lookDown;

	// Use this for initialization
	void Start () {
	
		startPosition = transform.position;
		dropDelay = new CooldownTimer(delayTime, false);
		respawnTimer = new CooldownTimer(respawnTime, false);
		hitInfo = new RaycastHit();
		objWidth = renderer.bounds.size;

		//Simple method of deciding whether to detect up or down. Could be enum, but prob overkill.
		lookDown = detectDown ? -1 : 1;

	}
	
	// Update is called once per frame
	void Update () {
		objPos = transform.position;

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
		//Debug.DrawLine(transform.position, transform.position + new Vector3(0,-range, 0));
		Debug.DrawLine (objPos - (objWidth/ 2),objPos + new Vector3 (0, (lookDown * range), 0));
		Debug.DrawLine (objPos, objPos + new Vector3 (0, (lookDown * range), 0));
		Debug.DrawLine (objPos + (objWidth/ 2), objPos + new Vector3 (0, (lookDown * range), 0));
	}
	
	void detectPlayer(){

		Vector3 detectRange = new Vector3 (0, (lookDown * range), 0);

		if((Physics.Linecast(objPos - (objWidth/ 2), objPos + detectRange, out hitInfo, LayerMask.NameToLayer("players"))
		    || Physics.Linecast(objPos, objPos + detectRange, out hitInfo, LayerMask.NameToLayer("players"))
		    || Physics.Linecast(objPos + (objWidth/ 2), objPos + detectRange, out hitInfo, LayerMask.NameToLayer("players"))))
		   {

			PlayerInfo pi = hitInfo.transform.gameObject.GetComponent<PlayerInfo>();
			
			if(pi != null){
				dropDelayStarted = true;
				dropDelay.startCooldown();
			}
		}
	}

	void OnTriggerEnter(Collider hit)
	{
			PlayerInfo pi = hit.gameObject.GetComponent<PlayerInfo> ();

			if (pi != null) 
		{
				
				if (stun == true)
				{	
					pi.stunPlayer (stunDuration);
				}
			}
		}
}

