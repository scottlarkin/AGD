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
		PersistantDataContainer dc = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>();

		if(pi != null){

			pi.alive = false;
			PlayerManager.removePlayer(hit.gameObject);
			GameObject.Destroy(hit.gameObject);
		}

		if(PlayerManager.getPlayers().Count == 1){

			DontDestroyOnLoad(dc);

			if(PlayerManager.getPlayers().Count != 0){

				foreach(Score s in dc.scores){

					if(s.playerNumber == int.Parse(PlayerManager.getPlayers()[0].GetComponent<PlayerInfo>().playerNumber)){
						s.score += 5;
						break;
					}
				}

			}

			dc.OrderScores();

			var ll = new LoadLevel();

			foreach(var score in dc.scores){
				Debug.Log ("player:  " + score.playerNumber + "   score:  " + score.score);
				if(score.score == 5){
					Application.LoadLevel("Podiums_Set_02");
					return;
				}

			}

			Application.LoadLevel(ll.GetRandomLevel());
		
		}
	}
}
