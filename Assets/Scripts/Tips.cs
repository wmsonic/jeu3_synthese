using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour {

	[SerializeField]
	private Text _tipsField;

	private Text _textTest;


	[SerializeField]
	[TextArea]
	private string _tipText;

	[SerializeField]
	private float _tipLenght = 4f;

	private string _playerTag = "Player";

	private void Start(){
		// _textTest = GameObject.FindGameObjectWithTag("Text-test-tag").Text;
	}

	IEnumerator AfficherTip(){
		AfficherLeTip();
		yield return new WaitForSeconds(_tipLenght);
		StartCoroutine(EffacerTip());
	}

	private void AfficherLeTip(){
		// print(_tipText);
		_tipsField.text = _tipText;
		_tipsField.enabled = true;
	}

	IEnumerator EffacerTip(){
		EffacerLeTip();
		yield return null;
	}

	private void EffacerLeTip(){
		_tipsField.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == _playerTag){
			// print("This tip should appear");
			StartCoroutine(AfficherTip());
		}
	}
}
