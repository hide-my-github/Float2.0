using UnityEngine;
using System.Collections;

public class enemyShoot : MonoBehaviour {

    public GameObject target;
    public GameObject launchPrefab;

    float nextFire = 0.5f;
    float fireVelocity = 5f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //FPS
    void FixedUpdate()
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y < max.y)
        {
            nextFire -= Time.deltaTime;

            if (nextFire <= 0.0f)
            {
                //target = GameObject.Find("Player");
				target = GameObject.Find ("AI");
                GameObject launchThis = (GameObject)Instantiate(launchPrefab, transform.position, transform.rotation);
                nextFire = 0.5f;
                Vector3 targetDir = target.transform.position - transform.position;
                targetDir.Normalize();
                targetDir *= fireVelocity;
                launchThis.GetComponent<Rigidbody2D>().velocity = targetDir;
                launchThis.transform.up = launchThis.transform.GetComponent<Rigidbody2D>().velocity;

                launchThis.GetComponent<Rigidbody2D>().isKinematic = false;
                launchThis.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                Physics2D.IgnoreCollision(launchThis.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            }
        }
    }
}
