using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

	[SerializeField]
	private GameObject[] _linkedLasers;

	private string _playerTag = "Player";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other is BoxCollider2D && other.gameObject.tag == _playerTag){
			for(int i = 0; i <= _linkedLasers.Length - 1 ; i++){
				_linkedLasers[i].GetComponent<Animator>().SetBool("laserIsOn", true);
			}
		}
	}

	
	private void OnTriggerExit2D(Collider2D other){
		if(other is BoxCollider2D && other.gameObject.tag == _playerTag){
			for(int i = 0; i <= _linkedLasers.Length - 1; i++){
				_linkedLasers[i].GetComponent<Animator>().SetBool("laserIsOn", false);
			}
		}
	}



}
