using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour {
    public const int ENEMY_STAND = 0;
    public const int ENEMY_WALK = 1;
    public const int ENEMY_RUN = 2;
    public const int AI_ATTACK_DISTANCE = 10;
    public const float AI_THINK_TIME = 0.5f;

    private int enemyState;
    private float backUptime;
    private GameObject player;

    void Start() {
        enemyState = ENEMY_STAND;
        player = GameObject.FindWithTag("Player");
    }
    void Update() {
        if(Time.time - backUptime >= AI_THINK_TIME) {
            backUptime = Time.time;
            int rand = Random.Range(0, 9);
            if(rand == 0) enemyState = ENEMY_STAND;
            else if(rand == 1) RotateRange(0);
            else if(rand == 2) RotateRange(90);
            else if(rand == 3) RotateRange(180);
            else if(rand == 4) RotateRange(270);
            else if(rand == 5) RotateRange(45);
            else if(rand == 6) RotateRange(135);
            else if(rand == 7) RotateRange(225);
            else if(rand == 8) RotateRange(315);
        }
        switch(enemyState) {
            case ENEMY_STAND:
                break;
            case ENEMY_WALK:
                transform.Translate(Vector3.forward * Time.deltaTime * 0.6f);
                break;
        }
        switch(enemyState) {
            case ENEMY_STAND:
                break;
            case ENEMY_RUN:
                if(Vector3.Distance(transform.position, player.transform.position) < 10) {
                    transform.Translate(Vector3.forward * Time.deltaTime * 1.8f);
                }
                break;
        }
    }
    private void RotateRange(int value) {
        Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * value, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
        enemyState = ENEMY_WALK;
    }
}