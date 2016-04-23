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
			PlayerManager.removePlayer(hit.gameObject);
			GameObject.Destroy(hit.gameObject);
		}

		if(PlayerManager.getPlayers().Count <= 1){

			DontDestroyOnLoad(GameObject.Find("PersistantDataContainer"));

			if(PlayerManager.getPlayers().Count != 0)
				GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().scores[int.Parse(PlayerManager.getPlayers()[0].GetComponent<PlayerInfo>().playerNumber) - 1] += 1;

			var ll = new LoadLevel();

			foreach(var score in GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().scores){

				if(score == 5){
					Application.LoadLevel("Podiums_Set_02");
					return;
				}

			}

			Application.LoadLevel(ll.GetRandomLevel());
		
		}
	}
}
