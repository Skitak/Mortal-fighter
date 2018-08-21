using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicPlayer : MonoBehaviour {

	private PlayerState state;
	public Slider slider;
	public Animator animator;
	public CharacterController controller;
	public DamagingCollider damagingCollider;
	public GameObject other;
	public float speed;
	public int playerNumber;
	private int health = 100;
	public bool isFacingRight = false;
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
			slider.value = health;
		}
	}
	
	protected void Start () {
		state = new BeginningState(this);
		state.Enter();
	}
	
	protected void Update () {
		state.Update(Time.deltaTime);
	}

	protected void Hit(int damages, float hitStun){
		state.Hit(damages, hitStun);
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
			animator.SetFloat("move", axisValue * -1);
		else
			animator.SetFloat("move", axisValue);
			// Debug.Log("yo");
		if (isEnemyBehind()){
			controller.transform.Rotate(0f,180f,0f);
			isFacingRight = isFacingRight ? false : true ; 
			Debug.Log ("returned");
		}
	}

	bool isEnemyBehind(){
		float xPosition = this.transform.position.x;
		float enemyPosition = other.transform.position.x;
		return  isFacingRight ? xPosition > enemyPosition : xPosition < enemyPosition;
	}

	void Crouch(){
		animator.SetBool("crouch", true);
		ChangeState(new CrouchState(this));
	}

	void Jump(){
		animator.SetBool("jump", true);
	}

	public void Attack(){
		bool hit = false;
		string[] animations = new string[]{"heavy normal", "light normal", "anti-air", "over-head", "throw"};
		foreach (string animation in animations){
			if (Input.GetButtonDown(animation)){
				animator.SetTrigger(animation);
				hit = true;
			}
		}
		if (hit){
			ChangeState(new HitState(this));
			damagingCollider.damages = 10;
		}
	}

	public void SetIdleState(){
		ChangeState(new NormalState(this));
	}
}
