using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[CreateAssetMenu(fileName="New Character", menuName="Character/Create Character")]
public class PlayerInfo : ScriptableObject{

	[SerializeField]
	public GameObject perso;

	public string character_name;


	

}
