using UnityEngine;
using System.Collections;

public class GameMastering : MonoBehaviour {
	private int missionType;					//Checks win and lose conditions of certain mission.
	private Collider2D[] playerSpot;
	private Collider2D[] enemySpot;
	//private Collider2D[] enemyTargetSpot;
	//private Collider2D[] NPCSpot;

	public Transform CircleStart;
	public int MissionType {
		get { return missionType; }
		set { missionType = value; }
	}

	// Use this for initialization
	void Start () {
		missionType = 1;
	}
	
	// Update is called once per frame
	void Update () {


		switch (missionType) {
			case 1:
				KillAll();
				break;
		}
	}

	void KillAll () {
		playerSpot = Physics2D.OverlapCircleAll (CircleStart.position, 2000f, 1 << LayerMask.NameToLayer("Player"));
		enemySpot = Physics2D.OverlapCircleAll (CircleStart.position, 2000f, 1 << LayerMask.NameToLayer("Enemy"));

		if (playerSpot.Length <= 0) {
			Debug.Log("Lose");
			Application.LoadLevel("StartScreen");
		}

		if (enemySpot.Length <= 0) {
			Debug.Log("Win");
			Application.LoadLevel("StartScreen");
		}
	}
}
