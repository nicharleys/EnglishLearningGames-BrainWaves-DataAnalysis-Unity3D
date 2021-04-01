using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClassSetting : MonoBehaviour {
    [SerializeField] Image[] classButton_Image;
    bool[] classClick_bool = new bool[15];
    string[] saveClickData_String = new string[15];
    string chooseDataValue_String = " Union Select q_num From word";
    string searchDataValue_String = " Union Select * From word";
    string allDataSearchValue = "";
    string classData_Web = "http://192.192.246.184/~u1025104/Unity/shownum.php";
    public void ClassChoose(int num) {
        ClickClass(num, chooseDataValue_String);
    }
    public void ClassSearch(int num) {
        ClickClass(num, searchDataValue_String);
    }
    private void ClickClass(int num, string dataValue) {
        if (classClick_bool[num - 1] == false) {
            classClick_bool[num - 1] = true;
            saveClickData_String[num - 1] = "" + dataValue + num;
            classButton_Image[num - 1].color = Color.gray;
        }
        else if (classClick_bool[num - 1] == true) {
            classClick_bool[num - 1] = false;
            saveClickData_String[num - 1] = "";
            classButton_Image[num - 1].color = Color.white;
        }
    }
    public void ClassChoseEnter() {
        allDataSearchValue = saveClickData_String[0] + saveClickData_String[1] + saveClickData_String[2] + saveClickData_String[3] + saveClickData_String[4] + saveClickData_String[5] + saveClickData_String[6] + saveClickData_String[7]
                           + saveClickData_String[8] + saveClickData_String[9] + saveClickData_String[10] + saveClickData_String[11] + saveClickData_String[12] + saveClickData_String[13] + saveClickData_String[14];
        PlayerPrefs.SetString("ClassChose", allDataSearchValue);
        Application.LoadLevel("TestUI");
    }
    public void ClassSearchEnter(Text ShowData) {
        allDataSearchValue = saveClickData_String[0] + saveClickData_String[1] + saveClickData_String[2] + saveClickData_String[3] + saveClickData_String[4] + saveClickData_String[5] + saveClickData_String[6] + saveClickData_String[7]
                           + saveClickData_String[8] + saveClickData_String[9] + saveClickData_String[10] + saveClickData_String[11] + saveClickData_String[12] + saveClickData_String[13] + saveClickData_String[14];
        StartCoroutine(PostClassData(ShowData));
    }
    private IEnumerator PostClassData(Text ShowData) {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("SgiveLS", allDataSearchValue);
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(classData_Web, form);
        yield return www;
        ShowData.text = www.text;
    }
    public void UIBack() {
        for (int i = 0; i <= 14; i++) {
            saveClickData_String[i] = "";
            classClick_bool[i] = false;
            classButton_Image[i].color = Color.white;
        }
    }
    public void UIBackOther(Text ShowData) {
        UIBack();
        ShowData.text = "";
    }

}
