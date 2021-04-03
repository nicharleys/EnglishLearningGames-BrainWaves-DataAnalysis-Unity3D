using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigibody;
    [HideInInspector] public bool controllPlayerRun_Bool = false;
    [HideInInspector] public bool controllPlayerWalk_Bool = false;
    float inputH;
    float inputV;

    void Update() {
        if(controllPlayerWalk_Bool == false) {
            walk();
        }
    }
    void walk() {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        animator.SetFloat("inputH", inputH);
        animator.SetFloat("inputV", inputV);
        animator.SetBool("run", controllPlayerRun_Bool);

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 50f * Time.deltaTime;

        if(moveZ <= 0f) {
            moveX = 0f;
        }
        else if(controllPlayerRun_Bool) {
            moveX *= 3f;
            moveZ *= 3f;
        }
        rigibody.velocity = new Vector3(moveX, 0f, moveZ);
    }
}

