using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AccountSetting : MonoBehaviour {
    public static string nowUser;
    string signAccount_Web = "http://192.192.246.184/~u1025104/Unity/OldPlayerEnter.php";
    string createAccount_Web = "http://192.192.246.184/~u1025104/Unity/NewPlayerEnter.php";
    string changeAccount_Web = "http://192.192.246.184/~u1025104/Unity/ChangePlayerEnter.php";
    [SerializeField] UIControll StartUI_UIControll;
    [SerializeField] UIControll SignAccountUI_UIControll;
    [SerializeField] UIControll CreateAccountUI_UIControll;
    [SerializeField] UIControll CheckAccountUI_UIControll;
    [SerializeField] UIControll ChangeAccountUI_UIControll;
    [SerializeField] UIControll MenuUI_UIControll;
    [Space(10)]
    [SerializeField] InputField SignUIInputAccount_InputField;
    [SerializeField] InputField SignUIInputPassword_InputField;
    [SerializeField] Text SignUIShowID_Text;
    [Space(10)]
    [SerializeField] InputField CreateUIInputAccount_InputField;
    [SerializeField] InputField CreateUIInputPassword_InputField;
    [SerializeField] Text CreateUIShowID_Text;
    [SerializeField] Text CreateUIShowPW_Text;
    [SerializeField] Text CreateUIShowFailID_Text;
    [Space(10)]
    [SerializeField] InputField CheckUIInputAccount_InputField;
    [SerializeField] InputField CheckUIInputPassword_InputField;
    [SerializeField] Text CheckUIShowID_Text;
    [SerializeField] Text CheckUIShowPW_Text;
    [Space(10)]
    [SerializeField] InputField ChangeUIInputAccount_InputField;
    [SerializeField] InputField ChangeUIInputPassword_InputField;
    [SerializeField] Text ChangeUIShowID_Text;
    [SerializeField] Text ChangeUIShowPW_Text;
    [SerializeField] Text ChangeUIShowFailID_Text;
    string CheckUIInputAccount_String;
    public void SignAccount() {
        StartCoroutine("PostSignAccount");
    }
    public void CreateAccount() {
        StartCoroutine("PostCreateAccount");
    }
    public void CheckSignAccount() {
        StartCoroutine("PostCheckAccount");
    }
    public void ChangeSignAccount() {
        StartCoroutine("PostChangeAccount");
    }
    private IEnumerator PostSignAccount() { 
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("download", "1");
        data.Add("oldac", SignUIInputAccount_InputField.text);
        data.Add("oldpw", SignUIInputPassword_InputField.text);
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(signAccount_Web, form);
        yield return www;
        //Success
        if (www.text == "1") {
            SignUIShowID_Text.text = "ID：" + SignUIInputAccount_InputField.text;
            nowUser = SignUIInputAccount_InputField.text;
            SignAccountUI_UIControll.ui[1].SetActive(true);
            SignAccountUI_UIControll.ui[0].SetActive(false);
            SignAccountUI_UIControll.nowUINumber = 1;
        }
        //Fail
        else if (www.text == "2") {
            SignAccountUI_UIControll.ui[2].SetActive(true);
            SignAccountUI_UIControll.ui[0].SetActive(false);
            SignAccountUI_UIControll.nowUINumber = 2;
        }
        //Null
        else if (www.text == "0") {
            SignAccountUI_UIControll.ui[3].SetActive(true);
            SignAccountUI_UIControll.ui[0].SetActive(false);
            SignAccountUI_UIControll.nowUINumber = 3;
        }
    }
    private IEnumerator PostCreateAccount() {

        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("download", "1");
        data.Add("newac", CreateUIInputAccount_InputField.text);
        data.Add("newpw", CreateUIInputPassword_InputField.text);
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(createAccount_Web, form);
        yield return www;
        //Success
        if (www.text == "0") {
            CreateUIShowID_Text.text = "ID：" + CreateUIInputAccount_InputField.text;
            CreateUIShowPW_Text.text = "password：" + CreateUIInputPassword_InputField.text;
            CreateAccountUI_UIControll.ui[1].SetActive(true);
            CreateAccountUI_UIControll.ui[0].SetActive(false);
            CreateAccountUI_UIControll.nowUINumber = 1;
        }
        //Fail
        else if (www.text == "1") {
            CreateUIShowFailID_Text.text = "ID：" + CreateUIInputAccount_InputField.text;
            CreateAccountUI_UIControll.ui[2].SetActive(true);
            CreateAccountUI_UIControll.ui[0].SetActive(false);
            CreateAccountUI_UIControll.nowUINumber = 2;
        }
    }
    private IEnumerator PostCheckAccount() {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("download", "1");
        data.Add("oldac", CheckUIInputAccount_InputField.text);
        data.Add("oldpw", CheckUIInputPassword_InputField.text);
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(signAccount_Web, form);
        yield return www;
        //Success
        if (www.text == "1") {
            CheckUIInputAccount_String = CheckUIInputAccount_InputField.text;
            CheckUIShowID_Text.text = "ID：" + CheckUIInputAccount_InputField.text;
            CheckUIShowPW_Text.text = "password：" + CheckUIInputPassword_InputField.text;
            CheckAccountUI_UIControll.ui[1].SetActive(true);
            CheckAccountUI_UIControll.ui[0].SetActive(false);
            CheckAccountUI_UIControll.nowUINumber = 1;
        }
        //Fail
        else if (www.text == "2") {
            CheckAccountUI_UIControll.ui[2].SetActive(true);
            CheckAccountUI_UIControll.ui[0].SetActive(false);
            CheckAccountUI_UIControll.nowUINumber = 2;
        }
        //Null
        else if (www.text == "0") {
            CheckAccountUI_UIControll.ui[3].SetActive(true);
            CheckAccountUI_UIControll.ui[0].SetActive(false);
            CheckAccountUI_UIControll.nowUINumber = 3;
        }
    }
    private IEnumerator PostChangeAccount() {

        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("download", "1");
        data.Add("changeac", ChangeUIInputAccount_InputField.text);
        data.Add("changepw", ChangeUIInputPassword_InputField.text);
        data.Add("csuserac", CheckUIInputAccount_String);
        foreach (KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(changeAccount_Web, form);
        yield return www;
        //Success
        if (www.text == "0") {
            ChangeUIShowID_Text.text = "ID：" + ChangeUIInputAccount_InputField.text;
            ChangeUIShowPW_Text.text = "password：" + ChangeUIInputPassword_InputField.text;
            ChangeAccountUI_UIControll.ui[1].SetActive(true);
            ChangeAccountUI_UIControll.ui[0].SetActive(false);
            ChangeAccountUI_UIControll.nowUINumber = 1;
        }
        //Fail
        else if (www.text == "1") {
            ChangeUIShowFailID_Text.text = "ID：" + ChangeUIInputAccount_InputField.text;
            ChangeAccountUI_UIControll.ui[2].SetActive(true);
            ChangeAccountUI_UIControll.ui[0].SetActive(false);
            ChangeAccountUI_UIControll.nowUINumber = 2;
        }
    }
}
