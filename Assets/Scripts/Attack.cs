using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {


	public float range = 0.5f;
	public float coolDown = 5.0f;
	public Transform rayOrigin, attackRange;
	public bool applyAttack = false;
	public float pushPower = 5.0f;

	private PlayerInfo pi;
	private Vector3 attackVec;
	private float lastAttackTime;


	private struct attackParams
	{
		public attackParams(float f, Vector3 d){force = f; direction = d;}
		public float force;
		public Vector3 direction;
	}


	// Use this for initialization
	void Start () 
	{

		pi = gameObject.GetComponent<PlayerInfo>();

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		lastAttackTime = -coolDown;

	
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (Input.GetKey("joystick "+pi.playerNumber+" button 2"))
		{
			if (CanAttack())
			{
				PushAttack();
			}
		}
			
	}

	bool CanAttack ()
	{
		float timer;

		timer = Time.time;

		if  (lastAttackTime + coolDown <= timer) 
		{
			return true;
		}

		return false;

	}
	void PushAttack()
	{
		attackVec = attackRange.transform.position - rayOrigin.transform.position;
		attackVec = new Vector3 ((pi.GetDirection() * attackVec.x), attackVec.y, attackVec.z);
		lastAttackTime = Time.time;
		//Debug.Log ("You are attacking");
		Debug.DrawLine (rayOrigin.transform.position,rayOrigin.transform.position + attackVec, Color.green);

	
		int mask = LayerMask.NameToLayer("Player");
		RaycastHit objectHit;


		if (Physics.Linecast (rayOrigin.transform.position, rayOrigin.transform.position + attackVec, out objectHit))
		{
			Debug.Log (objectHit.collider.name);
			if(!objectHit.collider.gameObject.CompareTag(this.tag))
			{

				objectHit.collider.SendMessage("ApplyForce", new attackParams(pushPower, Vector3.Normalize(attackVec)) , SendMessageOptions.DontRequireReceiver);
			}
			
		}

	}
	
	void ApplyForce(attackParams ap)
	{

		//this.rigidbody.AddForce (new Vector3 (50, 10, 0) * amount);
		//this.rigidbody.velocity = new Vector3 (10, liftVec, 0) * amount; 
		this.rigidbody.velocity = ap.direction * ap.force; 
	}
}
