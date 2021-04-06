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
    float[] moveLength_Float;

    void Awake() {
        cantMoveMessage_Text.GetComponent<Text>().enabled = false;
        cantConveyFarMessage_Text.GetComponent<Text>().enabled = false;
        moveLength_Float = new float[10];
    }
    private float CountMoveLength(GameObject movePoint) {
        Vector3 movePosition = new Vector3(movePoint.transform.position.x, movePoint.transform.position.y, movePoint.transform.position.z);
        Vector3 distance = Player.transform.position - movePosition;
        return distance.magnitude;
    }
    private void SetMoveLength() {
        for(int i = 0; i < 10; i++) {
            moveLength_Float[i] = CountMoveLength(movePoint[i]);
        }
    }
    public void ControllMoveUp() {
        SetMoveLength();
        if(mindControllValue.meditationSum_Int >= 375 && controllMoveUp_Bool == false) {
            if(moveLength_Float[0] <= 1)
                Player.transform.position = movePoint[1].transform.position;
            if(moveLength_Float[1] <= 1)
                Player.transform.position = movePoint[0].transform.position;
            if(moveLength_Float[2] <= 1)
                Player.transform.position = movePoint[3].transform.position;
            if(moveLength_Float[3] <= 1)
                Player.transform.position = movePoint[2].transform.position;
            if(moveLength_Float[4] <= 1)
                Player.transform.position = movePoint[5].transform.position;
            if(moveLength_Float[5] <= 1)
                Player.transform.position = movePoint[4].transform.position;
            if(moveLength_Float[6] <= 1)
                Player.transform.position = movePoint[7].transform.position;
            if(moveLength_Float[7] <= 1)
                Player.transform.position = movePoint[6].transform.position;
            if(moveLength_Float[8] <= 1)
                Player.transform.position = movePoint[9].transform.position;
            if(moveLength_Float[9] <= 1)
                Player.transform.position = movePoint[8].transform.position;
        }
        else if(mindControllValue.meditationSum_Int < 375 && controllMoveUp_Bool == false) {
            if(moveLength_Float[0] <= 1 || moveLength_Float[1] <= 1 || moveLength_Float[2] <= 1 || moveLength_Float[3] <= 1 || moveLength_Float[4] <= 1 ||
               moveLength_Float[5] <= 1 || moveLength_Float[6] <= 1 || moveLength_Float[7] <= 1 || moveLength_Float[8] <= 1 || moveLength_Float[9] <= 1)
                StartCoroutine(showMessage.ShowMessages(cantMoveMessage_Text));
        }
        if(moveLength_Float[0] > 1 && moveLength_Float[1] > 1 && moveLength_Float[2] > 1 && moveLength_Float[3] > 1 && moveLength_Float[4] > 1 &&
           moveLength_Float[5] > 1 && moveLength_Float[6] > 1 && moveLength_Float[7] > 1 && moveLength_Float[8] > 1 && moveLength_Float[9] > 1)
            StartCoroutine(showMessage.ShowMessages(cantConveyFarMessage_Text));
    }
}
