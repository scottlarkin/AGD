using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistantDataContainer : MonoBehaviour {
	
	public int PlayerCount;
	public List<Score> scores;

	public PersistantDataContainer(){

		scores = new List<Score>();
	
	}

	public void OrderScores(){
		scores.Sort((x,y)=>y.score.CompareTo(x.score));
	}

	public void AddPlayer(){
		PlayerCount++;
		scores.Add(new Score(PlayerCount));
	}

}


public class Score{

	public Score(int playerNum){
		playerNumber = playerNum;
	}

	public int playerNumber;
	public int score;
}
