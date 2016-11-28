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
		Astar ();

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
		Debug.Log ("Here before A star");
		//use a list of strings 
		State initial = new State(this.gameObject.transform.position);
		path = astar.Aalgorithm(initial);
		Debug.Log ("After A*");
		foreach (string item in path) { Debug.Log ((item)); }
		Debug.Log ("Should print path");
		for (var j = path.GetEnumerator (); j.MoveNext ();) {
			move = j.Current;
			apply_move (move, initial);
		}
		path.Clear ();
	}

	public void moveUpLeft(State state){
		Vector2 pos = state.position;
		pos += new Vector2(-0.1f, 0.1f);
		transform.position = pos;
		state.position = pos;
	}
	public void moveUpRight(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.1f, 0.1f);
		transform.position = pos;
		state.position = pos;
	}
	public void moveDownLeft(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(-0.1f, -0.1f);
		transform.position = pos;
		state.position = pos;
	}
	public void moveDownRight(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.1f, -0.1f);
		transform.position = pos;
		state.position = pos;
	}
	public void moveUp(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.0f, 0.1f);
		transform.position = pos;
		state.position = pos;
	}
	public void moveDown(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.0f, -0.1f);
		transform.position = pos;
		state.position = pos;
	}
	public void moveLeft(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(-0.1f, 0.0f);
		transform.position = pos;
		state.position = pos;
	}
	public void moveRight(State state){
		Vector2 pos = transform.position;
		pos += new Vector2(0.1f, 0.0f);
		transform.position = pos;
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
		/*Type AImove = typeof(AIBehavior);
		MethodInfo action = AImove.GetMethod (name);
		if (action != null)
			action.Invoke (AI, 0f);*/
	}
}

	