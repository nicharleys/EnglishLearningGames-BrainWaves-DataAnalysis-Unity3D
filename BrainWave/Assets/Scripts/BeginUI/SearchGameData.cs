using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SearchGameData : MonoBehaviour {
    public InputField startTime_InputField;
    public InputField endTime_InputField;
    public Text showGameData_Text;
    public Text showTime_Text;
    string gameData_Web = "http://192.192.246.184/~u1025104/Unity/gamerecord.php";
    string mindData_Web = "http://192.192.246.184/~u1025104/Unity/EnterMind.php";
    public void SearchGameEnter() {
        showTime_Text.text = startTime_InputField.text + "~" + endTime_InputField.text;
        StartCoroutine(SearchGameRecord());
    }
    public void SearchGameShowBack() {
        startTime_InputField.text = "";
        endTime_InputField.text = "";
        showTime_Text.text = "";
        showGameData_Text.text = "";
    }
    public void SearchMind() {
        Application.OpenURL(mindData_Web);
    }
    private IEnumerator SearchGameRecord() {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("start", startTime_InputField.text);
        data.Add("end", endTime_InputField.text);
        data.Add("acnum", PlayerPrefs.GetString("UserAccount"));
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(gameData_Web, form);
        yield return www;
        showGameData_Text.text = www.text;
    }
}
