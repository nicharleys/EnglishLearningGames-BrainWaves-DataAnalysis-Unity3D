using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MindLink : MonoBehaviour {
    [SerializeField] Text connectStatus_Text;
    [SerializeField] Text showMindValue_Text;
    [SerializeField] Text comPortPlaceholder_Text;
    [SerializeField] Button link_Button;
    [SerializeField] public InputField comPort_InputField;
    bool linkStart_Bool = false;
    bool tab_Bool = false;
    void Update() {
        if (Input.GetButtonDown("Tab")) {
            if (tab_Bool == false) {
                tab_Bool = true;
                showMindValue_Text.enabled = true;
            }
            else {
                tab_Bool = false;
                showMindValue_Text.enabled = true;
            }
        }
    }
    public void LinkMind() {
        if (linkStart_Bool == false) {
            link_Button.GetComponentInChildren<Text>().text = "取消";
            comPort_InputField.enabled = false;
            connectStatus_Text.text = "Connecting...";
            SendMessage("OnHeadsetConnectionRequest", comPort_InputField.text);
            PlayerPrefs.SetString("comPort", comPort_InputField.text);
            StartCoroutine(DelayEnter(true));
        }
        else {
            comPortPlaceholder_Text.text = @"\\.\COM1";
            link_Button.GetComponentInChildren<Text>().text = "連線";
            comPort_InputField.enabled = true;
            SendMessage("OnHeadsetDisconnectionRequest");
            showMindValue_Text.text = "";
            connectStatus_Text.text = "Disonnect";
            PlayerPrefs.SetString("comPort", "");
            StartCoroutine(DelayEnter(false));
        }
    }
    public void Autolinkmind() {
        link_Button.GetComponentInChildren<Text>().text = "取消";
        comPort_InputField.enabled = false;
        connectStatus_Text.text = "Connecting...";
        SendMessage("OnHeadsetConnectionRequest", PlayerPrefs.GetString("comPort"));
        comPort_InputField.text = PlayerPrefs.GetString("comPort");
        StartCoroutine(DelayEnter(true));

    }
    IEnumerator DelayEnter(bool linkStart) {
        link_Button.enabled = false;
        yield return new WaitForSeconds(1);
        linkStart_Bool = linkStart;
        link_Button.enabled = true;
    }
}
