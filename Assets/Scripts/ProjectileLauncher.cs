//Projectile Launcher
//find the nearest player.
//instantiate a projectile.
//fire in direction of player.

//Author: Scott Larkin
//Date:   Jan 2016

using UnityEngine;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour {


	public GameObject projectile;
	public float range = 500;
	public float cooldown = 15;
	private CooldownTimer cd;

	// Use this for initialization
	void Start () {
		cd = new CooldownTimer(cooldown, true);

	}
	
	// Update is called once per frame
	void Update () {

		float minDist = float.PositiveInfinity;
		float dist;
		Vector3 vecToTarget = new Vector3();

		foreach(GameObject p in PlayerManager.getPlayers()){

			dist = (p.transform.FindChild("Center").transform.position - this.transform.position).magnitude;

			if(dist < minDist){
				minDist = dist;
				vecToTarget = p.transform.FindChild("Center").transform.position - this.transform.position;
			}
		}
	
		//fire projectile if in range and cooldown has elapsed
		if(minDist <= range && cd.checkCooldownOver()){
			fireProjectile(vecToTarget.normalized);
		}
	}

	void fireProjectile(Vector3 direction){

		GameObject o = (GameObject)GameObject.Instantiate(projectile, this.transform.position, this.transform.rotation);

		o.GetComponent<LinearProjectile>().setDir(direction);
		o.GetComponent<LinearProjectile>().setRange(range);

		cd.startCooldown();
	}
}



