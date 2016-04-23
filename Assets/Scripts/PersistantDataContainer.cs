using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistantDataContainer : MonoBehaviour {
	
	public int PlayerCount;
	public List<int> scores;

	public PersistantDataContainer(){

		scores = new List<int>();

		for(int i = 0; i < PlayerManager.maxPlayerCount; i++){
			scores.Add(0);
		}
	}

}
