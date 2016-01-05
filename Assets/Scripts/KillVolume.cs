using UnityEngine;
using System.Collections;

public class KillVolume : MonoBehaviour {

	void OnTriggerEnter(Collider hit)
	{
		try
		{
			hit.gameObject.GetComponent<PlayerInfo>().alive = false;
			GameObject.Destroy(hit.gameObject);

		}
		catch(UnityException e)
		{
		}
	}



}
