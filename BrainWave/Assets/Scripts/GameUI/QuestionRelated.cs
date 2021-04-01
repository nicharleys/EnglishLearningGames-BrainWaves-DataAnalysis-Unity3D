using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionRelated : MonoBehaviour {

    [SerializeField] Text userAnswer_Text;
    [SerializeField] Text question_Text;
    [SerializeField] Text showScore_Text;
    [SerializeField] Text showAnswerCount_Text;
    [SerializeField] Text showAnswerTrue_Text;
    [SerializeField] Text showAnswerFalse_Text;
    [SerializeField] Text showAnswerPass_Text;
    [SerializeField] Text showPlayTime_Text;
    [SerializeField] Text showStopScore_Text;
    [SerializeField] Text showEndscore_Text;
    [SerializeField] Text showAllAnswer_Text;
    [SerializeField] Text showAllQuestion_Text;
    [SerializeField] Button NextQuestion_Button;
    [SerializeField] GameObject gamePromptPanel_GameObject;
    [SerializeField] GameObject endUI_GameObject;
    [SerializeField] GameObject questionUI_GameObject;
    [SerializeField] ControllStop controllStop;

    string saveNowQuestion_String;
    string questionAnswer_String;

    bool controllButton_Bool = false;
    bool waitExecute = false;
    bool promptClick_Bool = false;
    bool controllTimeCount_Bool = false;

    int questionPoint_Int = 10;
    int allScore_Int = 0;
    int answerCount_Int = 0;
    int questionAmount_Int = 10;
    int allTimeCount_Int = 0;
    int secondTimeCount_Int = 0;
    int minuteTimeCount_Int = 0;
    int hourTimeCount_Int = 0;

    string getQuestionChinese_Web = "http://192.192.246.184/~u1025104/Unity/ChCatch.php";
    string getQuestionEnglish_Web = "http://192.192.246.184/~u1025104/Unity/EnCatch.php";
    string getRandomQuestion_Web = "http://192.192.246.184/~u1025104/Unity/Random.php";
    string saveRecord_Web = "http://192.192.246.184/~u1025104/Unity/saveEndRecord.php";

    void Awake() {
        showAnswerTrue_Text.GetComponent<Text>().enabled = false;
        showAnswerFalse_Text.GetComponent<Text>().enabled = false;
        showAnswerPass_Text.GetComponent<Text>().enabled = false;
        gamePromptPanel_GameObject.SetActive(false);
    }
    void Update() {
        StartTimercount();
    }
    public void AnswerQuestion() {
        if(controllButton_Bool == false && userAnswer_Text.text != "" && question_Text.text != "") {
            if(userAnswer_Text.text == questionAnswer_String) {
                NextQuestion_Button.GetComponent<Button>().enabled = true;
                NextQuestion_Button.GetComponent<Image>().enabled = true;
                StartCoroutine(ShowAnswerMessage(showAnswerTrue_Text));
                question_Text.text = "";
                userAnswer_Text.text = "";
                questionAnswer_String = "*";
                allScore_Int += questionPoint_Int;
                showScore_Text.text = "分數：" + allScore_Int;
                showStopScore_Text.text = "分數：" + allScore_Int;
                answerCount_Int += 1;
                showAnswerCount_Text.text = "答題數：" + answerCount_Int;
            }
            else if(userAnswer_Text.text != questionAnswer_String) {
                NextQuestion_Button.GetComponent<Button>().enabled = true;
                NextQuestion_Button.GetComponent<Image>().enabled = true;
                StartCoroutine(ShowAnswerMessage(showAnswerFalse_Text));
                question_Text.text = "";
                userAnswer_Text.text = "";
                questionAnswer_String = "*";
                answerCount_Int += 1;
                showAnswerCount_Text.text = "答題數：" + answerCount_Int;
            }
            if(answerCount_Int == questionAmount_Int) {
                controllButton_Bool = true;
                StartCoroutine(SaveEndRecord());
                StartCoroutine(SpawnWaves());
            }
        }
    }
    public void PassQuestion() {
        if(controllButton_Bool == false && waitExecute == false) {
            waitExecute = true;
            StartCoroutine(ShowAnswerMessage(showAnswerPass_Text));
            question_Text.text = "";
            userAnswer_Text.text = "";
            questionAnswer_String = "*";
            answerCount_Int += 1;
            showAnswerCount_Text.text = "答題數：" + answerCount_Int;
            if(answerCount_Int == questionAmount_Int) {
                controllButton_Bool = true;
                StartCoroutine(SaveEndRecord());
                StartCoroutine(SpawnWaves());
            }
            StartCoroutine(DelayEnter());
            waitExecute = false;
            NextQuestion_Button.GetComponent<Button>().enabled = true;
            NextQuestion_Button.GetComponent<Image>().enabled = true;
        }
    }
    public void NextQuestion() {
        if(controllButton_Bool == false && waitExecute == false) {
            waitExecute = true;
            NextQuestion_Button.GetComponent<Button>().enabled = false;
            NextQuestion_Button.GetComponent<Image>().enabled = false;
            StartCoroutine(WaitRandom());
            if(answerCount_Int == questionAmount_Int) {
                controllButton_Bool = true;
            }
            StartCoroutine(DelayEnter());
            waitExecute = false;
        }
    }
    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(1.5f);
        userAnswer_Text.text = "";
        Time.timeScale = 0;
        endUI_GameObject.SetActive(true);
        questionUI_GameObject.SetActive(false);
        controllStop.GetComponent<ControllStop>().enabled = false;
        showEndscore_Text.text = showScore_Text.text;
    }
    public IEnumerator GetWordChinese() {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("NowQ", saveNowQuestion_String);
        foreach(KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(getQuestionChinese_Web, form);
        yield return www;
        question_Text.text = www.text;
        showAllQuestion_Text.text += www.text + "\n";
    }
    public IEnumerator GetWordEnglish() {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("NowQ", saveNowQuestion_String);
        foreach(KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(getQuestionEnglish_Web, form);
        yield return www;
        questionAnswer_String = www.text;
        showAllAnswer_Text.text += www.text + "\n";
    }
    public IEnumerator RandomQuestion() {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("download", "1");
        data.Add("getLS", PlayerPrefs.GetString("ClassChose"));
        foreach(KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(getRandomQuestion_Web, form);
        yield return www;
        saveNowQuestion_String = www.text;
    }
    IEnumerator WaitRandom() {
        StartCoroutine(RandomQuestion());
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(GetWordEnglish());
        StartCoroutine(GetWordChinese());
    }
    IEnumerator DelayEnter() {
        yield return new WaitForSeconds(2.5f);
    }
    public IEnumerator SaveEndRecord() {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user", PlayerPrefs.GetString("UserAccount"));
        data.Add("score", "" + allScore_Int);
        data.Add("gametime", "" + allTimeCount_Int);
        //data.Add("thingd", savethingd.text);
        //data.Add("die", savedie.text);
        foreach(KeyValuePair<string, string> post in data) {
            form.AddField(post.Key, post.Value);
        }
        WWW www = new WWW(saveRecord_Web, form);
        yield return www;
    }
    private void StartTimercount() {
        if(controllTimeCount_Bool == false) {
            StartCoroutine(Timercount());
            controllTimeCount_Bool = true;
        }
    }
    IEnumerator Timercount() {
        yield return new WaitForSeconds(1);
        allTimeCount_Int++;
        secondTimeCount_Int += 1;
        controllTimeCount_Bool = false;
        if(secondTimeCount_Int == 60) {
            minuteTimeCount_Int += 1;
            secondTimeCount_Int = 0;
        }
        if(minuteTimeCount_Int == 60) {
            minuteTimeCount_Int = 0;
            hourTimeCount_Int += 1;
        }
        showPlayTime_Text.text = hourTimeCount_Int + ":" + minuteTimeCount_Int + ":" + secondTimeCount_Int;
    }
    IEnumerator ShowAnswerMessage(Text message) {
        message.enabled = true;
        yield return new WaitForSeconds(2);
        message.enabled = false;
    }
    public void GamePrompt() {
        if(promptClick_Bool == false) {
            gamePromptPanel_GameObject.SetActive(true);
            promptClick_Bool = true;
        }
        else {
            gamePromptPanel_GameObject.SetActive(false);
            promptClick_Bool = false;
        }
    }
}
