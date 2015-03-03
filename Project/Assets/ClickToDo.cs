﻿using UnityEngine;
using System.Collections;

public class ClickToDo : MonoBehaviour {
	private Vector3 target;											//Destination coord.

	public float moveSpeed = 1.5f;									//Movement speed.

	void Start () {
		target = transform.position;
	}
	void Update () {
		//Find Mouse Position on Left Mouse Button Click
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit.collider == null)
			{
				//Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);

				target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				target.z = transform.position.z;
			} else if (hit.collider != null) {
				Debug.Log("Shot");
			}
		}

		//Move object to target
		transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
	}
}
