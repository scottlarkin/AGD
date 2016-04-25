//Kill volume
//if an object containing a playerInfo component collides with this object, it will be killed and destroyed.

//Author: Scott Larkin
//Date:   Jan 2016

using UnityEngine;
using System.Collections;



public class KillVolume : MonoBehaviour {

	public bool water;


	void OnTriggerEnter(Collider hit)
	{

		PlayerInfo pi = hit.gameObject.GetComponent<PlayerInfo>();

		if(pi != null){
			pi.alive = false;

			if(water){

				GameObject waterSplash = GameObject.Instantiate(Resources.Load("WaterSplash")) as GameObject;
				AudioSource audio = waterSplash.GetComponent<AudioSource>();
				audio.volume = 1000;
				audio.Play();

				waterSplash.transform.position = pi.transform.position;

			}

			PlayerManager.removePlayer(hit.gameObject);
			GameObject.Destroy(hit.gameObject);
		}

	}
}
