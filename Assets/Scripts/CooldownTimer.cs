//Basic cooldown class for handleing any kind of cooldowns in the game
//eg: attack, block, "pizza throwers", etc...

//useage:
	//pass cooldown duration in seconds as param in the constructor, 2nd param in constructor indicates wether or not to start cooldown immediately
    //call startCooldown (this is called automatically by default)
    //call checkCooldownOver. returns true if the cooldown duration is elapsed, false otherwise
	//cooldown can be reset by calling startCooldown again.
	//see projectile launcher script for useage example

//Author: Scott Larkin
//Date:   Jan 2016

using UnityEngine;
using System.Collections;

public class CooldownTimer {

	public float cooldown;
	
	private float startTime;
 
	public CooldownTimer(float cd, bool startImmediately = true){
		cooldown = cd;
		startTime = startImmediately ? float.NegativeInfinity : float.PositiveInfinity;

		if(startImmediately) startCooldown();
	}
	
	public void startCooldown(){

		startTime = Time.time;
	}

	public bool checkCooldownOver(){

		if(startTime + cooldown <= Time.time) return true;

		return false;
	}
	
	public void stop(){
		startTime = float.PositiveInfinity;
	}
}
