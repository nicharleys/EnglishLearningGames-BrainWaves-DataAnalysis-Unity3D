using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MindSave_Game : MonoBehaviour {
    [SerializeField] Text connectStatus_Text;
    [SerializeField] Text showMindValue_Text;
    [SerializeField] MindGear mindGear;
    [SerializeField] MindLink mindLink;
    //0 : attention, 1 : meditation. 2 : delta, 3 : theta, 4 : lowAlpha, 5 : highAlpha, 6 : lowBeta, 7 : highBeta. 8 : lowGamma, 9 : highGamma
    [HideInInspector] public float[] saveMindValue = new float[10];
    string[] mindValueName = new string[11] { "poorSignal :", "attention :", "meditation :", "delta :", "theta :", "lowAlpha :", "highAlpha :", "lowBeta :", "highBeta :", "lowGamma :", "highGamma :" };

    thinkdata mindData;
    List<thinkdata> _thinkDataListFull = new List<thinkdata>();
    void Start() {
        mindLink.Autolinkmind();
    }
    void Update() {
        ShowMindValue();
    }
    private void ShowMindValue() {
        int mindValueIndex = 0;
        if(connectStatus_Text.text == "Connected" && mindGear.mindValue != null) {
            mindValueIndex = 0;
            mindData = new thinkdata(mindGear.mindValue.Keys.Count);
            for(int i = 0; i < 10; i++) {
                saveMindValue[i] = 0;
            }
            showMindValue_Text.text = "";
            foreach(string key in mindGear.mindValue.Keys) {
                float value = (float)Convert.ChangeType(mindGear.mindValue[key], typeof(float));
                mindData.dataValue_f[mindValueIndex++] = value;
            }

            showMindValue_Text.text += mindValueName[0] + mindData.dataValue_f[0] + Environment.NewLine;
            for(int i = 0; i < 11; i++) {
                saveMindValue[i] = mindData.dataValue_f[i + 1];
                showMindValue_Text.text += mindValueName[i + 1] + mindData.dataValue_f[i + 1] + Environment.NewLine;
            }
            _thinkDataListFull.Add(mindData);
        }
    }
    public class thinkdata {
        public float[] dataValue_f;
        public thinkdata(int keyCount) {
            dataValue_f = new float[keyCount];
        }
    }
}
