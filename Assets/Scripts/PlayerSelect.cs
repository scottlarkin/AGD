using UnityEngine;
using System.Collections;

public class PlayerSelect : MonoBehaviour {


	public GameObject P1_PressA;
	public GameObject P2_PressA;
	public GameObject P3_PressA;
	public GameObject P4_PressA;

	public GameObject P1_OK;
	public GameObject P2_OK;
	public GameObject P3_OK;
	public GameObject P4_OK;

	public AudioSource SelectSound;

	public PlayerCountContainer pcc;

	private int PlayerCount;
	
	// Use this for initialization
	void Start () {
	
		P1_OK.renderer.enabled = false;
		P2_OK.renderer.enabled = false;
		P3_OK.renderer.enabled = false;
		P4_OK.renderer.enabled = false;

		SelectSound.Pause();
		SelectSound.loop = false;

		PlayerCount = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey("joystick 1 button 0")){

			if(P1_PressA.renderer.enabled == true){
				SelectSound.Play();
				PlayerCount++;
			}

			P1_OK.renderer.enabled = true;
			P1_PressA.renderer.enabled = false;


		}

		if(Input.GetKey("joystick 2 button 0")){

			if(P2_PressA.renderer.enabled == true){
				SelectSound.Play();
				PlayerCount++;
			}

			P2_OK.renderer.enabled = true;
			P2_PressA.renderer.enabled = false;

		}

		if(Input.GetKey("joystick 3 button 0")){

			if(P3_PressA.renderer.enabled == true){
				SelectSound.Play();
				PlayerCount++;
			}

			P3_OK.renderer.enabled = true;
			P3_PressA.renderer.enabled = false;

		}

		if(Input.GetKey("joystick 4 button 0")){

			if(P4_PressA.renderer.enabled == true){
				SelectSound.Play();
				PlayerCount++;
			}

			P4_OK.renderer.enabled = true;
			P4_PressA.renderer.enabled = false;

		}

		for(int i = 1; i < 5; i++){

			if(Input.GetKey("joystick " + i.ToString() + " button 7")){

				if(PlayerCount >= 1){

					//start game
					pcc.PlayerCount = PlayerCount;

					DontDestroyOnLoad(pcc);

					Application.LoadLevel("Toms_Map");

				}
			}
		}
	}
}
