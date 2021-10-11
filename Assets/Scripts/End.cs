using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour {


	[SerializeField]
	private Text _ptsTxtField;

	private void Awake(){
		_ptsTxtField.text = GameManager.gameManagerInstance.Inventaire + " granola bars in your inventory";
	}
}
