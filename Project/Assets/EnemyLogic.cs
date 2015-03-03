using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
	public Transform sightStart, sightEnd;
	public bool inRange = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Raycasting ();
		Behaviours ();
	}

	void Raycasting(){
		Debug.DrawRay (sightStart.position, sightEnd.position, Color.green);

		//inRange = Physics2D.CircleCast
	}

	void Behaviours(){
	
	}
}
