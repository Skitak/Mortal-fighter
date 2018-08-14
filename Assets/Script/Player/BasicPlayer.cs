using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicPlayer : MonoBehaviour {

	private PlayerState state;
	public Slider slider;
	public Animator animator;
	public CharacterController controller;
	public float speed;
	private int health = 100;
	public bool isFacingLeft = false;
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
		float xAxisValue = Input.GetAxis("Horizontal");
		float yAxisValue = Input.GetAxis("Vertical");
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
		if (isFacingLeft)
			animator.SetFloat("move", axisValue * -1);
		else
			animator.SetFloat("move", axisValue);

	}

	void Crouch(){
		animator.SetBool("crouch", true);
	}

	void Jump(){
		animator.SetBool("jump", true);
	}
}
