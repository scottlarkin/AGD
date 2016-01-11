using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class UserInterface : MonoBehaviour {
	
	private static List<GameObject> players; 
	public Canvas UI;
	public GameObject DamageCounter;

	// Use this for initialization
	void Start () {


		//addTextObject (50, 0);
		
		//UI = GameObject.Find ("In Game UI");
		GameObject[] counters;
		players = PlayerManager.getPlayers ();

		for (int i = 1; i < players.Count; i++) 
		{

			GameObject playerText = Instantiate(DamageCounter) as GameObject;
			playerText.transform.SetParent(UI.transform, false);
			counters[i] = playerText;
			//addTextObject (0,0);
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void addTextObject(float posX, float posY)
	{
		/*GameObject textObject = new GameObject ("Text");
		textObject.transform.SetParent (UI.transform);

		RectTransform trans = textObject.AddComponent<RectTransform> ();
		trans.sizeDelta (1920, 1080);

		Text objName = UI.gameObject.AddComponent<Text> ();
		//objName.p
		objName.rectTransform.position.Set (posX, posY, 0);
		objName.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		objName.fontSize = 50;
		objName.text = "Hello";*/
		//objName.rectTransform.position.y = posY;
	}
}
