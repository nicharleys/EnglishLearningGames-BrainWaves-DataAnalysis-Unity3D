using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MindControllValue : MonoBehaviour {
    [SerializeField] MindSave_Game mindSave_Game;
    [SerializeField] ValueTransfer valueTransfer;
    [SerializeField] MindControllAction mindControllAction;
    [SerializeField] public GameObject player;
    [SerializeField] public Slider attention_Slider;
    [SerializeField] public Slider meditation_Slider;
    [SerializeField] public Slider playerHp_Slider;
    [HideInInspector] public int playerHp_Int = 10;
    [HideInInspector] public int playerDead_Int = 0;
    [HideInInspector] public int attentionSum_Int = 0;
    [HideInInspector] public int meditationSum_Int = 0;
    [HideInInspector] public int timer1_Int = 0;
    int timer2_Int = 0;
    bool controllTimeCount1_Bool = false;
    bool controllTimeCount2_Bool = false;
    string saveMind1_Web = "http://192.192.246.184/~u1025104/Unity/controllmindsave.php";
    string saveMind2_Web = "http://192.192.246.184/~u1025104/Unity/controllmindsave2.php";
    string getAttentionLevel_Web = "http://192.192.246.184/~u1025104/Unity/attlevel.php";
    string getMeditationLevel_Web = "http://192.192.246.184/~u1025104/Unity/medlevel.php";
    void Awake() {
        StartCoroutine(valueTransfer.GetLevel(getAttentionLevel_Web, "AttentionLevel"));
        StartCoroutine(valueTransfer.GetLevel(getMeditationLevel_Web, "MeditationLevel"));
    }
    void Update() {
        CountAttention();
        CountMeditation();
    }
    private void TransferValue(bool controllUpdate, bool controllTimeCount, int timer, string web) {
        if(mindSave_Game.saveMindValue[0] > 0 && mindSave_Game.saveMindValue[3] < 200) {
            if(controllUpdate == false) {
                controllUpdate = true;
                StartCoroutine(valueTransfer.MindUpdate(web));
                StartCoroutine(valueTransfer.DelayPost(controllUpdate, 1));
            }
            if(controllTimeCount == false) {
                TimeCount(timer, controllTimeCount);
                controllTimeCount = true;
            }
        }
    }
    private void Mindcount_MoreThan(int sum, Slider slider, int value) {
        if(sum <= 510) {
            sum += value;
            slider.value += value;
        }
    }
    private void Mindcount_LessThan(int sum, Slider slider, int value) {
        if(sum >= 0) {
            sum -= value;
            slider.value -= value;
        }
    }
    private void CountMeditation() {
        TransferValue(valueTransfer.controllUpdate2_Bool, controllTimeCount2_Bool, timer2_Int, saveMind2_Web);
        if(mindSave_Game.saveMindValue[1] / 1000 >= ( 55 - PlayerPrefs.GetInt("MeditationLevel") ))
            Mindcount_MoreThan(meditationSum_Int, meditation_Slider, 4);
        else if(mindSave_Game.saveMindValue[1] / 1000 > ( 35 - PlayerPrefs.GetInt("MeditationLevel") ) && mindSave_Game.saveMindValue[1] / 1000 <= ( 55 - PlayerPrefs.GetInt("MeditationLevel") ))
            Mindcount_MoreThan(meditationSum_Int, meditation_Slider, 3);
        else if(mindSave_Game.saveMindValue[1] / 1000 > ( 25 - PlayerPrefs.GetInt("MeditationLevel") ) && mindSave_Game.saveMindValue[1] / 1000 <= ( 35 - PlayerPrefs.GetInt("MeditationLevel") ))
            Mindcount_MoreThan(meditationSum_Int, meditation_Slider, 2);
        else if(mindSave_Game.saveMindValue[1] / 1000 >= 0 && mindSave_Game.saveMindValue[1] / 1000 <= ( 15 - PlayerPrefs.GetInt("MeditationLevel") ))
            Mindcount_LessThan(meditationSum_Int, meditation_Slider, 8);
    }
    private void CountAttention() {
        if(( valueTransfer.controllMindPushThing_Bool == true || valueTransfer.controllMindTakeThing_Bool == true ) && mindControllAction.nowClickThing_GameObject.transform.parent != player.transform) {
            TransferValue(valueTransfer.controllUpdate1_Bool, controllTimeCount1_Bool, timer1_Int, saveMind1_Web);

            if(mindSave_Game.saveMindValue[0] >= ( 50 - PlayerPrefs.GetInt("AttentionLevel") ) && mindSave_Game.saveMindValue[0] < ( 75 - PlayerPrefs.GetInt("AttentionLevel") ))
                Mindcount_MoreThan(attentionSum_Int, attention_Slider, 2);
            else if(mindSave_Game.saveMindValue[0] >= ( 75 - PlayerPrefs.GetInt("AttentionLevel") ) && mindSave_Game.saveMindValue[0] < 100)
                Mindcount_MoreThan(attentionSum_Int, attention_Slider, 4);
            else if(mindSave_Game.saveMindValue[0] == 100)
                Mindcount_MoreThan(attentionSum_Int, attention_Slider, 8);
            else if(mindSave_Game.saveMindValue[0] <= ( 25 - PlayerPrefs.GetInt("AttentionLevel") ))
                Mindcount_LessThan(attentionSum_Int, attention_Slider, 2);
            else if(mindSave_Game.saveMindValue[0] <= ( 15 - PlayerPrefs.GetInt("AttentionLevel") ))
                Mindcount_LessThan(attentionSum_Int, attention_Slider, 4);
            else if(mindSave_Game.saveMindValue[0] <= ( 10 - PlayerPrefs.GetInt("AttentionLevel") ))
                Mindcount_LessThan(attentionSum_Int, attention_Slider, 8);
        }
    }
    IEnumerator TimeCount(int timer, bool controllTimeCount) {
        yield return new WaitForSeconds(1);
        timer++;
        controllTimeCount = false;
    }
}
