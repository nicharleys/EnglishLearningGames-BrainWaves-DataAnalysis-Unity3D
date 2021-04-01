using UnityEngine;
using System.Collections;

public class LookPlayer : MonoBehaviour {
    [SerializeField] GameObject player;
	void Update () {
        Vector3 playerPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(playerPosition);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
