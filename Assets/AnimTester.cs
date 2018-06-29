using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTester : MonoBehaviour {

	public Animator anim;
	public enum Moves {Idle, Jump, WalkFW, WalkBW, PunchUp, PunchDown, KickUp, KickDown, StunUp, StunMid};
	
	public Moves Input0;
	public Moves Input1;
	public Moves Input2;
	public Moves Input3;
	public Moves Input4;
	public Moves Input5;
	public Moves Input6;
	public Moves Input7;
	public Moves Input8;
	public Moves Input9;


	
	void Start () 
	{
		
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Keypad0))
		{
			anim.SetTrigger(Input0.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			anim.SetTrigger(Input1.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			anim.SetTrigger(Input2.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad3))
		{
			anim.SetTrigger(Input3.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad4))
		{
			anim.SetTrigger(Input4.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad5))
		{
			anim.SetTrigger(Input5.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad6))
		{
			anim.SetTrigger(Input6.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad7))
		{
			anim.SetTrigger(Input7.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad8))
		{
			anim.SetTrigger(Input8.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Keypad9))
		{
			anim.SetTrigger(Input9.ToString());
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			anim.SetBool("WalkFW", true);
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow))
		{
			anim.SetBool("WalkFW", false);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			anim.SetBool("WalkBW", true);
		}
		if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			anim.SetBool("WalkBW", false);
		}
	}
}
