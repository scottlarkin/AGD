﻿//Player Manager
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

		players = new List<GameObject>();

		findPlayers();

		initialised = true;

	}

	private static void findPlayers(){
		for (int i = 1; i < maxPlayerCount + 1; i++) {
			
			try{
				players.Add(GameObject.FindGameObjectWithTag("P_" + i.ToString()));
			}
			catch(UnityException e){}
			
		}
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
	}

	public static List<GameObject> getPlayers(){
		checkInit();
		return players;
	}

}
