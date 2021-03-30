using UnityEngine;
using System.Collections;

public class CheckAnswer : MonoBehaviour {
    public static string answerSave_string;
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "english(Clone)") {
            answerSave_string = answerSave_string + col.gameObject.tag;
        }
    }
}
