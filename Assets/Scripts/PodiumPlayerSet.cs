//Scott larkin

//Sets the number of players on the podium, and colours players depending on scores so that the winner is on the highest podium, etc.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PodiumPlayerSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		PersistantDataContainer dc = GameObject.Find("PersistantDataContainer").GetComponent<PersistantDataContainer>();

		List<List<Material>> materials = new List<List<Material>>();

		for(int i = 0; i < 4; i++){
			List<Material> m = new List<Material>();
			m.Capacity = 2;
			materials.Add(m);
		}

		materials[0].Add(Resources.Load ("Orange_Head", typeof(Material)) as Material);
		materials[0].Add(Resources.Load ("Orange_Body", typeof(Material)) as Material);

		materials[1].Add(Resources.Load ("Green_Head", typeof(Material)) as Material);
		materials[1].Add( Resources.Load ("Green_Body", typeof(Material)) as Material);

		materials[2].Add(Resources.Load ("Blue_Head", typeof(Material)) as Material);
		materials[2].Add(Resources.Load ("Blue_Body", typeof(Material)) as Material);
								
		materials[3].Add(Resources.Load ("Pink_Head", typeof(Material)) as Material);
		materials[3].Add(Resources.Load ("Pink_Body", typeof(Material)) as Material);

		Debug.Log (dc.PlayerCount);
		if(dc.PlayerCount < 3){
			GameObject.Find("Character_3rd_Place").renderer.enabled = false;

			if(dc.PlayerCount < 4){
				GameObject.Find("Character_4th_Place").renderer.enabled = false;
			}
		}

		GameObject.Find("Character_1st_Place").renderer.materials = materials[dc.scores[0].playerNumber - 1].ToArray();
		GameObject.Find("Character_2nd_Place").renderer.materials = materials[dc.scores[1].playerNumber - 1].ToArray();
		GameObject.Find("Character_3rd_Place").renderer.materials = materials[dc.scores[2].playerNumber - 1].ToArray();
		GameObject.Find("Character_4th_Place").renderer.materials = materials[dc.scores[3].playerNumber - 1].ToArray();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
