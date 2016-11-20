using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timeScript : MonoBehaviour
{

    public Text text;
    private float time;
    private float roundTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        roundTime = Mathf.Round(time);
        text.text = "Time: " + roundTime;
    }
}
