using System.Collections;
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
	private int health;
	public int Health{
		get{return health;}
		set{
			if (value <= 0){
				health = 0;
				ChangeState(new DeadState(this));
			}
			else 
				health = value;
			healthSlider.value = health;
		}
	}
	public bool isFacingRight = false;

	private CMS cmsInfos;
	public CMS CmsInfos {
		get { return cmsInfos;}
	}
	
	protected void Start () {
		state = new NormalState(this);
		state.Enter();
		FetchPlayerInformations();
		SetupTimers();
	}

	void FetchPlayerInformations(){
		cmsInfos = CSVFileReader.fetchCMSFromFile(cmsTextAsset);
		double hp;
		cmsInfos.movements.TryGetValue("health", out hp);
		health = Mathf.RoundToInt((float) hp);
		healthSlider.maxValue = health;
		healthSlider.value = health;
		// There is a lot more informations to fetch here such as speed, height, etc..
	}

	void SetupTimers(){
		blockBufferTimer = new Timer(blockRecoveringBufferTime, delegate (){
			blockTimer.IsReversed = true;
			blockTimer.Play();
			// Debug.Log("Blocking bar is recharging");
		});

		blockTimer = new Timer(maxBlockTime, delegate(){
			Debug.Log("Blocking bar is empty");
			Debug.Log(blockTimer.IsFinished());
			this.ChangeState(new NormalState(this));
		});

		blockTimer.OnTimerUpdate += delegate () {
			blockSlider.value = blockTimer.GetPercentageLeft() ;
		};
		
		freeBlockTimer = new Timer(freeBlockTime, delegate(){
			blockTimer.IsReversed = false;
			// Debug.Log("Free blocking bar is empty");
			blockTimer.Play();
		});
	}
	

	public void Hit(int damages, int hitStun, string action){ state.Hit(damages, hitStun, action); }
	public void Damaged(int damages, int hitStun){ state.Damaged(damages, hitStun);	}
	protected void Update () { state.Update();}
	public void Attack(string action){ state.Attack(action); }
	public void Block(){ state.Block(); }
	public void Unblock(){ state.Unblock(); }
	public void Move(float axisValue){ state.Move(axisValue); }
	public void Crouch(){ state.Crouch(); }
	public void Stand(){ state.Stand();	}
	public void Jump(Vector3 direction){	state.Jump(direction); }
	public void Dash(Vector3 direction){ state.Dash(direction); }
	public void ChangeState(PlayerState newState){
		state.Exit();
		state = newState;
		newState.Enter();
	}

	public bool ShouldTurn(float playerDirection){
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

	int getHitStunFrames(string action){
		CMS.Ability ability;
		double value;
		cmsInfos.abilities.TryGetValue(action, out ability);
		if (ability == null){
			Debug.Log("Action named " + action + " does not exists in cms file.");
			return 10;
		}
		ability.informations.TryGetValue("damage",out value);
		return Mathf.RoundToInt((float) value); 
	}
	
}
