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
	private List<GameObject> players;
	private int playerCount = 0;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = gameObject.GetComponent<Camera>();

		players = new List<GameObject>();

		for (int i = 1; i < 5; i++) {
			
			try{
				players.Add(GameObject.FindGameObjectWithTag("P_" + i.ToString()));
				playerCount++;
			}
			catch(UnityException e){}

		}
	}

	// Update is called once per frame
	void Update () {
		moveCamera();
	}

	Vector3 getNewCameraPosition(Vector3 midPoint){

		Vector3 pos = new Vector3(0,0, transform.position.z);
		
		pos.x = Mathf.Lerp(transform.position.x, midPoint.x, Time.deltaTime * moveSpeed);
		pos.y = Mathf.Lerp(transform.position.y, midPoint.y, Time.deltaTime * moveSpeed);

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

		foreach (GameObject p in players) {
			try{
				if (p.GetComponent<PlayerInfo>().alive) {

					midPoint += p.transform.position;

					foreach (GameObject p2 in players) {
						
						if (p2.GetComponent<PlayerInfo>().alive) {

							if(p != p2){
								dist = (p.transform.position - p2.transform.position).magnitude;

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
				playerCount--;
				//remove player from list
				players.Remove(p);
				//go back to start of the function, since the collection has changed so the loop is now unreliable
				moveCamera();
				//end execution of this instance of the function
				return;
			}
		}

		midPoint /= playerCount;
			
		//rotate camera
		transform.rotation = getNewCameraRotation(midPoint);

		//move camera x and y pos
		transform.position = getNewCameraPosition(midPoint);

		//clamp range to resonable values
		maxDistance = Mathf.Clamp(maxDistance, 5, 15);

		//"zoom" camera by changine the orthographic size
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxDistance, Time.deltaTime * zoomSpeed);
		
	}
} 



