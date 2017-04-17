using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float score;
    public List<Image> scoreUnits;

    public float maxSpeed = 100;
    public float colourChangeDelay = 0.5f;
    public float acceleration = 5;

    public GameObject debris;
    public float debrisCount;

    private float currentDelay = 0f;
    private bool hitObstacle = false;
    private float speed = 100;
    private GameObject collided;
    private bool dead = false;
    

    void Start  ()
    {
        score = 100;
        updateScore();
    }

	// Update is called once per frame
	void Update () {
        checkColourChange();
        speedUp();
        updateScore();
        
    }  

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Debug.Log(moveHorizontal);

        Vector3 movement = new Vector3(moveHorizontal * 2, 0, speed);

        
        transform.Translate(moveHorizontal, 0, Time.deltaTime * speed);
        transform.Rotate(new Vector3(0, 0, moveHorizontal * 2));        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            speed = 0;
            hitObstacle = true;
            currentDelay = Time.time + colourChangeDelay;
            collided = other.gameObject;
            collided.SetActive(false);

            score -= 100;

            for (int i = 0; i < debrisCount; i++)
            {
                GameObject obj = (GameObject)Instantiate(debris);

                obj.transform.position = collided.transform.position + new Vector3(0, 5, -5);
                obj.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                obj.SetActive(true);
            }

        }
    }

    void checkColourChange()
    {
        if (hitObstacle)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(169, 30, 79, 255);
            if (Time.time > currentDelay)
            {
                gameObject.GetComponent<Renderer>().material.color = new Color32(30, 88, 169, 255);
                hitObstacle = false;
                

                
            }
        }
    }

    void speedUp()
    {
        if (speed < maxSpeed && !hitObstacle && !dead)
        {
            speed  = speed + acceleration;
        }
    }

    void updateScore()
    {
        int times = Mathf.FloorToInt(score / 100);
        for (int i = 0; i < scoreUnits.Count; i++) 
        {
            scoreUnits[i].enabled = false;
        }
        for (int i = 0; i < times; i++)
        {
            scoreUnits[i].enabled = true;
        }
    }

    public void kill()
    {
        dead = true;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddExplosionForce(100, transform.position - new Vector3(0, 1, 0), 5);
        speed = 0;
    }
}
