using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour {
    [HideInInspector] public bool controllCantCatch_Bool;
    public void ShowCantCatch(Text cantCatch) {
        if(controllCantCatch_Bool == false) {
            controllCantCatch_Bool = true;
            StartCoroutine(ShowMessages(cantCatch));
            controllCantCatch_Bool = false;
        }
    }
    public IEnumerator ShowMessages(Text message) {
        message.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(1);
        message.GetComponent<Text>().enabled = false;
    }
}
