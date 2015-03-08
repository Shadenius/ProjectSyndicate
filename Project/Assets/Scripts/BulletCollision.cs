using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {
	private float damage = 1f;
	private float bulletSpeed = 1.5f;
	
	void Start() {
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * Mathf.PI;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void Update() {

	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Enemy") {
			//Shorten "coll.gameObject.GetComponent<Enemy>()" to "foe" for convinience's sake.
			var foe = coll.gameObject.GetComponent<Enemy>();
			//Debug.Log("Hit" + foe.Health);
			//Enemy takes damage
			foe.Health = foe.Health - damage;					
			
			//Debug.Log (foe.Health);
			//If enemy's health drops to 0 then remove the game object
			if (foe.Health <= 0)
				Destroy(coll.gameObject);
		}

		if (coll.gameObject.tag == "Player") {

		} else {
			Destroy (this.gameObject);
		}
	}
}
