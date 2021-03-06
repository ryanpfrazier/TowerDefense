﻿using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;
	public int health;

	private Transform target;
	private float dist;
	private int waypointIndex;
	public float rotationSpeed;
	public bool enemyAlive = true;

	void Start () {
		target = Waypoints.points [0];
	}

	public void SubtractHealth (int damage) {
		health -= damage;
		if (health <= 0 && enemyAlive) {
			enemyAlive = false;
			DieAndProfit ();
		}
	}

	void DieAndProfit() {
		Destroy (gameObject);
		Stats.Cash += 10;
	}

	void Update () {
		// move enemy to next target waypoint
		dist = Vector3.Distance (transform.position, target.position);
		ChangeTarget ();
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
		// rotate enemy towards next target waypoint
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * rotationSpeed);
	}
		
	void ChangeTarget() {
		// make next waypoint the target
		if (dist <= 0.25f) {
			waypointIndex++;
		}
		// Destroy enemy game object if it reaches the final waypoint
		if (waypointIndex >= Waypoints.points.Length) {
			EnemyReachesSun ();
			return;
		}
		target = Waypoints.points [waypointIndex];
	}

	void EnemyReachesSun () {
		Stats.Lives--;
		Destroy (gameObject);
	}
}
