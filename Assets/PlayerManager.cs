using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


	//delegates
	public delegate void SelectionPersoDelegate(GameEvent e, GameObject perso);
	public event SelectionPersoDelegate selectionPersoEvent;



	//parametres

	public static PlayerManager playerManagerInstance;

	[SerializeField]
	private List<PlayerInfo> _persosInfo;

	private RuntimeAnimatorController _selectedPersoAnims;
	public RuntimeAnimatorController SelectedPersoAnims{
		get{
			return _selectedPersoAnims;
		}
		set{
			_selectedPersoAnims = value;
		}
	}

	private string _selectedPersoName;
	public string SelectedPersoName{
		get{
			return _selectedPersoName;
		}
		set{
			_selectedPersoName = value;
		}
	}

	private void Awake(){
		if(playerManagerInstance==null){
			playerManagerInstance = this;
		}else{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void Select(GameObject perso){
		if(selectionPersoEvent != null){
			selectionPersoEvent(GameEvent.DeselectPerso, perso);
			selectionPersoEvent(GameEvent.SelectPerso, perso);
		}
	}

	public void InstantiatePersos(){
		for(int i = 0; i<_persosInfo.Count; i++){
			Instantiate(_persosInfo[i].perso);
		}
		if(_persosInfo.Count == 0){
			GameManager.gameManagerInstance.ChangeScene(ScenesName.End);
		}
	}

	public void RemovePersoFromList(string persoToRemove){
		foreach (PlayerInfo playerInfo in _persosInfo){
			print(playerInfo.character_name + " was checked");
			print(persoToRemove + " is being compared");
			if(persoToRemove.Contains(playerInfo.character_name)){
				print(playerInfo + " was removed");
				_persosInfo.Remove(playerInfo);
			}
		}
		
	}

	

}

public enum GameEvent{
	SelectPerso,
	DeselectPerso
}
