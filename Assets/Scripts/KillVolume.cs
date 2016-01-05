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
			pi.alive = false;
			GameObject.Destroy(hit.gameObject);
		}

	}
}
