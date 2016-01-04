using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
	
	public float rotationSpeed=2;

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
	
	
	void moveCamera() {

		midPoint = new Vector3 (0, 0, 0);
		maxDistance = float.NegativeInfinity;
		float dist;

		foreach (GameObject p in players) {

			if (p.GetComponent<PlayerInfo>().isAlive()) {

				midPoint += p.transform.position;

				foreach (GameObject p2 in players) {
					
					if (p2.GetComponent<PlayerInfo>().isAlive()) {

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

		midPoint /= playerCount;
				
		Vector3 direction = midPoint - transform.position;
		Vector3 pos = new Vector3(0,0, transform.position.z);

		pos.x = Mathf.Lerp(transform.position.x, midPoint.x, Time.deltaTime);
		pos.y = Mathf.Lerp(transform.position.y, midPoint.y, Time.deltaTime);

		//rotate camera
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime);

		//move camera x and y pos
		transform.position = pos;

		//clamp range to resonable values
		maxDistance = Mathf.Clamp(maxDistance, 5, 15);

		//"zoom" camera by changine the orthographic size
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxDistance, Time.deltaTime * 1);
		
	}
} 



