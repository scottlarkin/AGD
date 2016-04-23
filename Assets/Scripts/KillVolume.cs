//Kill volume
//if an object containing a playerInfo component collides with this object, it will be killed and destroyed.

//Author: Scott Larkin
//Date:   Jan 2016

using UnityEngine;
using System.Collections;

public class KillVolume : MonoBehaviour {

	void OnTriggerEnter(Collider hit)
	{

		PlayerInfo pi = hit.gameObject.GetComponent<PlayerInfo>();
		
		if(pi != null){

			//update player score here

			pi.alive = false;
			PlayerManager.removePlayer(hit.gameObject);
			GameObject.Destroy(hit.gameObject);
		}

		if(PlayerManager.getPlayers().Count <= 1){

			//1 player left.
			//load new level

			var ll = new LoadLevel();
			Application.LoadLevel(ll.GetRandomLevel());
		
		}

	}
}
