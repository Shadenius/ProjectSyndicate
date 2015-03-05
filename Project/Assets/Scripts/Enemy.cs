using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private float health;
	private float moveSpeed;

	public float Health {
		get { return health; }
		set { health = value;}
	}

	// Use this for initialization
	void Start () {
		health = 4;
		moveSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
