using UnityEngine;
using System.Collections;

public class TrackController : MonoBehaviour {

    public GameObject player;

    private float offset;
	
	void Start () {
        offset = transform.position.z - player.transform.position.z;

    }

    void LateUpdate()
    {
        transform.position = new Vector3(0, 0, player.transform.position.z + offset);
    }

}
