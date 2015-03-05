using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{				
	private GameObject bullet;
	private Vector3 target;										//Move destination coord.
	private float damage;										//How much base damage character deals
	private float maxHealth;									//player max health
	private float curHealth;									//player current health
	private float moveSpeed;									//player movement speed

	public GameObject Bullet;

	void Start () {
		maxHealth = 10;											//Set player health
		curHealth = maxHealth;									//Set player current health equal to max
		moveSpeed = 1.5f;										//Set player movement speed
		damage = 1f;											//Set player damage

		target = transform.position;							//Set target position in the world space
	}

	void Update () {
		//Find Mouse Position on Left Mouse Button Click and check if it hits any colliders
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			/*
			 * Check if Mouse Position is within any Collider. 
			 * If null -> move 
			 * If not null -> do onCollision function.
			 */
			if(hit.collider == null)
			{
				//Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
				
				target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				target.z = transform.position.z;
			} else if (hit.collider != null){
				onCollision(hit.collider);
			}
		}
		
		//Move object to target
		transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
	}

	//Sets a function what to do on collision
	void onCollision(Collider2D coll) {
		if (coll.gameObject.name == "Player") {
			//What happens when collision is with "Player" named Game Object
			//Debug.Log ("Player");
		} else if (coll.gameObject.name == "Enemy") {
			//What happens when collision is with "Enemy" named Game Object
			//Shorten "coll.gameObject.GetComponent<Enemy>()" to "foe" for convience sake.
			var foe = coll.gameObject.GetComponent<Enemy>();
			foe.Health = foe.Health - damage;					//Enemy takes damage

			//Debug.Log (foe.Health);
			//If enemy's health drops to 0 then remove the game object
			if (foe.Health <= 0)
				Destroy(coll.gameObject);
		}
	}
}


