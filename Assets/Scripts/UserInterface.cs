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

		PersistantDataContainer pdc = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>();
		players = PlayerManager.getPlayers (); //get a list of players
		uiObjects  = new List<GameObject>(); //create a new list of gameobjects
		playersInfo  = new List<PlayerInfo>(); //create a new list of playerinfos
		noOfPlayers = pdc.PlayerCount;
		
		float screenDiv = Screen.width / noOfPlayers;
		Debug.Log ("ScreenDiv = " + screenDiv);
		
		uiObjects.Clear ();
		for (int i = 0; i < noOfPlayers; i++) //for all players
		{

			PlayerInfo pInfo = players[i].GetComponent<PlayerInfo>();
			playersInfo.Add(pInfo);
			
			GameObject playerText = Instantiate(DamageCounter) as GameObject;
			playerText.transform.SetParent(UI.transform, false);
			
			RectTransform uiPos = playerText.GetComponent<RectTransform>();
			Text uiText = playerText.GetComponent<Text>();
			
			uiPos.sizeDelta.Set(Screen.width, Screen.height);
			uiPos.anchorMin = new Vector2(0, 0); //set the anchors in which the the position is related to. This is currently bottom left
			uiPos.anchorMax = new Vector2(0, 0);
			//uiPos.anchoredPosition = new Vector2((i * (screenDiv + uiPos.rect.width)), dmgYPos);
			uiPos.anchoredPosition = new Vector2((float)((screenDiv * (i + 0.5)) - (uiPos.rect.width / 2)) + 160 , dmgYPos);
			uiText.color = colors[int.Parse (GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().players[i])-1]; //The correct colour appears on the ui dependent on the selected players
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
