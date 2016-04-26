using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas controlsMenu;
	public GameObject playButton;
	public GameObject controlButton;
	public GameObject exitButton;
	public GameObject yesButton;
	public GameObject noButton;
	public AudioSource titleMusic;
	public GameObject selectSound;
	public GameObject highlightSound;

	private Button playClick;
	private Button controlsClick;
	private Button exitClick;


	private AudioSource sss;
	private AudioSource hss;

	private GameObject selected;

	// Use this for initialization
	void Start () 
	{
	
		quitMenu = quitMenu.GetComponent<Canvas> ();
		playClick = playButton.GetComponent<Button> ();
		controlsClick = controlButton.GetComponent<Button> ();
		exitClick = exitButton.GetComponent<Button> ();
		titleMusic = titleMusic.GetComponent<AudioSource>();
		sss = selectSound.GetComponent<AudioSource>();
		hss = highlightSound.GetComponent<AudioSource>();

		//titleMusic.Play();
		quitMenu.enabled = false;
		controlsMenu.enabled = false;
		yesButton.SetActive(false);
		noButton.SetActive(false);

	}

	public void PlayClick()
	{

		GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>().menuMusicTime = titleMusic.time;
		
		DontDestroyOnLoad(GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>());

		sss.Play ();
		Application.LoadLevel ("Player Select");
		
	}

	public void ExitClick()
	{
		sss.Play ();
		quitMenu.enabled = true;
		playClick.enabled = false;
		controlsClick.enabled = false;
		exitClick.enabled = false;
		yesButton.SetActive(true); //Needed so that the button becomes selectable now.
		noButton.SetActive(true); //Needed so that the button becomes selectable now.
		EventSystem.current.SetSelectedGameObject (noButton);
	
	}

	public void ControlsClick()
	{
		controlsMenu.enabled = true;
		sss.Play ();

	}

	public void YesClick()
	{
		sss.Play ();

		Application.Quit ();
		
	}

	public void NoClick()
	{
		sss.Play ();
		quitMenu.enabled = false;
		playClick.enabled = true;
		controlsClick.enabled = true;
		exitClick.enabled = true;
		yesButton.SetActive(false); //Needed so that the button cannot be selected whilst on main menu.
		noButton.SetActive(false); //Needed so that the button cannot be selected whilst on main menu.

		EventSystem.current.SetSelectedGameObject (playButton);

	}



	// Update is called once per frame
	void Update () {

		if(selected != EventSystem.current.currentSelectedGameObject){

			hss.Play();

			selected = EventSystem.current.currentSelectedGameObject;

		}
	
		if (EventSystem.current.currentSelectedGameObject == selected)
		{
			if (selected != null)
			selected.transform.Rotate( new Vector3(0,0,Mathf.PingPong(Time.time, 0.5f)-0.25f));
		
		}

		if(Input.GetKey("joystick button 1") && controlsMenu.enabled == true)
		{
			NoClick ();
			controlsMenu.enabled = false;
		}


	}
}