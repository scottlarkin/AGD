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

	private CooldownTimer cd;

	// Use this for initialization
	void Start () {
		cd = new CooldownTimer(1, false);

	}
	
	// Update is called once per frame
	void Update () {

		GameObject target;
		float minDist = float.PositiveInfinity;
		float dist;

		foreach(GameObject p in PlayerManager.getPlayers()){

			dist = (p.transform.position - this.transform.position).magnitude;

			if(dist < minDist){
				minDist = dist;
				target = p;
			}
		}

		if(cd.checkCooldownOver()){
			fireProjectile();
		}
	}

	void fireProjectile(){

		//GameObject.Instantiate(projectile, this.transform.position, this.transform.rotation);
		cd.startCooldown();
	}
}



