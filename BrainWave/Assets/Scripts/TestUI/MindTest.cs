using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MindTest : MonoBehaviour {
    [SerializeField] int startFunction_i;
    [SerializeField] MindSave mindSave;
    [SerializeField] public Button startTest_Button;
    [SerializeField] public Text centerConnectStatus_Text;
    [HideInInspector] public int userLevel_i = 0;
    [HideInInspector] public bool mindUINext_Bool = false;
    [SerializeField] Renderer ballColor_Renderer;
    [SerializeField] Text showTestTime_Text;
    [SerializeField] Text connectStatus_Text;
    int levelValue_i = 0;
    int mindValueSum_i = 0;
    int mindTestAmount_i = 0;
    int timeSave_i;
    int timeSum_i;
    int timeAvg_i;
    int avgTime_i;
    int timer_i = 0;
    bool countSeconds_Bool = false;
    bool resetTest_Bool = false;
    bool startTestMind_Bool = false;
    bool mindUIClose_Bool = false;
    bool changeButtonFunction_Bool = false;
    string saveAttentionLevel_String = "";
    void Update() {
        TestMind(startFunction_i);
    }
    public void StartTest() {
        if (connectStatus_Text.text == "Connected") {
            startTest_Button.enabled = false;
            if (mindUIClose_Bool == false) {
                showTestTime_Text.text = "";
                centerConnectStatus_Text.text = "腦波儀已連線";
                startTest_Button.GetComponentInChildren<Text>().text = "測試中";
                resetTest_Bool = true;
            }
        }
        else {
            centerConnectStatus_Text.text = "腦波儀未連線";
            startTest_Button.GetComponentInChildren<Text>().text = "開始測試";
        }
    }
    private void TestMind(int mindAccumulateType) {
        if (resetTest_Bool == true) {
            Reset();
        }
        if (startTestMind_Bool == true && mindSave.saveMindValue[0] > 0) {
            StartTimeCount();
            MindAccumulate(mindAccumulateType);
        }
    }
    private void MindAccumulate(int mindAccumulateType) {
        if (mindAccumulateType == 0) {
            AttentionAccumulate();
            ChangeBallColor(Color.blue, Color.green, Color.yellow, Color.red, "測試放鬆");
        }
        if (mindAccumulateType == 1) {
            MeditationAccumulate();
            ChangeBallColor(Color.red, Color.yellow, Color.green, Color.blue, "開始遊戲");
        }
    }
    private void Reset() {
        ballColor_Renderer.material.color = Color.white;
        mindUIClose_Bool = true;
        startTestMind_Bool = true;
    }
    private void AttentionAccumulate() {
        if (mindSave.saveMindValue[0] >= (40 - levelValue_i) && mindSave.saveMindValue[0] < (65 - levelValue_i)) mindValueSum_i += 4;
        else if (mindSave.saveMindValue[0] >= (65 - levelValue_i) && mindSave.saveMindValue[0] < 100) mindValueSum_i += 7;
        else if (mindSave.saveMindValue[0] == 100) mindValueSum_i += 10;
        else if (mindSave.saveMindValue[0] <= (25 - levelValue_i) && mindValueSum_i >= 0) mindValueSum_i -= 4;
        else if (mindSave.saveMindValue[0] <= (15 - levelValue_i) && mindValueSum_i >= 0) mindValueSum_i -= 7;
        else if (mindSave.saveMindValue[0] <= (10 - levelValue_i) && mindValueSum_i >= 0) mindValueSum_i -= 10;
    }
    private void MeditationAccumulate() {
        if (mindSave.saveMindValue[1] / 1000 > (55 - levelValue_i)) mindValueSum_i += 4;
        else if (mindSave.saveMindValue[1] / 1000 > (35 - levelValue_i) && mindSave.saveMindValue[1] / 1000 <= (55 - levelValue_i)) mindValueSum_i += 3;
        else if (mindSave.saveMindValue[1] / 1000 > (25 - levelValue_i) && mindSave.saveMindValue[1] / 1000 <= (35 - levelValue_i)) mindValueSum_i += 2;
        else if (mindSave.saveMindValue[1] / 1000 >= 0 && mindSave.saveMindValue[1] / 1000 <= (15 - levelValue_i) && mindValueSum_i >= 0) mindValueSum_i -= 2;
    }
    private void ChangeBallColor(Color ballColor1, Color ballColor2, Color ballColor3, Color ballColor4, string buttoValue) {
        if (mindValueSum_i < 125 && mindValueSum_i >= 0) ballColor_Renderer.material.color = Color.white;
        else if (mindValueSum_i < 250 && mindValueSum_i >= 125) ballColor_Renderer.material.color = ballColor1;
        else if (mindValueSum_i < 375 && mindValueSum_i >= 250) ballColor_Renderer.material.color = ballColor2;
        else if (mindValueSum_i < 500 && mindValueSum_i >= 375) ballColor_Renderer.material.color = ballColor3;
        else if (mindValueSum_i >= 500) {
            ballColor_Renderer.material.color = ballColor4;
            TestTime(buttoValue);
        }
    }
    private void TestTime(string buttoValue) {
        mindTestAmount_i += 1;
        timeSave_i = timer_i;
        timeSum_i += timeSave_i;
        timeAvg_i = timeSum_i / 3;
        timer_i = 0;
        resetTest_Bool = false;
        startTestMind_Bool = false;
        timer_i = 0;
        mindValueSum_i = 0;
        PerformRetest(buttoValue);
    }

    private void PerformRetest(string buttonValue) {
        if (mindTestAmount_i == 3) {
            if (timeAvg_i > 10) {
                CountLevel(5, -1);
            }
            else if (timeAvg_i < 5) {
                CountLevel(-5, 1);
            }
            else if (timeAvg_i <= 10 && timeAvg_i >= 5) {
                DetermineLevel(buttonValue);
            }
        }
        else if (mindTestAmount_i < 3) {
            showTestTime_Text.text = "花費時間:" + timeSave_i;
            startTest_Button.GetComponentInChildren<Text>().text = "開始測試";
            mindUIClose_Bool = false;
            mindUINext_Bool = true;
            startTest_Button.enabled = true;
        }
    }
    private void CountLevel(int levelValue, int mindLevel) {
        mindTestAmount_i = 0;
        levelValue_i = levelValue_i + levelValue;
        userLevel_i = userLevel_i + mindLevel;
        showTestTime_Text.text = "花費時間:" + timeSave_i;
        timeSave_i = 0;
        timeSum_i = 0;
        timeAvg_i = 0;
        startTest_Button.GetComponentInChildren<Text>().text = "重新測試";
        mindUIClose_Bool = false;
        startTest_Button.enabled = true;
    }
    private void DetermineLevel(string buttonValue) {
        if (userLevel_i <= -2) saveAttentionLevel_String = "F";
        if (userLevel_i == -1) saveAttentionLevel_String = "E";
        if (userLevel_i == 0) saveAttentionLevel_String = "D";
        if (userLevel_i == 1) saveAttentionLevel_String = "C";
        if (userLevel_i == 2) saveAttentionLevel_String = "B";
        if (userLevel_i == 3) saveAttentionLevel_String = "A";
        if (userLevel_i >= 4) saveAttentionLevel_String = "S";
        showTestTime_Text.text = "花費時間:" + timeSave_i;
        startTest_Button.GetComponentInChildren<Text>().text = buttonValue;
        mindUIClose_Bool = true;
        changeButtonFunction_Bool = true;
        startTest_Button.enabled = true;
    }
    private IEnumerator TimerCount() {
        yield return new WaitForSeconds(1);
        timer_i++;
        countSeconds_Bool = false;
    }
    private void StartTimeCount(){
        if (countSeconds_Bool == false) {
            StartCoroutine(TimerCount());
            countSeconds_Bool = true;
        }
    }
}
