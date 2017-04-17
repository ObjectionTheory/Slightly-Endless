using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public float score;

    private GameObject player;

    private float speed;
    private float height;
    private float initialHeight;
    

    private float[] possibleHeights = new float[3] { 3, 6, 9 };

    // Use this for initialization
    void Start () {
        reset();
    }
	
	// Update is called once per frame
	void Update () {
        raise();

        player = GameObject.Find("Ship");

        if (transform.position.z + 20 < player.transform.position.z)
        {
            gameObject.SetActive(false);
        }
        if (transform.position.z + 1 < player.transform.position.z && player.GetComponent<PlayerController>().score + score <= 500)
        {
            player.GetComponent<PlayerController>().score += score;
            score = 0;
        }
    }

    void raise()
    {
        
        if (transform.localScale.z < height)
        {
            transform.localScale = (new Vector3(10, 4, initialHeight));
            initialHeight += speed;
        }
    }

    public void reset()
    {
        int choice = Random.Range(0, 2);
        height = possibleHeights[choice];
        initialHeight = 0.01f;
        speed = height / 10;

        score = (choice + 1) * 10;

        transform.localScale = new Vector3(10, 4, initialHeight);
    }

  
}
