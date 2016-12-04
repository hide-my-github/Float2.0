using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Linq;
using System.Text;
using Priority_Queue;

public class Astar: MonoBehaviour {

	public GameObject Enemy;
	public GameObject EnemyBullet;
	//The class to be enqueued.
	public class Info
	{
		public string action_name { get; private set;}
		public State info_state { get; private set;}
		public Info(string name, State state)
		{
			action_name = name;
			info_state = state;
		}
	}
	public AIBehavior AIBehavior;
	//public State State;
	//public State newState;
	//Queue
	SimplePriorityQueue<Info> frontier;

	public List<string> path;
	List<string> path_maker;
	public Dictionary<State, State> came_from;
	public Dictionary<State, string> came_from_name; //string name of action that brought it to that state
	public Dictionary<State, float> cost_so_far; //distance
	public Dictionary<State, int> steps_so_far;

	public List<string> good_moves;
	string action_name = null;
	float distance = 0.0f;
	float new_cost = 0.0f;
	float priority = 0.0f;
	Vector2 current_pos;
	string state_name = null;
	State current_state;
	State initial_state;
	public enemySpawn eneScript;
	public List<GameObject> eneList;

	Collider2D[] colliders;
	Collider2D testcol;
	Vector2 testpos;
	Vector2 colliderpos;
	//List<Collider2D> colliders;
	int i = 0;
	int count;
	// Use this for initialization
	void Start () {

		frontier = new SimplePriorityQueue<Info>();

		came_from = new Dictionary<State, State> ();
		came_from_name = new Dictionary<State, string> ();
		cost_so_far = new Dictionary<State, float> ();
		steps_so_far = new Dictionary<State, int> ();
		path = new List<string> ();
		path_maker = new List<string> ();

		//colliders = new Collider2D[300]; //or some large allocation for space
		//colliders = new List<Collider2D> ();
		good_moves = new List<string>();
		good_moves.Add ("moveUpLeft");
		good_moves.Add ("moveUpRight");
		good_moves.Add ("moveDownLeft");
		good_moves.Add ("moveDownRight");
		good_moves.Add ("moveUp");
		good_moves.Add ("moveDown");
		good_moves.Add ("moveLeft");
		good_moves.Add ("moveRight");
		good_moves.Add ("doNothing");

	}


	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		eneList = eneScript.listOfEnemies;

		//left wall
		if (this.transform.position.x <= -5.75f) {
			good_moves.Remove ("moveUpLeft");
			good_moves.Remove ("moveLeft");
			good_moves.Remove ("moveDownLeft");
		}
		//right wall
		else if (this.transform.position.x >= 5.75f) {
			good_moves.Remove ("moveUpRight");
			good_moves.Remove ("moveRight");
			good_moves.Remove ("moveDownRight");
		}
		//top wall
		else if (this.transform.position.y >= 4.50f) {
			good_moves.Remove ("moveUpLeft");
			good_moves.Remove ("moveUp");
			good_moves.Remove ("moveUpRight");
		}
		//bottom wall
		else if (this.transform.position.y <= -4.50f) {
			good_moves.Remove ("moveDownLeft");
			good_moves.Remove ("moveDown");
			good_moves.Remove ("moveDownRight");
		} else {
			good_moves.Clear ();
			good_moves.Add ("moveUpLeft");
			good_moves.Add ("moveUpRight");
			good_moves.Add ("moveDownLeft");
			good_moves.Add ("moveDownRight");
			good_moves.Add ("moveUp");
			good_moves.Add ("moveDown");
			good_moves.Add ("moveLeft");
			good_moves.Add ("moveRight");
			good_moves.Add ("doNothing");
		}
	}

	public List<string> Aalgorithm(State state) {

		initial_state = new State(state.position);
		came_from[initial_state] = null;
		came_from_name[initial_state] = "";
		cost_so_far[initial_state] = 0.0f;
		steps_so_far[initial_state] = 0;
		path.Add ("");
		Info initial = new Info ("", initial_state);
		frontier.Enqueue(initial, 0);
		count = 0;
		//eneList = eneScript.listOfEnemies;

		//while time() - start_time < limit { //if fixed step doesnt properly do what we want
		while (count < 10000) {
			if (count == 9999) {
				Debug.Log ("rip in pasta");
			}
			count++;
			Info current_info = frontier.Dequeue ();
			state_name = current_info.action_name;
			current_state = current_info.info_state;
			current_pos = current_state.position;

			/*if (Enemy.activeSelf == true) {
				Debug.Log ("Enemy Active");
			} else {
				Debug.Log ("Enemy Not active");
			}*/
			//Debug.Log (eneList.Count);
			// 2 exit conditions; one checking for an enemy, another for no enemy in which case, remain in place dodging
			Vector2 ene_pos = Enemy.transform.position;
			if (steps_so_far[current_state] == 3) {	
				if (count > 1000) {
					Debug.Log ("iteration: " + count);
				}
				path = creatingPath (current_state, state_name);
				return path;
				/*
				List<string> temp = new List<string>();
				temp.Add ("moveUp");
				temp.Add ("moveRight");
				return temp;
				*/
			}
			GameObject[] bulletArray = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			//Debug.Log ("Prior: " + bulletArray.Length);
			List<GameObject> nearbyThreats = keepThreatsIntoList (current_pos, bulletArray);
			 
			/*
			Vector2 dodge_pos = transform.position;
			if (current_pos == dodge_pos && !Enemy.activeSelf) {
				Debug.Log ("Path B");
				path = creatingPath (current_state, state_name);
				return path;
			}*/
			//path.Add(F_name); //gives "" dunno if want
			//print(current_state)

			for (var j = good_moves.GetEnumerator (); j.MoveNext ();) {
				//NEXT = Name, State effected by action, and Time cost
				action_name = j.Current;
				//Debug.Log (action_name);
				State newState = new State(current_state.position);
				AIBehavior.apply_move(action_name, newState);
				// Simulate state here
				//Debug.Log (newState.position);
				float distance = Vector2.Distance(newState.position, current_state.position);
				new_cost = cost_so_far[current_state] + distance;

				float testValue;
				if ((cost_so_far.TryGetValue(newState, out testValue) == false) || new_cost < cost_so_far [newState]) {
					cost_so_far[newState] = new_cost;
					priority = new_cost + heuristic(current_state, newState, ene_pos, nearbyThreats, steps_so_far[current_state]+1, action_name);
					Info newer_info = new Info(action_name, newState);
					steps_so_far[newState] = steps_so_far[current_state]+1;
					came_from[newState] = current_state;
					came_from_name[newState] = action_name;
					frontier.Enqueue(newer_info, priority);
				}
			}



		}
		frontier.Clear ();
		//path = creatingPath (current_state, state_name);
		return path;
	}
	//Almost like P5
	private float heuristic(State current, State newState, Vector2 ene_position, List<GameObject> nearby_enemies, int steps, string action) {
		float STEP_TIME = 0.02f;
		float COLLISION_DIST = 1.00f;
		float GREEDY_HIT = 10000f;
		//for group of bullets around playerL
		//if any have same position, return inf
		//Debug.Log("current: " + current.position);
		//Debug.Log ("new: " + newState.position);

		float output = 0;
		// Check if hit
		for (var i = nearby_enemies.GetEnumerator (); i.MoveNext ();) {
			GameObject test_ene = i.Current;
			Vector2 vel = test_ene.GetComponent<Rigidbody2D>().velocity;
			Vector2 pos = test_ene.transform.position;

			Vector2 est_pos = pos + (vel * (STEP_TIME * steps));

			//Debug.Log (steps + ": " + pos + " -> " + est_pos + " (" + vel + ")");

			float dist = Vector2.Distance(newState.position, est_pos);
			if (dist <= COLLISION_DIST) {
				Debug.Log ("memes");
				return output + GREEDY_HIT;
			}
		}

		//need a collider check to see if any actually collide with main 

		//Debug.Log ("distance: " + distance);
		return output;
	}

	private float time() {
		return Time.deltaTime;
	}

	private List<string> creatingPath(State current, string name) {
//		Debug.Log ("HUH?");
		path_maker.Clear ();
		string F_name = name;
		//State F_state = current_state;
		State F_state = current;

		while (came_from[F_state] != null) {
			path_maker.Add(F_name);
			F_state = came_from[F_state];
			F_name = came_from_name[F_state];
		}
		path_maker.Reverse();
		frontier.Clear ();
		return path_maker;
	}

	private List<GameObject> keepThreatsIntoList(Vector2 pos, GameObject[] array){
		List<GameObject> list = new List<GameObject>();
		//Vector3 v3pos = new Vector3 (pos.x, pos.y, 0);
		foreach (GameObject bullet in array) {
			float dist = Vector2.Distance (pos, bullet.transform.position);
			if (dist <= 3.0f) {
				list.Add(bullet);
			}
		}
		return list; 
	}
}