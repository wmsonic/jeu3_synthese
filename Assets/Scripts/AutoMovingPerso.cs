using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovingPerso : MonoBehaviour {


	private Animator _animator;

	private Transform[] _waypoints = {};

	private Player  _PlayerScript;

	private float _speed;

	private string _hoveredPerso;

	private float _randomWaitTime;

	private string _movingAnimParam = "IsMoving";
	private string _horizontalAnimParam = "Horizontal_move";
	private string _verticalAnimParam = "Vertical_move"; 

	private string _monstreTag = "Monstre";

	private string _playerTag = "Player";


	// Use this for initialization
	void Start () {
		if(gameObject.tag == _playerTag){
			_waypoints = QGManager.instance.getWps(name);
		}else if(gameObject.tag == _monstreTag){
			_waypoints = GetComponent<MonstreWps>().Waypoints ;
		}
		
		_animator = GetComponent<Animator>();
		_PlayerScript = GetComponent<Player>();
		if(gameObject.tag == _playerTag){
			_speed = _PlayerScript.GetSpeed;
		}
		PlayerManager.playerManagerInstance.selectionPersoEvent += OnSelectionPerso;
		StartCoroutine(AllerRetour());
		
	}

	private void FixedUpdate(){
		_randomWaitTime = Random.Range(0f,2f);
	}

	private void OnDestroy(){
		if(gameObject.tag != _monstreTag){
			PlayerManager.playerManagerInstance.selectionPersoEvent -= OnSelectionPerso;
		}
		
	}


	IEnumerator AllerRetour(){
		// Debug.Log("ALLER RETOUR" + name);
		while(true){
			yield return StartCoroutine(Aller());
			yield return StartCoroutine(Retour());
		}
	}

	IEnumerator Aller(){
		// Debug.Log("ALLER" + name);
		for(int i=0; i < _waypoints.Length; i++){
			yield return new WaitForSeconds(_randomWaitTime);
			// print(_waypoints[i]);
			yield return randomMonstreSpeed();
			yield return StartCoroutine(GoToPos(_waypoints[i].position));
		}
		
		yield return null;
		
	}

	IEnumerator Retour(){
		// Debug.Log("RETOUR" + name);
		for(int i = _waypoints.Length - 1; i > 0; i--){
			yield return new WaitForSeconds(_randomWaitTime);
			yield return randomMonstreSpeed();
			yield return StartCoroutine(GoToPos(_waypoints[i].position));
		}
		yield return null;
		
	}

	IEnumerator GoToPos(Vector2 waypoint){
		_animator.SetBool(_movingAnimParam,true);
		
		Vector2 delta = waypoint - (Vector2)transform.position;

		_animator.SetFloat(_horizontalAnimParam, delta.normalized.x);
		_animator.SetFloat(_verticalAnimParam, delta.normalized.y);

		//fait déplacer le personnage vers le waypoint donner
		while((Vector2)transform.position != waypoint){
			transform.position = Vector2.MoveTowards(transform.position, waypoint, _speed * Time.deltaTime);
			yield return null;
		}
		_animator.SetBool(_movingAnimParam,false);
		
	}

	private void OnMouseUp(){
		PlayerManager.playerManagerInstance.Select(this.gameObject);
	}

	private void OnSelectionPerso(GameEvent e, GameObject persoSelectionner){
		if (this == null){
			return; //je sais pas pourquoi mais un bug se produit si c'est pas la
		}
		if(e == GameEvent.SelectPerso && this.gameObject == persoSelectionner && this.tag != _monstreTag){
			print(gameObject.name + " is selected");
			_PlayerScript.enabled = true;
			this.StopAllCoroutines();
			this.enabled = false;
		}else if(e == GameEvent.DeselectPerso && this.gameObject != persoSelectionner && this.tag != _monstreTag){
			print(gameObject.name + " is deselected");
			_PlayerScript.enabled = false;
			this.enabled = true;
			this.StopAllCoroutines();
			this.StartCoroutine(AllerRetour());
		}
	}

	private float randomMonstreSpeed(){
		if(gameObject.tag == _monstreTag){
			_speed = Random.Range(5f, 10f);
			return _speed;			
		}
		return _speed;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == _playerTag && this.tag == _monstreTag){
			other.GetComponent<Player>().KillPlayer();
		}
	}
	

}
