using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    
    public GameObject wall;
    public int dist = 100;
    public GameObject panel;

    private int counter;
    private float[] wallPositions = new float[4] { -15, -5, 5, 15 };
    
    private int number;
    private int randomWall;

    private GameObject player;
    private PlayerController playerScript;
    private float lastPlayerPos;
    private bool canPlace = false;

    private bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Ship");
        lastPlayerPos = player.transform.position.z;

        Physics.IgnoreLayerCollision(0, 8, true);
        playerScript = player.GetComponent<PlayerController>();

    }

    

    // Update is called once per frame
    void Update()
    {
        loadWalls();
        checkGameOver();
        playerScript = player.GetComponent<PlayerController>();
    }

    void loadWalls()
    {

        List<float> toInstantiate = new List<float>();
        
        number = Random.Range(2, 4);
        
        counter = (int)player.transform.position.z;

        if (counter - dist > lastPlayerPos)
        {
            lastPlayerPos = counter;
            
            while (toInstantiate.Count < number)
            {
                randomWall = Random.Range(0, 4);
                
                if (!toInstantiate.Contains(wallPositions[randomWall]))
                {
                    toInstantiate.Add(wallPositions[randomWall]);
                }
                

            }
            for (int i = 0; i < toInstantiate.Count; i++)
            {
                GameObject wallToPlace = ObjectPooler.SharedInstance.GetPooledObject();
                if (wallToPlace != null)
                {
                    wallToPlace.transform.position = new Vector3(toInstantiate[i], 0.1f, counter + dist);
                    wallToPlace.transform.rotation = Quaternion.Euler(270, 0, 0);
                    wallToPlace.GetComponent<Wall>().reset();
                    wallToPlace.SetActive(true);
                }
                    

            }

            
        }
        
    }

    void checkGameOver()
    {
        float playerScore = playerScript.score;

        if (playerScore < 0)
        {
            gameOver = true;
            playerScript.kill();
            StartCoroutine(fadeScreen(panel));
            
        }
    }

    IEnumerator fadeScreen (GameObject obj)
    {
        for (int i = 0; i <= 255; i += 2)
        {
            obj.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)i);
            yield return null;
        }
        reset();
        yield return null;
    }

    void reset()
    {

    }
}


