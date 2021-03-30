using UnityEngine;
using System.Collections;

public class LookPlayer : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(playerPosition);

        transform.Rotate(new Vector3(0, 180, 0));
    }
}
