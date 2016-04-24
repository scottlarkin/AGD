//Player Manager
//script to keep track of the players
//central location for storing list of players, use this any time you need to iterate over all players.

//Author: Scott larkin
//Date:   Jan 2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerManager{

	public static int maxPlayerCount = 4;
	private static List<GameObject> players;
	private static bool initialised = false;
	
	public static void Init(){

		if(initialised)
			return;

		players = new List<GameObject>();

		findPlayers();

		initialised = true;

	}

	public static void findPlayers(){

	
		//players.Clear();

		//for (int i = 1; i < maxPlayerCount + 1; i++) {
		//	
		//	GameObject o = GameObject.FindGameObjectWithTag("P_" + i.ToString());
		//
		//	if(o != null){
		//		Debug.Log(o.name + "   " + i);
		//		players.Add(o);
		//	}
		//
		//}
	}

	private static void checkInit(){
		if(!initialised)
			Init();
	}

	public static void addPlayer(GameObject o){
		checkInit();
		players.Add(o);
	}

	public static void removePlayer(GameObject o){
		checkInit();
		players.Remove(o);

		PersistantDataContainer dc = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>();

		if(PlayerManager.getPlayers().Count == 1){
			
			//DontDestroyOnLoad(dc);
			
			if(PlayerManager.getPlayers().Count != 0){
				
				foreach(Score s in dc.scores){
					
					//increase score of the last player standing
					if(s.playerNumber == int.Parse(PlayerManager.getPlayers()[0].GetComponent<PlayerInfo>().playerNumber)){
						s.score += 1;
						break;
					}
				}
			}
			
			dc.OrderScores();
			
			var ll = new LoadLevel();
			
			foreach(var score in dc.scores){
				
				//Debug.Log ("player:  " + score.playerNumber + "   score:  " + score.score);
				
				if(score.score == 5){
					Application.LoadLevel("Podiums_Set_02");
					return;
				}
				
			}
			
			Application.LoadLevel(ll.GetRandomLevel());
			
		}
	}

	public static List<GameObject> getPlayers(){
		checkInit();
		return players;
	}

}
