using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Astar : MonoBehaviour {

	public State State;
	public State newState;
	//Queue
	frontier = [];
		
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

	// Use this for initialization
	void Start () {
		State = gameObject.GetComponent<State> ();
		//newState = gameObject.GetComponent<State> ();
		legal_actions = null;
		newPos = new Vector2 ();
	}

	public string Aalgorithm(State state) {
		State initial_state = state.copy ();
		came_from.Add (initial_state, null);
		came_from_name.Add (initial_state, "");
		cost_so_far.Add (initial_state, 0);
		path.Add ("");
		heappush(frontier, (0, "start", initial_state))

		//while time() - start_time < limit { //if fixed step doesnt properly do what we want
		while (frontier not empty) {
			priority, state_name, current_state = heappop(frontier);
			//print(current_state)
			if (current_state.position.y == enemyblahblah) {	//some end goal like if max damage
				F_name = state_name
				F_state = current_state

				while came_from[F_state]:
					path.Add((F_state, F_name))
					F_state = came_from[F_state]
					F_name = came_from_name[F_state]

				path.append((F_state, F_name))
				path.reverse()

				return path
			}
				
			legal_actions = current_state.legal_moves();

			for (var j = legal_actions.GetEnumerator (); j.MoveNext ();) {
				//NEXT = Name, State effected by action, and Time cost
				action_name = j.Current;
				newState = current_state;
				newState.apply_move(action_name);
				newPos = newState.position;
				distance = Vector2.Distance (current_state.position, newState.position);

				new_cost = cost_so_far[current_state] + distance;

				if (cost_so_far[newState] == null || new_cost < cost_so_far[newState]) {
					cost_so_far[newState] = new_cost;
					priority = new_cost + heuristic(current_state, newState);
					heappush(frontier, (priority, newState))
					came_from[newState] = current_state;
					came_from_name[newState] = action_name
				}
			}
		}
		//Failed to find a path
		return ("");
	}

	//Almost like P5
	private float heuristic(State current, State newState) {

		if (!newState.isAlive()) {
			return Mathf.Infinity;
		}
		Vector2 current_pos = current.position;
		//copy = current_state;
		//copy2_name = action[0];
		//copy2 = action[1].copy();

		//check first if enemy x position is around same position?
		if {
			return inf;
		}
		return 0;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
