﻿//Scott larkin

//Sets the number of players on the podium, and colours players depending on scores so that the winner is on the highest podium, etc.
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PodiumPlayerSet : MonoBehaviour {

	public GameObject text;

	// Use this for initialization
	void Start () {
	

		
		Dictionary<int, string> d = new Dictionary<int, string>();
		d.Add(1, "ONE");
		d.Add(2, "TWO");
		d.Add(3, "THREE");
		d.Add(4, "FOUR");

		Text t = text.GetComponent<Text>();

		text.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
		text.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
		text.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/4.0f, 175.0f);

		PersistantDataContainer dc = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>();

		t.text = "PLAYER " +  d[dc.scores[0].playerNumber] + " WINS!";

		List<List<Material>> materials = new List<List<Material>>();

		for(int i = 0; i < 4; i++){
			List<Material> m = new List<Material>();
			m.Capacity = 2;
			materials.Add(m);
		}

		//load the materials
		materials[0].Add(Resources.Load ("Orange_Head", typeof(Material)) as Material);
		materials[0].Add(Resources.Load ("Orange_Body", typeof(Material)) as Material);

		materials[1].Add(Resources.Load ("Green_Head", typeof(Material)) as Material);
		materials[1].Add( Resources.Load ("Green_Body", typeof(Material)) as Material);

		materials[2].Add(Resources.Load ("Blue_Head", typeof(Material)) as Material);
		materials[2].Add(Resources.Load ("Blue_Body", typeof(Material)) as Material);
								
		materials[3].Add(Resources.Load ("Pink_Head", typeof(Material)) as Material);
		materials[3].Add(Resources.Load ("Pink_Body", typeof(Material)) as Material);

		//Debug.Log (dc.PlayerCount);

		//hide players if less than 4 playing
		if(dc.PlayerCount < 3){
			GameObject.Find("Character_3rd_Place").renderer.enabled = false;

			if(dc.PlayerCount < 4){
				GameObject.Find("Character_4th_Place").renderer.enabled = false;
				GameObject.Find("Bin").renderer.enabled = false;
			}
		}

		//colour the players sccording to scores.
		GameObject.Find("Character_1st_Place").renderer.materials = materials[dc.scores[0].playerNumber - 1].ToArray();
		GameObject.Find("Character_2nd_Place").renderer.materials = materials[dc.scores[1].playerNumber - 1].ToArray();

		if(dc.PlayerCount > 2)
			GameObject.Find("Character_3rd_Place").renderer.materials = materials[dc.scores[2].playerNumber - 1].ToArray();

		if(dc.PlayerCount > 3)
			GameObject.Find("Character_4th_Place").renderer.materials = materials[dc.scores[3].playerNumber - 1].ToArray();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("joystick 1 button 7") ||
		   Input.GetKey("joystick 2 button 7") ||
		   Input.GetKey("joystick 3 button 7") || 
		   Input.GetKey("joystick 4 button 7")){

			GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().clearAll();
			Application.LoadLevel("Main_Menu");
		}
	}
}
