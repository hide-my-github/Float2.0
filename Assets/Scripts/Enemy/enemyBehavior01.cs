using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;

public class enemyBehavior01 : MonoBehaviour {
	GameObject enePreFab;
    public int health;
    float speed;
	public Text enemyText;
	int enemyCount;

	enemySpawn thisList;
 
	// Use this for initialization
	void Awake() {
		enemyText = GameObject.Find ("EnemyCount").GetComponent<Text> ();
		enemyCount = int.Parse (enemyText.text) + 1;
		enemyText.text = enemyCount.ToString ();
	}
	void Start () {
		enePreFab = GameObject.Find ("EnemyBullet");
        speed = -5f;
		thisList = GetComponent<enemySpawn> ();
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
			enemyCount = int.Parse (enemyText.text) - 1;
			enemyText.text = enemyCount.ToString ();
			//thisList.listOfEnemies.Remove (this.gameObject);
			Destroy(this.gameObject);
        }
    }
		
}
