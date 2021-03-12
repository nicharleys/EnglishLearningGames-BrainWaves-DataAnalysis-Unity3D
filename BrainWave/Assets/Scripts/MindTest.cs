using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MindTest : MonoBehaviour {
    [SerializeField] MindSave mindSave;
    [SerializeField] Renderer ballColor_Renderer;
    [SerializeField] Button startTest_Button;
    [SerializeField] Text showAttentionTestTime_Text;

    int levelValue_i = 0;
    int attentionSum_i = 0;
    int userAttentionlevel_i = 0;
    int attentionTestAmount_i = 0;
    int attentionTimeSave_i;
    int attentionTimeSum_i;
    int attentionTimeAvg_i;
    int attentionAvgTime_i;
    int timer_i = 0;

    bool countSeconds_Bool = false;
    bool resetTest = false;
    bool startTestAttention = false;
    bool attentionUIClose_Bool = false;
    bool changeButtonFunction_Bool = false;

    string saveAttentionLevel_String = "";

    void Start () {
	
	}
	void Update () {
        TestAttention();
    }
    public void NextText() {
        if (changeButtonFunction_Bool == true) {
            //savecom.text = givecom.text;
            //close2.OnApplicationQuit2();
            //Minds2C.GetComponent<minds2thinkgear>().enabled = false;
            //Minds2C.GetComponent<ThinkGearController>().enabled = false;
            //Minds3.SetActive(true);
            //Presetcom3.text = "";
            //fakecom2.text = savecom.text;

            //Minds2.SetActive(false);
            //link.linkmind3();
        }
    }
    private void TestAttention() {
        if (resetTest == true) {
            ballColor_Renderer.material.color = Color.white;
            attentionUIClose_Bool = true;
            startTestAttention = true;
        }
        if (startTestAttention == true && mindSave.saveMindValue[0] > 0) {
            if (countSeconds_Bool == false) {
                StartCoroutine(TimerCount());
                countSeconds_Bool = true;
            }
            AttentionAccumulate();
            ChangeBallColor();
        }
    }
    private void AttentionAccumulate() {
        if (mindSave.saveMindValue[0] >= (40 - levelValue_i) && mindSave.saveMindValue[0] < (65 - levelValue_i)) attentionSum_i += 4;
        else if (mindSave.saveMindValue[0] >= (65 - levelValue_i) && mindSave.saveMindValue[0] < 100) attentionSum_i += 7;
        else if (mindSave.saveMindValue[0] == 100) attentionSum_i += 10;
        else if (mindSave.saveMindValue[0] <= (25 - levelValue_i) && attentionSum_i >= 0) attentionSum_i -= 4;
        else if (mindSave.saveMindValue[0] <= (15 - levelValue_i) && attentionSum_i >= 0) attentionSum_i -= 7;
        else if (mindSave.saveMindValue[0] <= (10 - levelValue_i) && attentionSum_i >= 0) attentionSum_i -= 10;
    }
    private void ChangeBallColor() {
        if (attentionSum_i < 125 && attentionSum_i >= 0) ballColor_Renderer.material.color = Color.white;
        else if (attentionSum_i < 250 && attentionSum_i >= 125) ballColor_Renderer.material.color = Color.blue;
        else if (attentionSum_i < 375 && attentionSum_i >= 250) ballColor_Renderer.material.color = Color.green;
        else if (attentionSum_i < 500 && attentionSum_i >= 375) ballColor_Renderer.material.color = Color.yellow;
        else if (attentionSum_i >= 500) {
            ballColor_Renderer.material.color = Color.red;
            TestTime();
        }
    }
    private void TestTime() {
        attentionTestAmount_i += 1;
        attentionTimeSave_i = timer_i;
        attentionTimeSum_i += attentionTimeSave_i;
        attentionTimeAvg_i = attentionTimeSum_i / 3;
        timer_i = 0;
        resetTest = false;
        startTestAttention = false;
        timer_i = 0;
        attentionSum_i = 0;
        PerformRetest();
    }
    private void PerformRetest() {
        if (attentionTestAmount_i == 3) {
            if (attentionTimeAvg_i > 10) {
                CountLevel(5, -1);
            }
            else if (attentionTimeAvg_i < 5) {
                CountLevel(-5, 1);
            }
            else if (attentionTimeAvg_i <= 10 && attentionTimeAvg_i >= 5) {
                DetermineLevel();
            }
        }
        else if (attentionTestAmount_i < 3) {
            showAttentionTestTime_Text.text = "花費時間:" + attentionTimeSave_i;
            startTest_Button.GetComponentInChildren<Text>().text = "開始測試";
            attentionUIClose_Bool = false;
            startTest_Button.enabled = true;
        }
    }
    private void CountLevel(int levelValue, int attentionLevel) {
        attentionTestAmount_i = 0;
        levelValue_i = levelValue_i + levelValue;
        userAttentionlevel_i = userAttentionlevel_i + attentionLevel;
        showAttentionTestTime_Text.text = "花費時間:" + attentionTimeSave_i;
        attentionTimeSave_i = 0;
        attentionTimeSum_i = 0;
        attentionTimeAvg_i = 0;
        startTest_Button.GetComponentInChildren<Text>().text = "重新測試";
        attentionUIClose_Bool = false;
        startTest_Button.enabled = true;
    }
    private void DetermineLevel() {
        if (userAttentionlevel_i <= -2) saveAttentionLevel_String = "F";
        if (userAttentionlevel_i == -1) saveAttentionLevel_String = "E";
        if (userAttentionlevel_i == 0) saveAttentionLevel_String = "D";
        if (userAttentionlevel_i == 1) saveAttentionLevel_String = "C";
        if (userAttentionlevel_i == 2) saveAttentionLevel_String = "B";
        if (userAttentionlevel_i == 3) saveAttentionLevel_String = "A";
        if (userAttentionlevel_i >= 4) saveAttentionLevel_String = "S";
        showAttentionTestTime_Text.text = "花費時間:" + attentionTimeSave_i;
        startTest_Button.GetComponentInChildren<Text>().text = "測試放鬆";
        attentionUIClose_Bool = true;
        changeButtonFunction_Bool = true;
        startTest_Button.enabled = true;
    }
    private IEnumerator TimerCount() {
        yield return new WaitForSeconds(1);
        timer_i++;
        countSeconds_Bool = false;
    }
}
