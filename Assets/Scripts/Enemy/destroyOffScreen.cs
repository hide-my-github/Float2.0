using UnityEngine;
using System.Collections;

public class destroyOffScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// FPS
	void FixedUpdate () {
        //This is the bottom-left of screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		//top right
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        //Destroy poopSplatter if goes out of screen
        //Debug.Log("Ball: "+transform.position.y);
        //Debug.Log("Window: " + min.y);
		if ((transform.position.y) < min.y - 0.5f || (transform.position.y) > max.y + 0.5)
        {
            Destroy(gameObject);
        }
		else if (transform.position.x < min.x - 0.5f || transform.position.x > max.x + 0.5){
			Destroy (gameObject);
		}
    }
}
