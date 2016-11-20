using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node: MonoBehaviour {


	public Node parent;
	public string parent_action; //Either this is a vector2 or a string of the name
	//key = string for function, action name in AIBehavior, value = transformed_position, or Vector2
	public Dictionary<string, Node> child_nodes = new Dictionary<string, Node> ();
	public List<string> untried_actions = new List<string> ();
	public int visits;
	public float damage;


	public Node (Node par, string par_act, List<string> act_list) {
		parent = par;
		parent_action = par_act;
		untried_actions = act_list;
	}

	public Node () {
		parent = null;
		parent_action = null;
		untried_actions = null;
		visits = 0;
		damage = 0;
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
