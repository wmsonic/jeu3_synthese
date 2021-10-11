using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private float _speed = 5f;

	private Animator _animator;

	private Rigidbody2D _rigidBody;

	private string _DefaultPlayerPrefabName = "Player";
	private RuntimeAnimatorController _animatorController;
	private RuntimeAnimatorController _SelectedAnims;

	private string _name;
	private string _SelectedName;

	private string _movingAnimParam = "IsMoving";
	private string _horizontalAnimParam = "Horizontal_move";
	private string _verticalAnimParam = "Vertical_move"; 

	private string _horizontalInput = "Horizontal";
	private string _verticalInput = "Vertical";

	private string _shiftInput = "Fire3";

	[SerializeField]
	private float _shiftSlowDown = 2f;

	private string _levelSelectTag = "ToLevelSelect";
	private string _tutoRoomTag = "ToTutorialRoom";
	private string _level1tag = "ToLevel1";
	private string _level2tag = "ToLevel2";
	private string _level3tag = "ToLevel3";
	private string _monstreTag = "Monstre";



	public float GetSpeed{
		get{
			return _speed;
		}
	}

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_rigidBody = GetComponent<Rigidbody2D>();
		_animatorController = _animator.runtimeAnimatorController;
		_name = name;
		

		if(_animatorController == null){
			_SelectedAnims = PlayerManager.playerManagerInstance.SelectedPersoAnims;
			// print(_animatorController + " is the animator before set");
			_animatorController = _SelectedAnims;
			// print(_animatorController + " is the selected animator");
		}else{
			_SelectedAnims = _animatorController;
		}
		_animator.runtimeAnimatorController = _animatorController; // donne à la composante animator le animator controller renvoyer par le perso dans la scene précédente
		
		if(_name == _DefaultPlayerPrefabName){
			_SelectedName = PlayerManager.playerManagerInstance.SelectedPersoName;
			_name = _SelectedName;
		}else{
			_SelectedName = _name;
		}
		gameObject.name = _name;

		print(name);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis(_horizontalInput);
		float vertical = Input.GetAxis(_verticalInput);
		float shift = Input.GetAxis(_shiftInput);
		if(shift>0){
			_rigidBody.velocity = new Vector2(horizontal * _speed / _shiftSlowDown , vertical * _speed / _shiftSlowDown);
		}else{
			_rigidBody.velocity = new Vector2(horizontal * _speed , vertical * _speed);
		}
		
		if(horizontal>0 || vertical>0 || horizontal<0 || vertical<0){
			_animator.SetFloat(_horizontalAnimParam, horizontal);
			_animator.SetFloat(_verticalAnimParam, vertical);
			_animator.SetBool(_movingAnimParam, true);
		}else{
			_animator.SetBool(_movingAnimParam, false);
		}
		
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == _levelSelectTag){
			//aller à la scene "level select" avec le GameManager
			PlayerManager.playerManagerInstance.SelectedPersoAnims = _animatorController;
			PlayerManager.playerManagerInstance.SelectedPersoName = _name;
			// print("Animator was set to " + _animatorController);
			GameManager.gameManagerInstance.ChangeScene(ScenesName.LevelSelect);
		}else if(other.tag == _level1tag){
			GameManager.gameManagerInstance.ChangeScene(ScenesName.Level1);
		}else if(other.tag == _level2tag){
			GameManager.gameManagerInstance.ChangeScene(ScenesName.Level2);
		}else if(other.tag == _level3tag){
			GameManager.gameManagerInstance.ChangeScene(ScenesName.Level3);
		}else if(other.tag == _tutoRoomTag){
			//aller à la scene "Tutorial room" avec le GameManager
			GameManager.gameManagerInstance.ChangeScene(ScenesName.TutorialRoom);
		}
	}

	public void KillPlayer(){
		print("Time to die bro");
		destroyPerso();
	}

	private void destroyPerso(){
		GameManager.gameManagerInstance.ChangeScene(ScenesName.QG);
		PlayerManager.playerManagerInstance.RemovePersoFromList(name);
	}
}
