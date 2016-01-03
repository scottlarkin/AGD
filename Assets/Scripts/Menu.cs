using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Menu : MonoBehaviour {

	public Canvas quitMenu;
	public Button playGame;
	public Button exitGame;
	public GameObject playButton;
	public GameObject yesButton;

	// Use this for initialization
	void Start () 
	{
	
		quitMenu = quitMenu.GetComponent<Canvas> ();
		playGame = playGame.GetComponent<Button> ();
		exitGame = exitGame.GetComponent<Button> ();

		quitMenu.enabled = false;
	}

	public void PlayClick()
	{
		Application.LoadLevel (1);
		
	}

	public void ExitClick()
	{
		quitMenu.enabled = true;
		playGame.enabled = false;
		exitGame.enabled = false;
		EventSystem.current.SetSelectedGameObject (yesButton);
	
	}

	public void YesClick()
	{
		Application.Quit ();
		
	}

	public void NoClick()
	{
		quitMenu.enabled = false;
		playGame.enabled = true;
		exitGame.enabled = true;

		EventSystem.current.SetSelectedGameObject (playButton);

	}



	// Update is called once per frame
	void Update () {
	
	}
}
