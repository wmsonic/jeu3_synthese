using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstreWps : MonoBehaviour {

	[SerializeField]
	private Transform[] _waypoints;

	public Transform[] Waypoints{
		get{
			return _waypoints;
		}
	}
}
