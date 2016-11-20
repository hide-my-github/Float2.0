using UnityEngine;
using System.Collections;

public class playerBehavior : MonoBehaviour {

    public GameObject launchPrefab;
    float fireDelay = 3f;

    float nextFire = 0.5f;
    float fireVelocity = 5f;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
            moveUpLeft();
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
            moveUpRight();
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
            moveDownLeft();
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            moveDownRight();
        }
        else if (Input.GetKey(KeyCode.W)){
            moveUp();
        }
        else if (Input.GetKey(KeyCode.A)) {
            moveLeft();
        }
        else if (Input.GetKey(KeyCode.S)) {
            moveDown();
        }
        else if (Input.GetKey(KeyCode.D)) {
            moveRight();
        }
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

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("HIT");
        Destroy(col.gameObject);
        GameObject.Find("_SCRIPTS_").GetComponent<hitsScript>().incrementHits();
    }


    private void shoot(){
        GameObject launchThis = (GameObject)Instantiate(launchPrefab, transform.position, transform.rotation);
        launchThis.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 8.0f);
        launchThis.transform.up = launchThis.transform.GetComponent<Rigidbody2D>().velocity;

        launchThis.GetComponent<Rigidbody2D>().isKinematic = false;
        launchThis.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        Physics2D.IgnoreCollision(launchThis.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
    }

    public void moveUpLeft(){
        Vector2 pos = transform.position;
        pos += new Vector2(-0.1f, 0.1f);
        transform.position = pos;
    }
    public void moveUpRight(){
        Vector2 pos = transform.position;
        pos += new Vector2(0.1f, 0.1f);
        transform.position = pos;
    }
    public void moveDownLeft(){
        Vector2 pos = transform.position;
        pos += new Vector2(-0.1f, -0.1f);
        transform.position = pos;
    }
    public void moveDownRight(){
        Vector2 pos = transform.position;
        pos += new Vector2(0.1f, -0.1f);
        transform.position = pos;
    }
    public void moveUp(){
        Vector2 pos = transform.position;
        pos += new Vector2(0.0f, 0.1f);
        transform.position = pos;
    }
    public void moveDown(){
        Vector2 pos = transform.position;
        pos += new Vector2(0.0f, -0.1f);
        transform.position = pos;
    }
    public void moveLeft(){
        Vector2 pos = transform.position;
        pos += new Vector2(-0.1f, 0.0f);
        transform.position = pos;
    }
    public void moveRight(){
        Vector2 pos = transform.position;
        pos += new Vector2(0.1f, 0.0f);
        transform.position = pos;
    }
}
