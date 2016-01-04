using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
	
	public float rotationSpeed=2;

	private Vector3 midPoint;

	private List<GameObject> players;

	// Use this for initialization
	void Start () {
		players = new List<GameObject>();

		for (int i = 1; i < 5; i++) {
			
			try{
				players.Add(GameObject.FindGameObjectWithTag("P_" + i.ToString()));

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
		foreach (GameObject p in players) {

			if (p.GetComponent<PlayerInfo>().isAlive()) {
				midPoint += p.transform.position;
			}

		}
		
		midPoint /= 4;
				
		Vector3 Direction = midPoint - transform.position;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Direction), Time.deltaTime * rotationSpeed);
		
	}
} 



