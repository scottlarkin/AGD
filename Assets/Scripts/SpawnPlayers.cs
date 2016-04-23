using UnityEngine;
using System.Collections;

public class SpawnPlayers : MonoBehaviour {


	// Use this for initialization
	void Start () {

		int count = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().PlayerCount;

		var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

		for(int i = 1; i < count + 1; i++){

			GameObject p = GameObject.Instantiate(Resources.Load("Player " + i.ToString())) as GameObject;

			p.transform.position = spawnPoints[i-1].transform.position;

			PlayerManager.addPlayer(p);

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
