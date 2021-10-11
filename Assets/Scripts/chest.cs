using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

	private Animator _animator;

	private string _spacebar = "Jump";
	private const string _redChestName = "Container_red";
	private const string _brownChestName = "Container_brown";
	private const string _blueChestName = "Container_blue";

	private string _openChest = "_openChest";
	private string _playerTag = "Player";

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerStay2D(Collider2D other){
		if(other.tag == _playerTag){
			if(Input.GetButtonUp(_spacebar)){
				_animator.SetTrigger(_openChest);
				switch(gameObject.name){
					case _redChestName: addPoints(5); break;
					case _brownChestName: addPoints(10); break;
					case _blueChestName: addPoints(20); break;
				}
				GameManager.gameManagerInstance.ChangeScene(ScenesName.QG);
			}
		}
		
	}

	private void addPoints(int ptsToAdd){
		print(GameManager.gameManagerInstance.Inventaire);
		GameManager.gameManagerInstance.Inventaire += ptsToAdd;
		print(GameManager.gameManagerInstance.Inventaire);

	}
}
