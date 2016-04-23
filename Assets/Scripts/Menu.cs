using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	public Canvas quitMenu;
	public GameObject playButton;
	public GameObject exitButton;
	public GameObject yesButton;
	public GameObject noButton;
	public AudioSource titleMusic;
	public AudioSource selectSound;
	public AudioSource highlightSound;

	private Button playClick;
	private Button exitClick;

	// Use this for initialization
	void Start () 
	{
	
		quitMenu = quitMenu.GetComponent<Canvas> ();
		playClick = playButton.GetComponent<Button> ();
		exitClick = exitButton.GetComponent<Button> ();
		titleMusic = titleMusic.GetComponent<AudioSource>();
		selectSound = selectSound.GetComponent<AudioSource>();
		highlightSound = highlightSound.GetComponent<AudioSource>();

		titleMusic.Play();
		quitMenu.enabled = false;
		yesButton.SetActive(false);
		noButton.SetActive(false);

	}

	public void PlayClick()
	{
		selectSound.Play ();
		Application.LoadLevel (1);
		
	}

	public void ExitClick()
	{
		selectSound.Play ();
		quitMenu.enabled = true;
		playClick.enabled = false;
		exitClick.enabled = false;
		yesButton.SetActive(true); //Needed so that the button becomes selectable now.
		noButton.SetActive(true); //Needed so that the button becomes selectable now.
		EventSystem.current.SetSelectedGameObject (noButton);
	
	}

	public void YesClick()
	{
		selectSound.Play ();
		Application.Quit ();
		
	}

	public void NoClick()
	{
		selectSound.Play ();
		quitMenu.enabled = false;
		playClick.enabled = true;
		exitClick.enabled = true;
		yesButton.SetActive(false); //Needed so that the button cannot be selected whilst on main menu.
		noButton.SetActive(false); //Needed so that the button cannot be selected whilst on main menu.

		EventSystem.current.SetSelectedGameObject (playButton);

	}



	// Update is called once per frame
	void Update () {


		GameObject selected = EventSystem.current.currentSelectedGameObject;
	
		if (EventSystem.current.currentSelectedGameObject == selected)
		{
			selected.transform.Rotate( new Vector3(0,0,Mathf.PingPong(Time.time, 0.5f)-0.25f));
		
		}


	}
}