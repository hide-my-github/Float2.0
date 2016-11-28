using UnityEngine;
using System.Collections;
//using System.Reflection;
using System.Collections.Generic;

public class State: MonoBehaviour{

	public GameObject AI;
	public Vector2 position;

	public State(Vector2 pos) {
		position = pos;
	}



	// Use this for initialization
	void Start () {
	}
		

	public List<string> legal_moves () {
		List<string> good_moves = new List<string> ();
		good_moves.Add ("MoveUpLeft");
		good_moves.Add ("MoveUpRight");
		good_moves.Add ("MoveDownLeft");
		good_moves.Add ("MoveDownRight");
		good_moves.Add ("MoveUp");
		good_moves.Add ("MoveDown");
		good_moves.Add ("MoveLeft");
		good_moves.Add ("MoveRight");
		good_moves.Add ("DoNothing");
		return good_moves;
	}

	public bool isAlive() {
		if (AI.activeInHierarchy) {
			return true;
		} else {
			return false;
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
