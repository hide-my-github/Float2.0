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
		//astar = GetComponent<Astar> ();
		//State = GetComponent<State> ();
	}

	// Update is called once per frame
	void Update() {

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
		//Debug.Log ("before Astarpath");
		//Astar ();
		//Debug.Log ("after Astarpath");
		/*
		if (path != null && path.Count != 0) {
			string next_move = path [0];
			path.RemoveAt (0);
			Debug.Log ("A: " + next_move);
			State position = new State (this.gameObject.transform.position);
			apply_move (next_move, position);
		} else {*/
//		Debug.Log ("B");
		State initial = new State(this.gameObject.transform.position);
		path = astar.Aalgorithm(initial);
		//Debug.Log ("path: " + path[0]);
		apply_move (path [0]);
		/*for (int i = 0; i < path.Count; i++) {
			apply_move (path[i], initial);
		}*/
		path.Clear ();

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

	public void Astar () {
		//		Debug.Log ("Here before A star");
		//use a list of strings 
		//		Debug.Log ("After A*");
		//		foreach (string item in path) { Debug.Log ((item)); }
		//		Debug.Log ("Should print path");
	}

	public void moveUpLeft(State state){
		Vector2 pos = transform.position;
		pos += new Vector2 (-0.1f, 0.0f);
		pos += new Vector2 (0.0f, 0.1f);
		state.position = pos;
	}
	public void moveUpRight(State state){
		Vector2 pos = transform.position;
		pos += new Vector2 (0.1f, 0.0f);
		pos += new Vector2 (0.0f, 0.1f);
		state.position = pos;
	}
	public void moveDownLeft(State state){
		Vector2 pos = transform.position;
		pos += new Vector2 (-0.1f, 0.0f);
		pos += new Vector2 (0.0f, -0.1f);
		state.position = pos;
	}
	public void moveDownRight(State state){
		Vector2 pos = transform.position;
//		Debug.Log ("pos: " + pos);
		pos += new Vector2 (0.1f, 0.0f);
		pos += new Vector2 (0.0f, -0.1f);
		pos += new Vector2(0.1f, -0.1f);
		state.position = pos;
	}
	public void moveUp(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.0f, 0.1f);
		state.position = pos;
	}
	public void moveDown(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.0f, -0.1f);
		state.position = pos;
	}
	public void moveLeft(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(-0.1f, 0.0f);
		state.position = pos;
	}
	public void moveRight(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.1f, 0.0f);
		state.position = pos;
	}

	public void apply_move(string move, State state) {
		//applies move for AI to do certain function
		//only for simulation purposes
		if (move == "moveUpLeft") {
			moveUpLeft (state);
		} else if (move == "moveUpRight") {
			moveUpRight (state);
		} else if (move == "moveDownLeft") {
			moveDownLeft (state);
		} else if (move == "moveDownRight") {
			moveDownRight (state);
		} else if (move == "moveUp") {
			moveUp (state);
		} else if (move == "moveLeft") {
			moveLeft (state);
		} else if (move == "moveDown") {
			moveDown (state);
		} else if (move == "moveRight") {
			moveRight (state);
		}
	}

	/*		THESE MOVEMENT METHODS AFFECT REAL STATE
	 * 		Note how there are no arguments
	 */

	public void moveUpLeft(){
		Vector2 pos = transform.position;
		pos += new Vector2 (-0.1f, 0.0f);
		pos += new Vector2 (0.0f, 0.1f);
		transform.position = pos;
	}
	public void moveUpRight(){
		Vector2 pos = transform.position;
		pos += new Vector2 (0.1f, 0.0f);
		pos += new Vector2 (0.0f, 0.1f);
		transform.position = pos;
	}
	public void moveDownLeft(){
		Vector2 pos = transform.position;
		pos += new Vector2 (-0.1f, 0.0f);
		pos += new Vector2 (0.0f, -0.1f);
		transform.position = pos;
	}
	public void moveDownRight(){
		Vector2 pos = transform.position;
		//		Debug.Log ("pos: " + pos);
		pos += new Vector2 (0.1f, 0.0f);
		pos += new Vector2 (0.0f, -0.1f);
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


	public void apply_move(string move) {
		//applies move for AI to do certain function
		//only for simulation purposes
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