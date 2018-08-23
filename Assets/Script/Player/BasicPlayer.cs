﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class BasicPlayer : MonoBehaviour {

	private PlayerState state;
	public Slider healthSlider;
	public Slider blockSlider;
	public Animator animator;
	public CharacterController controller;
	public DamagingCollider damagingCollider;
	public GameObject other;
	public TextAsset cmsTextAsset;
	public float maxBlockTime;
	public float freeBlockTime;
	public float blockRecoveringBufferTime;
	public Timer freeBlockTimer;
	public Timer blockBufferTimer;
	public Timer blockTimer;
	public float speed;
	public int playerNumber;
	private int health = 100;
	public bool isFacingRight = false;

	private CMS cmsInfos;
	public int Health {
		get{
			return health;
		}
		set {
			if (value < 0){
				health = 0;
				ChangeState(new DeadState(this));
			}
			else {
				health = value;
			}
			healthSlider.value = health;
		}
	}
	
	protected void Start () {
		state = new BeginningState(this);
		state.Enter();
		cmsInfos = CSVFileReader.fetchCMSFromFile(cmsTextAsset);
		double hp;
		cmsInfos.movements.TryGetValue("health", out hp);
		Health = Mathf.RoundToInt((float) hp);
		healthSlider.maxValue = Health;
		healthSlider.value = Health;

		SetupTimers();
	}

	void SetupTimers(){
		blockBufferTimer = new Timer(blockRecoveringBufferTime, delegate (){
			blockTimer.IsReversed = true;
			blockTimer.Play();
			// Debug.Log("Blocking bar is recharging");
		});

		blockTimer = new Timer(maxBlockTime, delegate(){
			// Debug.Log("Blocking bar is empty");
			this.ChangeState(new NormalState(this));
		});

		blockTimer.OnTimerUpdate += delegate () {
			blockSlider.value = blockTimer.GetPercentageLeft() ;
			// Debug.Log(blockTimer.Time);
		};
		
		freeBlockTimer = new Timer(freeBlockTime, delegate(){
			blockTimer.IsReversed = false;
			// Debug.Log("Free blocking bar is empty");
			blockTimer.Play();
		});
	}
	
	protected void Update () {
		state.Update(Time.deltaTime);
	}

	public void Hit(int damages, int hitStun, string action){
		state.Hit(damages, hitStun, action);
	}

	public void Damaged(int damages, int hitStun){
		Health -= damages;
		ChangeState(new HitStunState(this, hitStun));
	}

	public void ChangeState(PlayerState newState){
		state.Exit();
		state = newState;
		newState.Enter();
	}

	public void Motion (float delta){
		float xAxisValue = Input.GetAxis("horizontal " + playerNumber);
		float yAxisValue = Input.GetAxis("vertical " + playerNumber);
		if (xAxisValue != 0)
			Move(delta, xAxisValue);
		else
			animator.SetFloat("move", 0f);
		if (yAxisValue > 0)
			Jump();
		else if (yAxisValue < 0)
			Crouch();
		else 
			animator.SetBool("crouch", false);
	}

	void Move(float delta, float axisValue){
		Vector3 movement = new Vector3(axisValue * delta * speed, 0, 0);
		controller.Move(movement);
		if (isFacingRight)
			animator.SetFloat("move", Mathf.Abs(axisValue));
		if (ShouldPlayerTurn(axisValue)){
			controller.transform.Rotate(0f,180f,0f);
			isFacingRight = isFacingRight ? false : true ; 
			Debug.Log ("returned");
		}
	}

	bool ShouldPlayerTurn(float playerDirection){
		return IsEnemyBehind() && IsGoingTowardsEnemy(playerDirection); 
	}
	public bool IsEnemyBehind(){
		float playerXPosition = this.transform.position.x;
		float enemyXPosition = other.transform.position.x;
		return  isFacingRight ? playerXPosition > enemyXPosition : playerXPosition < enemyXPosition;
	}

	bool IsGoingTowardsEnemy(float movementDirection){
		float playerXPosition = this.transform.position.x;
		float enemyXPosition = other.transform.position.x;
		return movementDirection > 0 ? playerXPosition <  enemyXPosition : playerXPosition > enemyXPosition; 
	}

	void Crouch(){
		animator.SetBool("crouch", true);
		ChangeState(new CrouchState(this));
	}

	void Jump(){
		animator.SetBool("jump", true);
	}

	public void Ability(){
		bool hit = false;
		CMS.Ability abilityInformations = null;
		string actionDone = "none";
		string[] actions = new string[]{"heavy normal", "light normal"/*, "anti-air", "over-head", "throw"*/};
		foreach (string action in actions){
			if (Input.GetButtonDown(action + " " + playerNumber)){
				abilityInformations=  GetAbilityInformations(action);
				animator.SetTrigger(action);
				hit = true;
				actionDone = action;
			}
		}
		if (hit){
			ChangeState(new HitState(this));
			damagingCollider.AbilityInformations = abilityInformations;
			damagingCollider.action = actionDone;
		}

		if (Input.GetButtonDown("block " + playerNumber) && !blockTimer.IsFinished()){
			ChangeState(new BlockState(this));
		}
	}

	CMS.Ability GetAbilityInformations(string action){
		CMS.Ability ability;
		cmsInfos.abilities.TryGetValue(action, out ability);
		if (ability == null){
			Debug.Log("Action named " + action + " does not exists in cms file of player " + playerNumber);
			return new CMS.Ability();
		}
		return ability;
	}

	int getHitStunFrames(string action){
		CMS.Ability ability;
		double value;
		cmsInfos.abilities.TryGetValue(action, out ability);
		if (ability == null){
			Debug.Log("Action named " + action + " does not exists in cms file of player " + playerNumber);
			return 10;
		}
		ability.informations.TryGetValue("damage",out value);
		return Mathf.RoundToInt((float) value); 
	}
	public void SetIdleState(){
		ChangeState(new NormalState(this));
	}
	
}
