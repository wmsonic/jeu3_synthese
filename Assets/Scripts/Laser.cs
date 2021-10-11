using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	private Animator _animator;

	private LineRenderer _lineRenderer;

	private Vector2 _wallHitPosition;

	private int _lineRendPosCount = 2;

	private Vector3[] _lineRendPoints;

	[SerializeField]
	private LayerMask _wallHitLayer;

	[SerializeField]
	private LayerMask _playerHitLayer;

	private Vector2 _laserDirection;

	private string _laserToRight = "ToRight";
	private string _laserToLeft = "ToLeft";
	private string _laserToTop = "ToTop";
	private string _laserToBottom = "ToBottom";


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_lineRenderer = transform.Find("Line").GetComponent<LineRenderer>();
		_lineRendPoints = new Vector3[_lineRendPosCount];
		if(tag.Contains(_laserToLeft)){
			_laserDirection = Vector2.left;
		}else if(tag.Contains(_laserToRight)){
			_laserDirection = Vector2.right;
		}else if(tag.Contains(_laserToTop)){
			_laserDirection = Vector2.up;
		}else if(tag.Contains(_laserToBottom)){
			_laserDirection = Vector2.down;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_animator.GetBool("laserIsOn")){
			//rend le laser visible
			RaycastHit2D wallHit = Physics2D.Raycast(transform.position,_laserDirection, 10000000f, _wallHitLayer);
			_wallHitPosition = wallHit.point;
			_lineRendPoints[0] = new Vector3(transform.position.x, transform.position.y);
			_lineRendPoints[1] = new Vector3(_wallHitPosition.x, _wallHitPosition.y);
			_lineRenderer.enabled = true;

			RaycastHit2D playerHit = Physics2D.Raycast(transform.position,_laserDirection, wallHit.distance, _playerHitLayer);
			if(playerHit){
				//ends the game
				GameObject hitPlayer = playerHit.transform.gameObject;
				hitPlayer.GetComponent<Player>().KillPlayer();
			}
			_lineRenderer.SetPositions(_lineRendPoints);
		}else{
			_lineRenderer.enabled = false;
		}
		
	}
}
