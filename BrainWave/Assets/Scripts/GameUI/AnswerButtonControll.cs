using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButtonControll : MonoBehaviour {
    [SerializeField] Text userAnswer_Text;
    [SerializeField] Text questionChinese_Text;

    [SerializeField] Button answerQuestion_Button;
    [SerializeField] Button passQuestion_Button;

    void Update() {
        if(userAnswer_Text.text == "" || questionChinese_Text.text == "") {
            answerQuestion_Button.GetComponent<Button>().enabled = false;
            answerQuestion_Button.GetComponent<Image>().enabled = false;
        }
        else {
            answerQuestion_Button.GetComponent<Button>().enabled = true;
            answerQuestion_Button.GetComponent<Image>().enabled = true;
        }

        if(questionChinese_Text.text == "") {
            passQuestion_Button.GetComponent<Button>().enabled = false;
            passQuestion_Button.GetComponent<Image>().enabled = false;
        }
        else {
            passQuestion_Button.GetComponent<Button>().enabled = true;
            passQuestion_Button.GetComponent<Image>().enabled = true;
        }
    }
}

