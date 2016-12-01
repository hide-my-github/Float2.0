using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Linq;
using System.Text;
using Priority_Queue;

public class Astar : MonoBehaviour {

	public GameObject Enemy;
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
	public State State;
	//public State newState;
	//Queue
	SimplePriorityQueue<Info> frontier;
		
	public List<string> path;
	public Dictionary<State, State> came_from;
	public Dictionary<State, string> came_from_name; //string name of action that brought it to that state
	public Dictionary<State, float> cost_so_far; //distance

	List<string> legal_actions;
	string action_name = null;
	Vector2 newPos;
	float distance = 0;
	float new_cost = 0;
	float priority = 0;
	Vector2 current_pos;
	string state_name = null;
	State current_state;
	State initial_state;
	public enemySpawn eneScript;
	public List<GameObject> eneList;


	// Use this for initialization
	void Start () {

		State = gameObject.AddComponent<State> ();
		//newState = gameObject.GetComponent<State> ();
		legal_actions = null;
		newPos = new Vector2 ();
		frontier = new SimplePriorityQueue<Info>();

		came_from = new Dictionary<State, State> ();
		came_from_name = new Dictionary<State, string> ();
		cost_so_far = new Dictionary<State, float> ();
		path = new List<string> ();

	}


	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

	}
	public List<string> Aalgorithm(State state) {
		initial_state = new State(state.position);
		came_from[initial_state] = null;
		came_from_name[initial_state] = "";
		cost_so_far[initial_state] = 0;
		path.Add ("");
		Info initial = new Info ("", initial_state);
		frontier.Enqueue(initial, 0);

		eneList = eneScript.listOfEnemies;

		//while time() - start_time < limit { //if fixed step doesnt properly do what we want
		while (frontier.Count != 0) {
			Info current_info = frontier.Dequeue ();
			state_name = current_info.action_name;
			current_state = current_info.info_state;
			current_pos = current_state.position;

			// 2 exit conditions; one checking for an enemy, another for no enemy in which case, remain in place dodging
			if (eneList.Count != 0) {
				GameObject enemyTarget = eneList [0];
				Vector2 ene_pos = enemyTarget.transform.position;

				if (current_pos.x == ene_pos.x && this.gameObject.activeInHierarchy) {	
					string F_name = state_name;
					State F_state = current_state;

					while (came_from[F_state] != null) {
						path.Add(F_name);
						F_state = came_from[F_state];
						F_name = came_from_name[F_state];
					}
					path.Reverse();
					frontier.Clear ();
					return path;
				}
			}
					
			Vector2 dodge_pos = transform.position;
			if (current_pos == dodge_pos && this.gameObject.activeInHierarchy) {
				string F_name = state_name;
				State F_state = current_state;

				while (came_from[F_state] != null) {
					path.Add(F_name);
					F_state = came_from[F_state];
					F_name = came_from_name[F_state];
				}
				path.Reverse();
				frontier.Clear ();
				return path;
			}
				//path.Add(F_name); //gives "" dunno if want
				//print(current_state)
			


			legal_actions = current_state.legal_moves();

			for (var j = legal_actions.GetEnumerator (); j.MoveNext ();) {
				//NEXT = Name, State effected by action, and Time cost
				action_name = j.Current;
				Debug.Log (action_name);
				State newState = new State(current_state.position);
				AIBehavior.apply_move(action_name, newState);
				newPos = newState.position;
				distance = Vector2.Distance (current_state.position, newState.position);
				new_cost = cost_so_far[current_state] + distance;

				float testValue;
				if ((cost_so_far.TryGetValue(newState, out testValue) == false) || new_cost < cost_so_far [newState]) {
					cost_so_far[newState] = new_cost;
					priority = new_cost + heuristic(current_state, newState);
					Info newer_info = new Info(action_name, newState);
					frontier.Enqueue(newer_info, priority);
					came_from[newState] = current_state;
					came_from_name[newState] = action_name;
				}
			}
		
		//Failed to find a path
		}
		frontier.Clear ();
		return path;
	}
		//Almost like P5
	private float heuristic(State current, State newState) {

		if (!newState.isAlive()) {
				return Mathf.Infinity;
		}
		Vector2 current_heur_check = current.position;
			//copy = current_state;
			//copy2_name = action[0];
			//copy2 = action[1].copy();

			//check first if enemy x position is around same position?
			/*if {
			return Mathf.Infinity;
		}*/
		return 0;
	}
}
