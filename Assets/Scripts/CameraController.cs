//Camera controller
//Script to automate the movement of the main camera.
//Aims to keep all players on the screen.
//Camera will move, zoom, and rotate according to the players positions.

//Author: Scott Larkin
//Date:   Jan 2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
	
	public float rotationSpeed=1;
	public float moveSpeed=1;
	public float zoomSpeed=1;

	private Vector3 midPoint;
	private float maxDistance;

	private int playerCount = 0;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = gameObject.GetComponent<Camera>();

		PlayerManager.Init();
	}

	// Update is called once per frame
	void Update () {
		moveCamera();
	}

	Vector3 getNewCameraPosition(Vector3 midPoint){

		Vector3 pos = new Vector3(0,0, transform.position.z);
		
		pos.x = Mathf.Lerp(transform.position.x, midPoint.x, Time.deltaTime * moveSpeed);
		pos.y = Mathf.Lerp(transform.position.y, midPoint.y, Time.deltaTime * moveSpeed) + 0.15f;

		return pos;
	}

	Quaternion getNewCameraRotation(Vector3 midPoint){

		Vector3 direction = midPoint - transform.position;

		return Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * rotationSpeed);
	}

	void moveCamera() {

		midPoint = new Vector3 (0, 0, 0);
		maxDistance = float.NegativeInfinity;
		float dist;
		List<GameObject> players = PlayerManager.getPlayers();


		foreach (GameObject p in players) {
			try{
				if (p.GetComponent<PlayerInfo>().alive) {

					midPoint += p.transform.FindChild("Center").transform.position;

					foreach (GameObject p2 in players) {

						if (p2.GetComponent<PlayerInfo>().alive) {

							if(p != p2){
								dist = (p.transform.FindChild("Center").transform.position - p2.transform.FindChild("Center").transform.position).magnitude;

								if(dist > maxDistance){
									maxDistance = dist;
								}
							}
						}
					}
				}
			}
			//handle a player being deleted
			catch(MissingReferenceException e){

				//remove player from list
				PlayerManager.removePlayer(p);
				//go back to start of the function, since the collection has changed so the loop is now unreliable
				moveCamera();
				//end execution of this instance of the function
				return;
			}
		}
		
		midPoint /= players.Count;
			
		//rotate camera
		transform.rotation = getNewCameraRotation(midPoint);
	
		//move camera x and y pos
		transform.position = getNewCameraPosition(midPoint);
	
		//clamp range to resonable values
		maxDistance = Mathf.Clamp(maxDistance, 10, 25);
	
		//"zoom" camera by changine the orthographic size
		//cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxDistance, Time.deltaTime * zoomSpeed);
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxDistance * 2.0f, Time.deltaTime * zoomSpeed);
		
	}
} 



