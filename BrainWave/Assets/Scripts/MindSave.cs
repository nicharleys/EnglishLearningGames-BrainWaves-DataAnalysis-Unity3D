using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MindSave : MonoBehaviour {
    [SerializeField] Text connectStatus_Text;
    [SerializeField] Text showMindValue_Text;
    [SerializeField] MindGear mindGear;

    //0 : attention, 1 : meditation. 2 : delta, 3 : theta, 4 : lowAlpha, 5 : highAlpha, 6 : lowBeta, 7 : highBeta. 8 : lowGamma, 9 : highGamma
    float[] saveMindValue = new float[10];
    string[] mindValueName = new string[11] { "poorSignal :", "attention :", "meditation :", "delta :", "theta :", "lowAlpha :", "highAlpha :", "lowBeta :", "highBeta :", "lowGamma :", "highGamma :" };
    bool stopPostPartMind_Bool = false;
    bool stopPostAllMind_Bool = false;

    thinkdata mindData;
    List<thinkdata> _thinkDataListFull = new List<thinkdata>();

    string saveMind1_Web = "http://192.192.246.184/~u1025104/Unity/mindsave.php";
    string saveMind2_Web = "http://192.192.246.184/~u1025104/Unity/mindsave2.php";
    string saveMind3_Web = "http://192.192.246.184/~u1025104/Unity/mindsave3.php";
    void Update() {
        ShowMindValue();
    }
    private void ShowMindValue() {
        int mindValueIndex = 0;
        if (connectStatus_Text.text == "Connected" && mindGear.mindValue != null) {
            mindValueIndex = 0;
            mindData = new thinkdata(mindGear.mindValue.Keys.Count);
            for (int i = 0; i < 10; i++) {
                saveMindValue[i] = 0;
            }
            showMindValue_Text.text = "";
            foreach (string key in mindGear.mindValue.Keys) {
                float value = (float)Convert.ChangeType(mindGear.mindValue[key], typeof(float));
                mindData.dataValue_f[mindValueIndex++] = value;
            }

            showMindValue_Text.text += mindValueName[0] + mindData.dataValue_f[0] + Environment.NewLine;
            for (int i = 0; i < 11; i++) {
                saveMindValue[i] = mindData.dataValue_f[i + 1];
                showMindValue_Text.text += mindValueName[i + 1] + mindData.dataValue_f[i + 1] + Environment.NewLine;
            }
            PostMindValue();
            _thinkDataListFull.Add(mindData);
        }
    }
    private void PostMindValue() {
        if (saveMindValue[0] < 15 && saveMindValue[0] > 0) {
            PostOptions(stopPostPartMind_Bool, saveMind1_Web);
        }
        if (saveMindValue[0] > 90) {
            PostOptions(stopPostPartMind_Bool, saveMind2_Web);
        }
        if (saveMindValue[0] > 0) {
            PostOptions(stopPostAllMind_Bool, saveMind3_Web);
        }
    }
    private void PostOptions(bool stopPost, string saveMindWeb) {
        if (stopPost == false) {
            stopPost = true;
            StartCoroutine(UploadMind(saveMindWeb));
            StartCoroutine(DelayPost(stopPost));
        }
    }
    IEnumerator UploadMind(string uploadMind_web) {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        string[] postMindName = new string[10] { "att", "med", "del", "the", "la", "ha", "lb", "hb", "lg", "hg" };
        for (int i = 0; i < 10; i++) {
            data.Add(postMindName[i], "" + saveMindValue[i]);
        }
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(uploadMind_web, form);
        yield return www;
    }
    IEnumerator DelayPost(bool stopPost) {
        yield return new WaitForSeconds(2);
        stopPost = false;
    }
    public class thinkdata {
        public float[] dataValue_f;
        public thinkdata(int keyCount) {
            dataValue_f = new float[keyCount];
        }
    }
}
