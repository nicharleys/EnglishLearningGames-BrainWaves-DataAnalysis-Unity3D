using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody rbody;
    float inputH;
    float inputV;
    bool run = false;

    void Update() {
        walk();
    }
    void walk() {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetBool("run", run);

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 50f * Time.deltaTime;

        if(moveZ <= 0f) {
            moveX = 0f;
        }
        else if(run) {
            moveX *= 3f;
            moveZ *= 3f;
        }
        rbody.velocity = new Vector3(moveX, 0f, moveZ);
    }
}

