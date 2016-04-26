using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	
	
	public float attackCoolDown = 5.0f; //This is the cooldown for the attack/push
	public Transform rayOrigin, attackRange; //These transforms allow you to edit where the attack takes effect.
	public float pushPower = 5.0f; //This modifies how strong the attack/push is.
	public float counterPower = 7.50f;// This modifies how strong the counter push is.
	public float counterDuration = 3.0f; //How long the player can hold their block for.
	public float counterCoolDown = 5.0f; //How long after the release of block until they block again.
	public int attackDamage = 5; //How much damage is applied on attack.
	public int counterDamage = 10; //How much damage is applied on counter.
	
	private PlayerInfo pi, otherPlayer;
	private CharacterController cc;
	private Vector3 attackVec, counterVec; //These vectors will hold the information based on the transform rayOrigin and attackRange.
	private float lastAttackTime, lastBlockTime;
	private Animator animator;
	private bool attacked; 
	private ArrayList attackArr = new ArrayList();
	private CooldownTimer attackCD;
	private CooldownTimer counterCD;
	private CooldownTimer counterDur;


	private ParticleSystem attackVFX;
	private ParticleSystem blockVFX;
	private AudioSource attackSound;

	// Use this for initialization
	void Start () 
	{
		attackSound = gameObject.transform.Find("rayOrigin").GetComponent<AudioSource>();
		attackSound.volume = 0.8f;
		attackSound.Stop();
		attackVFX = gameObject.transform.Find("character/Attack_VFX").GetComponent<ParticleSystem>();
		attackVFX.playOnAwake = false;
		blockVFX = gameObject.transform.Find("character/Block").GetComponent<ParticleSystem>();
		blockVFX.playOnAwake = false;

		pi = gameObject.GetComponent<PlayerInfo>(); //Gets this players PlayerInfo script information.
		cc = gameObject.GetComponent<CharacterController>(); //Gets this players CharacterController information
		animator = transform.Find("character").GetComponent<Animator>();
		attackCD = new CooldownTimer (attackCoolDown, true); //creates a cooldown for the attack.
		counterCD = new CooldownTimer (counterCoolDown); //creates a cooldown for the counter 
		counterDur = new CooldownTimer (counterDuration, true);

		counterCD.forceCooldownOver();

	}

	// Update is called once per frame

	void FixedUpdate () 
	{
		if (attacked == true) {
			attacked = false;
			animator.SetBool ("attack", false);
		}
		//Debug.Log (pi.GetDirection ());
		
		//ATTACK
		if (Input.GetKey ("joystick " + pi.playerNumber + " button 2") && !pi.isBlocking && !pi.isStunned()) { //Checks whether 'X' has been pressed on the players controller.
			if (attackCD.checkCooldownOver()) //Checks whether the attack is on cooldown or not.
			{
				animator.SetBool ("attack", true);
				attacked = true;
				
				attackSound.Play();
				attackVFX.Play();
				
				PushAttack (); //if not on cooldown then attack!
				
			}
		}
		
		//BLOCK
		if(!pi.isStunned())
			Block();



	}
	
	void PushAttack() //The main attack
	{
		attackVec = attackRange.transform.position - rayOrigin.transform.position; //sets the positions of the attack transform objects to a Vector we can use.
		//attackVec = new Vector3 ((pi.GetDirection() * attackVec.x), attackVec.y, attackVec.z); //modifies the vectore depending on which way you are facing.
		//Debug.DrawLine (rayOrigin.transform.position,rayOrigin.transform.position + attackVec, Color.green);
		//Debug.Log (attackVec.x + " " + attackVec.y + " " + attackVec.z);
		
		attackCD.startCooldown(); //Starts the attack cooldown
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
				try
				{
					if (otherPlayer.isBlocking && otherPlayer.GetDirection() != pi.GetDirection()) //checks whether the other player was blocking and facing the attack.
					{
						GetCountered ();
					}
					else
					{
						attackArr.Add (pushPower);
						attackArr.Add (attackVec.normalized);
						
						//Sends a message to that player to use their ApplyForce function using the parameters we give it.
						objectHit.collider.SendMessage("ApplyForce", attackArr, SendMessageOptions.DontRequireReceiver);
						objectHit.collider.SendMessage("ApplyDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
						attackArr.Clear();
					}
				}
				catch
				{
					//used to catch NullReferenceException when the object you are trying to push doesn't
					// have a PlayerInfo script.
					Debug.Log ("NullReferenceException: The object is missing playerinfo script");
				}
			}	
		}
		
	}
	
	
	void GetCountered() //The block's counter move.
	{
		counterVec = attackRange.transform.position - rayOrigin.transform.position;// Gets the attackVec information
		counterVec = new Vector3 (-counterVec.x, counterVec.y, counterVec.z); //Modifies the vector to aim in the opposite direction to the player so you're always hit backwards.
		Debug.Log(-pi.GetDirection());
		//Debug.DrawLine (rayOrigin.transform.position, rayOrigin.transform.position + Vector3.Normalize (counterVec), Color.magenta);
		attackArr.Clear ();
		attackArr.Add (counterPower);
		attackArr.Add (counterVec.normalized);
		this.SendMessage ("ApplyForce", attackArr, SendMessageOptions.DontRequireReceiver); //Has this player knocked backwards.
		this.SendMessage ("ApplyDamage", counterDamage, SendMessageOptions.DontRequireReceiver);
	}

	void Block()
	{
		animator.SetBool("block", pi.isBlocking);
		
		if (pi.isBlocking)
		{
			blockVFX.renderer.enabled = true;
			blockVFX.Play ();
		}
		else
		{
			blockVFX.Clear();
			blockVFX.renderer.enabled = false;
			blockVFX.Stop();
		}
		
		if (Input.GetButton ("P_" + pi.playerNumber + " block")) // Checks whether 'B' has been pressed on the players controller.
		{
			
			if(!pi.isBlocking && counterCD.checkCooldownOver()){
				
				if(!counterDur.isTiming){
					counterDur.startCooldown();
				}
				
				pi.isBlocking = true;
				
			}
			
		}
		else 
		{
			if(pi.isBlocking){
				counterDur.stop();
				pi.isBlocking = false;
				counterCD.startCooldown();
			}
		}
		
		if(pi.isBlocking && counterDur.checkCooldownOver()){
			counterDur.stop();
			pi.isBlocking = false;
			counterCD.startCooldown();
		}
	}
}