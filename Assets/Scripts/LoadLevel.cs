using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadLevel{
	List<string> levels;
	
	public LoadLevel() {

		levels = new List<string>();
		levels.Add("Toms_Map");
		//levels.Add("Callums_Map");

	}

	public string GetRandomLevel(){

		int r = Random.Range(0, levels.Count);
		//Debug.Log(r);
		return levels[r];
	}

}
