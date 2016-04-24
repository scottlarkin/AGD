using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour {
	

	private int playerCount;
	private List<Score> orderedScores;


	// Use this for initialization
	void Start () {
		PersistantDataContainer pdc = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>();

		orderedScores = pdc.OrderScores ();

		orderScoreboard ();

	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	void orderScoreboard()
	{
		for (int i = 0; i < orderedScores.Count; i++)
		{

			GameObject scorePanel = GameObject.Instantiate(Resources.Load ("Score P" + orderedScores[i].playerNumber.ToString())) as GameObject;


			Vector2 panelPos = new Vector2(950, -(((i + 1) * 280) - (i * 60))); //Magic numbers everywhere
			scorePanel.transform.SetParent(this.gameObject.transform, false);
			scorePanel.GetComponent<RectTransform>().anchorMin = new Vector2(0,1); //Sets the anchors for the UI
			scorePanel.GetComponent<RectTransform>().anchorMax = new Vector2(0,1);
			scorePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(1556, 205); //Needed so that the width/height doesn't change with the anchors.
			scorePanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(panelPos.x, panelPos.y, 0);

			Text scoreVal = scorePanel.transform.Find("Value").GetComponent<Text>();
			scoreVal.text = orderedScores[i].score.ToString();
		}

	}
}
