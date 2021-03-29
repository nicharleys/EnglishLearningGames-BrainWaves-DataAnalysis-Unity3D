using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MindGear : MonoBehaviour {
    //0 : ifconnecteds, 1: ifconnecterrors, 2 : ifdisconnects
    [SerializeField] Text[] connectStatusPrompt_Text;
    [SerializeField] Text connectStatus_Text;
    [HideInInspector] public Hashtable mindValue;
    void Awake() {
        for (int i = 0; i < 3; i++) {
            connectStatusPrompt_Text[i].enabled = false;
        }
    }
    bool waitConnectStatus = false;

    void OnHeadsetConnected() {
        StartCoroutine(ConnectStatus(connectStatusPrompt_Text[0]));
        connectStatus_Text.text = "Connected";
    }
    void OnHeadsetConnectionError() {
        StartCoroutine(ConnectStatus(connectStatusPrompt_Text[1]));
        connectStatus_Text.text = "Disonnect";
    }
    void OnHeadsetDisconnected() {
        StartCoroutine(ConnectStatus(connectStatusPrompt_Text[2]));
        connectStatus_Text.text = "Disonnect";
    }
    void OnHeadsetDataReceived(Hashtable values) {
        mindValue = values;
    }
    public void OnApplicationQuit() {
        SendMessage("OnHeadsetDisconnectionRequest");
    }
    private IEnumerator ConnectStatus(Text connectStatusPrompt) {
        if (waitConnectStatus == false) {
            waitConnectStatus = true;
            connectStatusPrompt.enabled = true;
            yield return new WaitForSeconds(2);
            connectStatusPrompt.enabled = false;
            waitConnectStatus = false;
        }
    }
}
