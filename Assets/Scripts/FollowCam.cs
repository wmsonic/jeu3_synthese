using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FollowCam : MonoBehaviour {

	[SerializeField]
	private Transform _target;

	[SerializeField]
	private TilemapRenderer _floorTilemap;

	private Camera _camera;

	private Vector2 _velocity = Vector2.zero;	
	private float _initZ;

	[SerializeField]
	private float _smoothTime = .5f;

	// Use this for initialization
	void Start () {
		_initZ = transform.position.z;
		_camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_target != null){
			float demiLargeurCam = _camera.orthographicSize * _camera.aspect;
			float demiHauteurCam = _camera.orthographicSize;
			float limiteMaxX = _floorTilemap.bounds.max.x - demiLargeurCam;
			float limiteMinX = _floorTilemap.bounds.min.x + demiLargeurCam;
			float limiteMaxY = _floorTilemap.bounds.max.y - demiHauteurCam;
			float limiteMinY = _floorTilemap.bounds.min.y + demiHauteurCam;
			
			Vector3 newPos = Vector2.SmoothDamp(transform.position, _target.position, ref _velocity, _smoothTime); //smoothDamp pas necessaire mais c'est fun d'avoir l'option
			newPos.z = _initZ;
			float clampX = Mathf.Clamp(newPos.x, limiteMinX, limiteMaxX);
			// print(clampX + " is clamp x");
			// float clampY = Mathf.Clamp(newPos.y, -4, 2);
			float clampY = Mathf.Clamp(newPos.y, limiteMinY, limiteMaxY);
			// print(clampY + " is clamp y");
			transform.position = new Vector3(clampX,clampY,newPos.z);
		}
	
		
	}
}
