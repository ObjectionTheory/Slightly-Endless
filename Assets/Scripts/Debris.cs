using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour {

    private float delay = 1f;
    private float currentDelay;

	// Use this for initialization
	void Start () {
        currentDelay = Time.time + delay;

        transform.localScale = new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), Random.Range(0.5f, 2));

	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > currentDelay)
        {
            Destroy(gameObject);
        }
	}
}
