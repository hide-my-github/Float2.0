using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hitsScript : MonoBehaviour {

    public Text text;
    private int hits;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Hits: " + hits;
	}

    public void incrementHits(){
        hits++;
    }
}
