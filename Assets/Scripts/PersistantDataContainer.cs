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
