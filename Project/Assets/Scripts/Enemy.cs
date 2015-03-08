using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private int chanceToHit;			//What chance does player have hit the enemy. 0 - 100
	private int randomNum;				//"The dice roll" for hit
	private float health;				//Enemy's health
	private float moveSpeed;			//Enemy's movement speed
	private float damage;				//Enemy's damage
	private float timer;				//Timer to wait on shoot
	private Collider2D[] playerSpot;	//2D Collider list of player

	public Transform circleStart;

	public float Health {
		get { return health; }
		set { health = value;}
	}

	// Use this for initialization
	void Start () {
		chanceToHit = 85;
		health = 4;
		moveSpeed = 1f;
		damage = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 2f) {
			timer = 2f;
		}

		Raycasting ();
		Behaviours ();
	}

	void Raycasting () {
		//Creates 3 unites large circle at circleStart transform's location. 
		//Makes list of all game objects within range in the layer "Player".
		playerSpot = Physics2D.OverlapCircleAll (circleStart.position, 10f, 1 << LayerMask.NameToLayer("Player"));
		//if (playerSpot.Length > 0)
		//	Debug.Log ("Spotted");
	}

	void Behaviours () {
		if (playerSpot.Length > 0) {
			//Move towards player's location
			transform.position = Vector2.MoveTowards(transform.position, playerSpot[0].transform.position, moveSpeed * Time.deltaTime);

			Shoot ();
		}
	}

	void Shoot () {
		if (timer >= 2f) {
			randomNum = Random.Range(0, 100);
			if ( randomNum < chanceToHit) {
				var foe = playerSpot[0].gameObject.GetComponent<Player>();
				//Debug.Log(foe.CurHealth);
				foe.CurHealth = foe.CurHealth - damage;

				if (foe.CurHealth <= 0)
					Destroy(playerSpot[0].gameObject);
			}
			timer -= 2f;
		}
	}
}
