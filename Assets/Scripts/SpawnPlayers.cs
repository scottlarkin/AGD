//Scott Larkin
//spawn players in the level absed on number of players selected in player select

using UnityEngine;
using System.Collections;

public class SpawnPlayers : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

		PlayerManager.getPlayers().Clear();

		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().time = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().menuMusicTime;

		int count = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().PlayerCount;

		var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

		var x = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().players;

		foreach(string player in x){

			GameObject p = GameObject.Instantiate(Resources.Load("Player " + player ), spawnPoints[int.Parse(player) - 1].transform.position, spawnPoints[int.Parse(player) - 1].transform.rotation) as GameObject;
			
			//p.transform.position = spawnPoints[int.Parse(player) - 1].transform.position;
			
			p.GetComponent<PlayerInfo>().setNumber(player);

			PlayerManager.addPlayer(p);

		}
	}

}
