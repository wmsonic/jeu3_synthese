﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameManager.gameManagerInstance.UpdateInventaire();
	}
	
}
