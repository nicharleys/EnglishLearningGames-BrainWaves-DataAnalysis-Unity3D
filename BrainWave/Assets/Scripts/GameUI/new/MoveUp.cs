using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveUp : MonoBehaviour {
    [SerializeField] ShowMessage showMessage;
    [SerializeField] MindControllValue mindControllValue;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject[] movePoint;
    [SerializeField] Text cantMoveMessage_Text;
    [SerializeField] Text cantConveyFarMessage_Text;
    [HideInInspector] public bool controllMoveUp_Bool = false;
    float[] moveLength;
    void Awake() {
        cantMoveMessage_Text.GetComponent<Text>().enabled = false;
        cantConveyFarMessage_Text.GetComponent<Text>().enabled = false;
    }
    private void CountMoveLength(GameObject movePoint, float moveLength) {
        Vector3 movePosition = new Vector3(movePoint.transform.position.x, movePoint.transform.position.y, movePoint.transform.position.z);
        Vector3 distance = Player.transform.position - movePosition;
        moveLength = distance.magnitude;
    }
    private void SetMoveLength() {
        for(int i = 0; i < 9; i++) {
            CountMoveLength(movePoint[i], moveLength[i]);
            Debug.Log(i);
        }
    }
    public void ControllMoveUp() {
        SetMoveLength();
        if(mindControllValue.meditationSum_Int >= 375 && controllMoveUp_Bool == false) {
            if(moveLength[0] <= 1)
                Player.transform.position = movePoint[1].transform.position;
            if(moveLength[1] <= 1)
                Player.transform.position = movePoint[0].transform.position;
            if(moveLength[2] <= 1)
                Player.transform.position = movePoint[3].transform.position;
            if(moveLength[3] <= 1)
                Player.transform.position = movePoint[2].transform.position;
            if(moveLength[4] <= 1)
                Player.transform.position = movePoint[5].transform.position;
            if(moveLength[5] <= 1)
                Player.transform.position = movePoint[4].transform.position;
            if(moveLength[6] <= 1)
                Player.transform.position = movePoint[7].transform.position;
            if(moveLength[7] <= 1)
                Player.transform.position = movePoint[6].transform.position;
            if(moveLength[8] <= 1)
                Player.transform.position = movePoint[9].transform.position;
            if(moveLength[9] <= 1)
                Player.transform.position = movePoint[8].transform.position;
        }
        else if(mindControllValue.meditationSum_Int < 375 && controllMoveUp_Bool == false) {
            if(moveLength[0] <= 1 || moveLength[1] <= 1 || moveLength[2] <= 1 || moveLength[3] <= 1 || moveLength[4] <= 1 ||
               moveLength[5] <= 1 || moveLength[6] <= 1 || moveLength[7] <= 1 || moveLength[8] <= 1 || moveLength[9] <= 1)
                StartCoroutine(showMessage.ShowMessages(cantMoveMessage_Text));
        }
        if(moveLength[0] > 1 && moveLength[1] > 1 && moveLength[2] > 1 && moveLength[3] > 1 && moveLength[4] > 1 &&
           moveLength[5] > 1 && moveLength[6] > 1 && moveLength[7] > 1 && moveLength[8] > 1 && moveLength[9] > 1)
            StartCoroutine(showMessage.ShowMessages(cantConveyFarMessage_Text));
    }
}
