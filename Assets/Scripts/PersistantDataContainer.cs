//Scott Larkin
//Store player and score data which needs to be kept between scene changes. 
//Theis script is attachejd to a game object of the same name.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistantDataContainer : MonoBehaviour {
	
	public int PlayerCount;
	public List<Score> scores;
	public float menuMusicTime;

	public List<string> players;

	public PersistantDataContainer(){

		scores = new List<Score>();
		players = new List<string>();
	
	}

	public List<Score> OrderScores(){
		scores.Sort((x,y)=>y.score.CompareTo(x.score));

		return scores;
	}

	public void AddPlayer(string playerNumber){
		PlayerCount++;
		players.Add(playerNumber);
		players.Sort((x,y)=> int.Parse(x).CompareTo(int.Parse(y))); //Sorted so that players always go in order. i.e. useful for looping through players
		scores.Add(new Score(int.Parse (playerNumber)));
	}

	public void clearAll()
	{
		PlayerCount = 0;
		players.Clear();
		scores.Clear();
	}

}


public class Score{

	public Score(int playerNum){
		playerNumber = playerNum;
	}

	public int playerNumber;
	public int score;
}
