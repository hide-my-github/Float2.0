using UnityEngine;
using System.Collections;

public class enemyBehavior01 : MonoBehaviour {

    public int health;
    float speed;
 
	// Use this for initialization
	void Start () {
        //negative so it's traveling downward
        speed = -5f;
	}

    // Update is called once per frame
    void Update () {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + (speed * Time.deltaTime));
        transform.position = position;
	}

    //FPS
    void FixedUpdate(){

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("HIT");
        Destroy(col.gameObject);
        health--;
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
}
