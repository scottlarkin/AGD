using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {


	public float range = 0.5f;
	public float coolDown = 5.0f;
	public Transform rayOrigin, attackRange;
	public float pushPower = 5.0f;
	public float counterPower = 7.50f;

	private PlayerInfo pi;
	private PlayerInfo otherPlayer;
	private Vector3 attackVec, counterVec;
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

		if (Input.GetKey("joystick " + pi.playerNumber + " button 1")) {
			CounterAttack ();

		}
			pi.isBlocking = false;
			
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
				otherPlayer = objectHit.collider.gameObject.GetComponent<PlayerInfo>();
				if (otherPlayer.isBlocking)
				{
					ApplyForce (new attackParams(counterPower, Vector3.Normalize(attackVec * -pi.GetDirection())));
				}
				else
				{
				objectHit.collider.SendMessage("ApplyForce", new attackParams(pushPower, Vector3.Normalize(attackVec)) , SendMessageOptions.DontRequireReceiver);
			
				}
			}	
		}

	}

	void CounterAttack()
	{
		pi.isBlocking = true;
		counterVec = attackRange.transform.position - rayOrigin.transform.position;
		counterVec = rayOrigin.transform.position + new Vector3 ((-pi.GetDirection() * counterVec.x), counterVec.y, counterVec.z);
		Debug.DrawLine (rayOrigin.transform.position, rayOrigin.transform.position + Vector3.Normalize (counterVec), Color.magenta);
		ApplyForce (new attackParams (counterPower, Vector3.Normalize (counterVec * count)));

	}
	
	void ApplyForce(attackParams ap)
	{

		//this.rigidbody.AddForce (new Vector3 (50, 10, 0) * amount);
		//this.rigidbody.velocity = new Vector3 (10, liftVec, 0) * amount;
		this.rigidbody.velocity = ap.direction * ap.force; 
	}
}
