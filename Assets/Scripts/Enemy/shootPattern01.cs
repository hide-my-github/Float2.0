using UnityEngine;
using System.Collections;

public class shootPattern01 : MonoBehaviour {

	public GameObject launchPrefab;

	float nextFire = 2.0f;
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
				for (int i = 0; i < 360; i += 18) {
					GameObject launchThis = (GameObject)Instantiate (launchPrefab, transform.position, transform.rotation);
					nextFire = 2.0f;
					Quaternion rotation = Quaternion.Euler(0,0,i);
					Debug.Log ("rotation: " + rotation);
					Vector3 targetDir = rotation * transform.up;
					Debug.Log ("Direction: " + targetDir);
					targetDir.Normalize ();
					targetDir *= fireVelocity;
					launchThis.GetComponent<Rigidbody2D> ().velocity = targetDir;
					launchThis.transform.up = launchThis.transform.GetComponent<Rigidbody2D> ().velocity;

					launchThis.GetComponent<Rigidbody2D> ().isKinematic = false;
					launchThis.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
					Physics2D.IgnoreCollision (launchThis.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());
				}
			}
		}
	}
}
