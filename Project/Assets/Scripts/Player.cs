using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{	
	private int shootMode;										//The shooting mode. 2 actual bullets | 1 precentage based
	private int chanceToHit;									//What chance does player have hit the enemy. 0 - 100
	private int randomNum;										//"The dice roll" for hit
	private GameObject bullet;
	private Vector3 target;										//Move destination coord.
	private float damage;										//How much base damage character deals
	private float maxHealth;									//player max health
	private float curHealth;									//player current health
	private float moveSpeed;									//player movement speed
	private float timer;										//Wait between shots (prevents rapid fire)

	public Slider HPslider;
	public Slider WaitSlider;
	public GameObject Bullet;
	public float CurHealth {
		get { return curHealth; }
		set { curHealth = value; }
	}

	void Start () {
		shootMode = 1;
		chanceToHit = 85;										//85% chance to hit
		maxHealth = 10;											//Set player health
		curHealth = maxHealth;									//Set player current health equal to max
		moveSpeed = 1.5f;										//Set player movement speed
		damage = 1f;											//Set player damage

		target = transform.position;							//Set target position in the world space
	}

	void Update () {
		timer += Time.deltaTime;
		WaitSlider.value = timer;
		HPslider.value = curHealth;
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

	//Sets a function what to do on mouse collision
	void onCollision(Collider2D coll) {
		if (coll.gameObject.name == "Player") {
			//What happens when collision is with "Player" named Game Object
			//Debug.Log ("Player");
		} else if (coll.gameObject.name == "Enemy") {
			//What happens when collision is with "Enemy" named Game Object
			if (timer > 2f) {
				if (shootMode == 1) {
					//Generate random number between 0 and 100.
					randomNum = Random.Range (0, 100);
					//Debug.Log(randomNum);
					//if randomNum is within range of chanceToHit then damage enemy.
					if (randomNum < chanceToHit) {
						//Shorten "coll.gameObject.GetComponent<Enemy>()" to "foe" for convinience's sake.
						var foe = coll.gameObject.GetComponent<Enemy> ();
						//Debug.Log("Hit" + foe.Health);
						//Enemy takes damage
						foe.Health = foe.Health - damage;					
					
						//Debug.Log (foe.Health);
						//If enemy's health drops to 0 then remove the game object
						if (foe.Health <= 0)
							Destroy (coll.gameObject);
					}
				} else if (shootMode == 2) {
					//Intiliaze bullet prefab as game object. Take bullet's and mouse's location, 
					//normalize their distance and add single instance of force.
					bullet = Instantiate (Bullet, transform.position, transform.rotation) as GameObject;
					Vector3 sp = Camera.main.WorldToScreenPoint (transform.position);
					Vector3 dir = (Input.mousePosition - sp).normalized;
					bullet.rigidbody2D.AddForce (dir * 0.001f, ForceMode2D.Impulse);
				}
				timer -= 2f;
			}
		}
	}
}


