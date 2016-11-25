using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehavior : MonoBehaviour {

	public GameObject launchPrefab2;
	public Astar astar;
	List<string> path;
	public State State;

	private string move;
	float fireDelay = 0.5f;

	float nextFire = 0.5f;
	float fireVelocity = 10f;

	// Use this for initialization
	void Start () {
		astar = GetComponent<Astar> ();
		State = GetComponent<State> ();
	}

	// Update is called once per frame
	void Update() {
		//use a list of strings 
		State initial = new State(transform.position);
		path = astar.Aalgorithm(initial);
		for (var j = path.GetEnumerator (); j.MoveNext ();) {
			move = j.Current;
			if (move == "moveUpLeft") {
				moveUpLeft ();
			} else if (move == "moveUpRight") {
				moveUpRight ();
			} else if (move == "moveDownLeft") {
				moveDownLeft ();
			} else if (move == "moveDownRight") {
				moveDownRight ();
			} else if (move == "moveUp") {
				moveUp ();
			} else if (move == "moveLeft") {
				moveLeft ();
			} else if (move == "moveDown") {
				moveDown ();
			} else if (move == "moveRight") {
				moveRight ();
			}
		}
	}

	//FPS
	void FixedUpdate()
	{
		nextFire -= Time.deltaTime;

		if (nextFire <= 0.0f)
		{
			nextFire = 0.5f;
			shoot();
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//Debug.Log("HIT");
		Destroy(col.gameObject);
		GameObject.Find("_SCRIPTS_").GetComponent<hitsScript>().incrementHits();
	}


	private void shoot(){
		GameObject launchThis = (GameObject)Instantiate(launchPrefab2, transform.position, transform.rotation);
		launchThis.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 8.0f);
		launchThis.transform.up = launchThis.transform.GetComponent<Rigidbody2D>().velocity;

		launchThis.GetComponent<Rigidbody2D>().isKinematic = false;
		launchThis.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
		Physics2D.IgnoreCollision(launchThis.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
	}

	public void moveUpLeft(){
		Vector2 pos = transform.position;
		pos += new Vector2(-0.1f, 0.1f);
		transform.position = pos;
	}
	public void moveUpRight(){
		Vector2 pos = transform.position;
		pos += new Vector2(0.1f, 0.1f);
		transform.position = pos;
	}
	public void moveDownLeft(){
		Vector2 pos = transform.position;
		pos += new Vector2(-0.1f, -0.1f);
		transform.position = pos;
	}
	public void moveDownRight(){
		Vector2 pos = transform.position;
		pos += new Vector2(0.1f, -0.1f);
		transform.position = pos;
	}
	public void moveUp(){
		Vector2 pos = transform.position;
		pos += new Vector2(0.0f, 0.1f);
		transform.position = pos;
	}
	public void moveDown(){
		Vector2 pos = transform.position;
		pos += new Vector2(0.0f, -0.1f);
		transform.position = pos;
	}
	public void moveLeft(){
		Vector2 pos = transform.position;
		pos += new Vector2(-0.1f, 0.0f);
		transform.position = pos;
	}
	public void moveRight(){
		Vector2 pos = transform.position;
		pos += new Vector2(0.1f, 0.0f);
		transform.position = pos;
	}
}
