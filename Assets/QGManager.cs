using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QGManager : MonoBehaviour {

	public static QGManager instance;

	[SerializeField]
	private Transform[] _wpsP1;
	
	[SerializeField]
	private Transform[] _wpsP2;

	[SerializeField]
	private Transform[] _wpsP3;

	[SerializeField]
	private Transform[] _wpsP4;


	private void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		PlayerManager.playerManagerInstance.InstantiatePersos();
		if(GameManager.gameManagerInstance.Inventaire < 4){
			GameManager.gameManagerInstance.ChangeScene(ScenesName.End);
		}
		GameManager.gameManagerInstance.UpdateInventaire();
	}

	public Transform[] getWps(string nom){
		
		if(nom.Contains("1")){
			return _wpsP1;
		}else if(nom.Contains("2")){
			return _wpsP2;
		}else if(nom.Contains("3")){
			return _wpsP3;
		}else if(nom.Contains("4")){
			return _wpsP4;
		}
		return null;
	}


	
	// Update is called once per frame
	void Update () {
		
	}

	
}
