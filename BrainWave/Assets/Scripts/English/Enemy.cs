using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{

    public const int ENEMY_STAND = 0;
    public const int ENEMY_WALK = 1;
    public const int ENEMY_RUN = 2;
    //  public const int ENEMY_PAUSE = 3;
    private int enemyState;
    private float backUptime;
    public const float AI_THINK_TIME = 0.5f;

    private GameObject player;

    public const int AI_ATTACK_DISTANCE = 10;
    public bool isHatred = false;
   // private GameObject handlight;

   
    void Start()
    {
        enemyState = ENEMY_STAND;
        player = GameObject.FindWithTag("Player");

        //handlight = GameObject.FindWithTag("handlight");


    }
 
    void Update()
    {
        if (Time.time - backUptime >= AI_THINK_TIME)
        {
            backUptime = Time.time;
            int rand = Random.Range(0, 9);
            if (rand == 0)
            {
                enemyState = ENEMY_STAND;
            }
            else if (rand == 1)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 0, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }
            else if (rand == 2)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }
            else if (rand == 3)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }
            else if (rand == 4)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 270, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }
            else if (rand == 5)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 45, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }
            else if (rand == 6)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 135, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }
            else if (rand == 7)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 225, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }
            else if (rand == 8)
            {
                Quaternion rotate = Quaternion.Euler(0, Random.Range(1, 5) * 315, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
                enemyState = ENEMY_WALK;
            }

        }
        switch (enemyState)
        {
            case ENEMY_STAND:
              //  gameObject.animation.Play("idle");
                break;
            case ENEMY_WALK:
             //   gameObject.animation.Play("walk");
                transform.Translate(Vector3.forward * Time.deltaTime * 0.6f);
                break;
           
        }

       /* if (Vector3.Distance(transform.position, player.transform.position) < AI_ATTACK_DISTANCE || isHatred)
        {
            if (handlight.active == true)
            {
                //gameObject.animation.Play("run");
                enemyState = ENEMY_RUN;
               

                transform.LookAt(player.transform);

                transform.Rotate(new Vector3(0, 180, 0));
            }
            
        }*/


        switch (enemyState)
        {
            case ENEMY_STAND:
               // gameObject.animation.Play("idle");
                break;
            case ENEMY_RUN:
               
                if (Vector3.Distance(transform.position, player.transform.position) < 10)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * 1.8f);
                }
                break;
        }

    }
}