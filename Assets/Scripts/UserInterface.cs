using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class UserInterface : MonoBehaviour {

	public Canvas UI;
	public GameObject DamageCounter;
	public Color[] colors = new Color[4];
	public float dmgYPos = 75;

	private static List<GameObject> players;
	private List<GameObject> uiObjects;
	private List<PlayerInfo> playersInfo;
	private int noOfPlayers;
	// Use this for initialization
	void Start () 
	{
		CreateUI ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateUI ();
	}
	
	void CreateUI()
	{
		
		players = PlayerManager.getPlayers (); //get a list of players
		uiObjects  = new List<GameObject>(); //create a new list of gameobjects
		playersInfo  = new List<PlayerInfo>(); //create a new list of playerinfos
		noOfPlayers = players.Count;

		float screenHalf = Screen.width / 2; // to be used for ui position offsets
		float screenDiv = Screen.width / noOfPlayers;
		Debug.Log ("ScreenDiv = " + screenDiv);
		Debug.Log ("ScreenHalf = " + screenHalf);
		
		for (int i = 1; i < noOfPlayers + 1; i++) //for all players, starting at 1 as i = 1 = player 1.
		{
			PlayerInfo pInfo = players[i - 1].GetComponent<PlayerInfo>();
			playersInfo.Add(pInfo);

			GameObject playerText = Instantiate(DamageCounter) as GameObject;
			playerText.transform.SetParent(UI.transform, false);

			RectTransform uiPos = playerText.GetComponent<RectTransform>();
			Text uiText = playerText.GetComponent<Text>();

			uiPos.sizeDelta.Set(Screen.width, Screen.height);
			uiPos.anchorMin = new Vector2(0, 0); //set the anchors in which the the position is related to. This is currently bottom left
			uiPos.anchorMax = new Vector2(0, 0);
			uiPos.anchoredPosition = new Vector2((i * (screenDiv + uiPos.rect.width -250)), dmgYPos); //-250 offset to be reviewed. For which resolution to be used.

			uiText.color = colors[i - 1];
			Debug.Log ("P" + i + ": =" + ((i * (screenDiv))));
			
			uiObjects.Add(playerText);
		}
	}

	void UpdateUI()
	{
		for (int i = 0; i < noOfPlayers; i++) //for all players
		{
			Text damageTaken = uiObjects[i].GetComponent<Text>(); //get the text component for each of the players
			
			if (!playersInfo[i].alive)
			{
				damageTaken.text = ("K.O!");
				continue;
			}

			string playerDmg = playersInfo[i].damageReceived.ToString(); // conver the player's damage to string
			damageTaken.text = (playerDmg + "%"); // add to the UI

		}

	}
}
