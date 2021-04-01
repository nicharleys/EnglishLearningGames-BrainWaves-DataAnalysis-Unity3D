using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MindTestUI : MonoBehaviour {
    [SerializeField] GameObject[] mindUI_GameObject;
    [SerializeField] GameObject loadingUI_GameObject;
    [SerializeField] Slider loadingBar_Slider;
    [SerializeField] Text firstConnectStatus_Text;
    [SerializeField] Text firstCenterConnectStatus_Text;
    string saveComPort_String = "";
    bool closeFirstUI_Bool = false;
    AsyncOperation _async;
    MindGear mindGear;
    string saveLevel_Web = "http://192.192.246.184/~u1025104/Unity/savelevel.php";
    public void StartTest() {
        if (firstConnectStatus_Text.text == "Connected") {
            if (closeFirstUI_Bool == false) {
                closeFirstUI_Bool = true;
                saveComPort_String = mindUI_GameObject[0].GetComponent<MindLink>().comPort_InputField.text;
                mindGear.OnApplicationQuit();
                mindUI_GameObject[1].SetActive(true);
                mindUI_GameObject[0].SetActive(false);
                mindUI_GameObject[1].GetComponent<MindLink>().comPort_InputField.text = saveComPort_String;
                mindUI_GameObject[1].GetComponent<MindLink>().Autolinkmind();
            }
        }
        else {
            firstCenterConnectStatus_Text.text = "腦波儀未連線";
        }
    }
    public void FirstTest() {
        if (mindUI_GameObject[1].GetComponent<MindTest>().mindUINext_Bool == true) {
            mindGear.OnApplicationQuit();
            mindUI_GameObject[2].SetActive(true);
            mindUI_GameObject[1].SetActive(false);
            mindUI_GameObject[2].GetComponent<MindLink>().comPort_InputField.text = saveComPort_String;
            mindUI_GameObject[2].GetComponent<MindLink>().Autolinkmind();
        }
    }
    public void SecondTest() {
        if (mindUI_GameObject[2].GetComponent<MindTest>().mindUINext_Bool == true) {
            StartCoroutine(LevelSave());
            mindGear.OnApplicationQuit();
            mindUI_GameObject[2].GetComponent<MindTest>().startTest_Button.enabled = false;
            loadingUI_GameObject.SetActive(true);
            StartCoroutine(LoadLevelWithBar());
        }
    }
    public IEnumerator LevelSave() {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("getuser", PlayerPrefs.GetString("UserAccount"));
        data.Add("savelevel", "" + mindUI_GameObject[1].GetComponent<MindTest>().userLevel_i);
        data.Add("savelevel2", "" + mindUI_GameObject[2].GetComponent<MindTest>().userLevel_i);
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(saveLevel_Web, form);
        yield return www;
    }
    IEnumerator LoadLevelWithBar() {
        _async = Application.LoadLevelAsync("Game");
        while (!_async.isDone) {
            loadingBar_Slider.value = _async.progress;
            yield return null;
        }
    }
    public void ExitTest() {
        mindGear.OnApplicationQuit();
        Application.LoadLevel("BeginUI");
    }
}
