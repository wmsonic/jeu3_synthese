using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gameManagerInstance;

	[SerializeField]
	private int _inventaire = 8;

	private Text _granolaBarTxt;

	private string _granolaBarTag = "granola-bars-text";

	public int Inventaire{
		get{
			return _inventaire;
		}
		set{
			_inventaire = value;
		}
	}

	private int _level1Price = 4;
	private int _level2Price = 6;
	private int _level3Price = 8;

	private void Awake(){
		if(gameManagerInstance != null){
			Destroy(gameObject);
		}else{
			gameManagerInstance = this;
			DontDestroyOnLoad (gameObject);
		}
	}


	public void ChangeScene(ScenesName nomScene){
		if(nomScene == ScenesName.Level1 && Inventaire>=_level1Price){
			Inventaire -= _level1Price;
			SceneManager.LoadScene(nomScene.ToString());
		}else if(nomScene == ScenesName.Level2 && Inventaire>=_level2Price){
			Inventaire -= _level2Price;
			SceneManager.LoadScene(nomScene.ToString());
		}else if(nomScene == ScenesName.Level3 && Inventaire>=_level3Price){
			Inventaire -= _level3Price;
			SceneManager.LoadScene(nomScene.ToString());
		}else if(nomScene != ScenesName.Level1 && nomScene != ScenesName.Level2 && nomScene != ScenesName.Level3){
			SceneManager.LoadScene(nomScene.ToString());
		}
	}
	public void UpdateInventaire(){
		print("inventaire updated" + Inventaire.ToString());
		_granolaBarTxt = GameObject.FindGameObjectWithTag(_granolaBarTag).GetComponent<Text>();
		_granolaBarTxt.text = "You have " + Inventaire + " granola bars";
	}



}

public enum ScenesName {
	Menu,
	QG,
	LevelSelect,
	TutorialRoom,
	Level1,
	Level2,
	Level3,
	End
}