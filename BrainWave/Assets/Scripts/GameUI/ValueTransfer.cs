using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValueTransfer : MonoBehaviour {
    [SerializeField] MindSave_Game mindSave_Game;
    [HideInInspector] public bool controllUpdate1_Bool = false;
    [HideInInspector] public bool controllUpdate2_Bool = false;
    [HideInInspector] public bool controllHpCost_Bool = false;
    [HideInInspector] public bool controllThingDrop_Bool = false;
    [HideInInspector] public bool controllAttentionDrop_Bool = false;
    [HideInInspector] public bool controllMindTakeThing_Bool = false;
    [HideInInspector] public bool controllMindPushThing_Bool = false;
    string attentionTimeUpdate_Web = "http://192.192.246.184/~u1025104/Unity/gpsmindsave.php";
    public IEnumerator MindUpdate(string web) {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user", PlayerPrefs.GetString("UserAccount"));
        data.Add("att", "" + mindSave_Game.saveMindValue[0]);
        data.Add("med", "" + mindSave_Game.saveMindValue[1]);
        data.Add("del", "" + mindSave_Game.saveMindValue[2]);
        data.Add("the", "" + mindSave_Game.saveMindValue[3]);
        data.Add("la", "" + mindSave_Game.saveMindValue[4]);
        data.Add("ha", "" + mindSave_Game.saveMindValue[5]);
        data.Add("lb", "" + mindSave_Game.saveMindValue[6]);
        data.Add("hb", "" + mindSave_Game.saveMindValue[7]);
        data.Add("lg", "" + mindSave_Game.saveMindValue[8]);
        data.Add("hg", "" + mindSave_Game.saveMindValue[9]);
        foreach(KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(web, form);
        yield return www;
    }
    public IEnumerator AttentionTimeUpdate(int time) {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user", PlayerPrefs.GetString("UserAccount"));
        data.Add("ta", "" + time);

        foreach(KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(attentionTimeUpdate_Web, form);
        yield return www;
    }
    public IEnumerator GetLevel(string web, string setName) {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user", PlayerPrefs.GetString("UserAccount"));
        foreach(KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(web, form);
        yield return www;
        PlayerPrefs.SetInt(setName, int.Parse(www.text));
    }
}
