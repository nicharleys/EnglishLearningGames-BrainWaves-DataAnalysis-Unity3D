using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllStop : MonoBehaviour {
    [SerializeField] ThinkGearController thinkGearController;
    [SerializeField] GameObject stopUI_GameObject;
    [SerializeField] GameObject descriptionUI_GameObject;
    [SerializeField] GameObject endUI_GameObject;
    [SerializeField] GameObject publishAnswerUI_GameObject;
    [SerializeField] Button answerPass_Button;
    [SerializeField] Button answerQuestion_Button;
    [SerializeField] Button nextQuestion_Button;
    bool controllstop_Bool = false;
    void Update() {
        StopPause();
    }
    private void StopPause() {
        if(Input.GetKeyDown("escape")) {
            if(controllstop_Bool == true) {
                controllstop_Bool = false;
                Time.timeScale = 1;
                stopUI_GameObject.SetActive(false);
                descriptionUI_GameObject.SetActive(false);
                answerPass_Button.GetComponent<Button>().enabled = true;
                answerQuestion_Button.GetComponent<Button>().enabled = true;
                nextQuestion_Button.GetComponent<Button>().enabled = true;
            }
            else if(controllstop_Bool == false) {
                controllstop_Bool = true;
                Time.timeScale = 0;
                stopUI_GameObject.SetActive(true);
                answerPass_Button.GetComponent<Button>().enabled = false;
                answerQuestion_Button.GetComponent<Button>().enabled = false;
                nextQuestion_Button.GetComponent<Button>().enabled = false;
            }
        }
    }
    public void BackToMenu() {
        Time.timeScale = 1;
        thinkGearController.OnHeadsetDisconnectionRequest();
        StartCoroutine(DelayEnter());
        Application.LoadLevel("BeginUI");
    }
    public void Replay() {
        Time.timeScale = 1;
        thinkGearController.OnHeadsetDisconnectionRequest();
        StartCoroutine(DelayEnter());
        Application.LoadLevel("Game");
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void GameDescription() {
        descriptionUI_GameObject.SetActive(true);
        stopUI_GameObject.SetActive(false);
    }
    public void GameDescriptionBack() {
        stopUI_GameObject.SetActive(true);
        descriptionUI_GameObject.SetActive(false);
    }
    IEnumerator DelayEnter() {
        yield return new WaitForSeconds(1);
    }
    public void PublishAnswer() {
        LookAnswer(publishAnswerUI_GameObject, endUI_GameObject);
    }
    public void PublishAnswerBack() {
        LookAnswer(endUI_GameObject, publishAnswerUI_GameObject);
    }
    private void LookAnswer(GameObject ui1, GameObject ui2) {
        ui1.SetActive(true);
        ui2.SetActive(false);
    }
}
