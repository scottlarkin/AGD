using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	
	
	public float attackCoolDown = 5.0f; //This is the cooldown for the attack/push
	public Transform rayOrigin, attackRange; //These transforms allow you to edit where the attack takes effect.
	public float pushPower = 5.0f; //This modifies how strong the attack/push is.
	public float counterPower = 7.50f;// This modifies how strong the counter push is.
	public float blockTime = 3.0f; //How long the player can hold their block for.
	public float blockCoolDown = 5.0f; //How long after the release of block until they block again.
	
	private PlayerInfo pi, otherPlayer;
	private CharacterController cc;
	private Vector3 attackVec, counterVec; //These vectors will hold the information based on the transform rayOrigin and attackRange.
	private float lastAttackTime, lastBlockTime;
	
	
	private struct attackParams //private struct which holds the information for the direction and how powerfully to push a player.
	{
		public attackParams(float f, Vector3 d){force = f; direction = d;}
		public float force;
		public Vector3 direction;
	}
	
	
	// Use this for initialization
	void Start () 
	{
		
		pi = gameObject.GetComponent<PlayerInfo>(); //Gets this players PlayerInfo script information.
		cc = gameObject.GetComponent<CharacterController>(); //Gets this players CharacterController information
		
		lastAttackTime = -attackCoolDown; //Initially allows you attack straight from the start of the game
		lastBlockTime = -blockCoolDown; //Initially allows you block without waiting for the cooldown.
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//Debug.Log (pi.GetDirection ());
		
		if (Input.GetKey("joystick "+pi.playerNumber+" button 2")) //Checks whether 'X' has been pressed on the players controller.
		{
			if (CanAttack()) //Checks whether the attack is on cooldown or not.
			{
				PushAttack(); //if not on cooldown then attack!
			}
		}
		
		if (Input.GetKey ("joystick " + pi.playerNumber + " button 1")) // Checks whether 'B' has been pressed on the players controller.
		{
			if (CanBlock ())
			{
				//gameObject.transform.Translate(new Vector3(-1,0,0));
				pi.isBlocking = true; //Sets the boolean in the playerinfo script to true.
				
			}
		} else 
		{
			pi.isBlocking = false;//If B isn't being pressed then they are not blocking.
		}
		//Debug.Log (pi.isBlocking);
	}
	
	bool CanAttack () //This is the function which implements the cooldown for the attack and returns whether the player should be able to attack or not.
	{
		float timer;
		
		timer = Time.time; //get the time.
		
		if  (lastAttackTime + attackCoolDown <= timer) //If the last attack time + the cooldown is less than the current time, then the cooldown has finished and you can attack.
		{
			return true;
		}
		
		return false;
		
	}
	void PushAttack() //The main attack
	{
		attackVec = attackRange.transform.position - rayOrigin.transform.position; //sets the positions of the attack transform objects to a Vector we can use.
		attackVec = new Vector3 ((pi.GetDirection() * attackVec.x), attackVec.y, attackVec.z); //modifies the vectore depending on which way you are facing.
		Debug.DrawLine (rayOrigin.transform.position,rayOrigin.transform.position + attackVec, Color.green);
		
		lastAttackTime = Time.time; //sets the last attack time to the current time.
		//Debug.Log ("You are attacking");
		
		
		RaycastHit objectHit; //This will be the object the player's attack hits.
		
		//creates a linecast from the position of the rayOrigin to the AttackVector, and returns information to the objectHit/
		//I used linecast instead of raycast, as linecast allows for easy drawing of lines between 'a' and 'b' whilst raycast 
		// is more for setting a direction and then the vector carries on continuouslly from that point.
		if (Physics.Linecast (rayOrigin.transform.position, rayOrigin.transform.position + attackVec, out objectHit))
		{
			//Debug.Log (objectHit.collider.name);
			if(!objectHit.collider.gameObject.CompareTag(this.tag)) //Make sure that the objectHit isn't the own player's collider.
			{
				otherPlayer = objectHit.collider.gameObject.GetComponent<PlayerInfo>(); //Get's the PlayerInfo script of the player who's collider was hit.
				if (otherPlayer.isBlocking) //checks whether the other player was blocking or not.
				{
					CounterAttack ();
				}
				else
				{
					//Sends a message to that player to use their ApplyForce function using the parameters we give it.
					objectHit.collider.SendMessage("ApplyForce", new attackParams(pushPower, Vector3.Normalize(attackVec)) , SendMessageOptions.DontRequireReceiver);
					
				}
			}	
		}
		
	}
	
	bool CanBlock () //This is the function which implements the cooldown for the block and returns whether the player should be able to block or not.
	{
		float timer;
		
		timer = Time.time; //get the time.
		
		if  (lastBlockTime + blockCoolDown <= timer) //If the last block time + the cooldown is less than the current time, then the cooldown has finished and you can block.
		{
			return true;
		}
		
		return false;
		
	}
	
	void CounterAttack() //The counter/block move
	{
		counterVec = attackRange.transform.position - rayOrigin.transform.position;// Gets the attackVec information
		counterVec = new Vector3 ((-pi.GetDirection() * counterVec.x), counterVec.y, counterVec.z); //Modifies the vector to aim in the opposite direction to the player so you're always hit backwards.
		
		//Debug.DrawLine (rayOrigin.transform.position, rayOrigin.transform.position + Vector3.Normalize (counterVec), Color.magenta);
		
		ApplyForce (new attackParams (counterPower, Vector3.Normalize (counterVec))); //Has this player knocked backwards.
		
	}
	
	void ApplyForce(attackParams ap) // This is called to apply a force to this player's character controller.
	{	
		//moves the charactercontroller in a certain direction by a certain amount of force.
		cc.Move((ap.direction * ap.force) * Time.deltaTime); 
		
	}
}
