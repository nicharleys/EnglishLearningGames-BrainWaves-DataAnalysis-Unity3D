using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MindControllAction : MonoBehaviour {
    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] MindControllValue mindControllValue;
    [SerializeField] ValueTransfer valueTransfer;
    [SerializeField] MindSave_Game mindSave_Game;
    [SerializeField] MoveUp moveUp;
    [SerializeField] ShowMessage showMessage;
    [SerializeField] GameObject playerPutPosition_GameObject;
    [SerializeField] GameObject answerPutPosition_GameObject;
    [SerializeField] GameObject Player_GameObject;
    [SerializeField] GameObject answerPlane_GameObject;
    [SerializeField] Camera player_Camera;
    [SerializeField] Text connectStatus_Text;
    [SerializeField] Text cantCatchFarMessage_Text;
    [SerializeField] Text cantCatchNearMessage_Text;
    [SerializeField] Text cantCatchFailMessage_Text;
    [SerializeField] Text cantCatchAllFailMessage_Text;
    [SerializeField] Vector3 roomSize_Vector3;
    [HideInInspector] public GameObject nowClickThing_GameObject;
    [HideInInspector] public GameObject lastClickThing_GameObject = null;
    bool controlLookThingPosition_Bool = false;
    bool controlLookAnswerPutPosition_Bool = false;
    bool controllThingMove_Bool = false;
    bool lowAttentionStatus_Bool = false;
    bool controllMouseClick_Bool = false;
    bool controllThingPush_Bool = false;
    bool closeThingMove_Bool = false;
    bool controllAttentionTimeUpdate_Bool = false;
    bool controllRandomThingPush_Bool = false;
    int saveTime_Int = 0;
    int saveAttentionTime_Int = 0;
    int allthingDropCount = 0;
    int thingDropCount = 0;
    float thingspeed = 1f;
    void Awake() {
        answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = false;
        cantCatchFarMessage_Text.GetComponent<Text>().enabled = false;
        cantCatchNearMessage_Text.GetComponent<Text>().enabled = false;
        cantCatchFailMessage_Text.GetComponent<Text>().enabled = false;
        cantCatchAllFailMessage_Text.GetComponent<Text>().enabled = false;
    }
    void Update() {
        LookThing();
        LookAnswerPlane();
        SpeedControll();
        MindTakeThing();
        MindPushThing();
        PlayerControll();
        MindMoveThing();
        MindAnswerMove();
        RandomMoveThing();
    }
    private void SpeedControll() {
        if(mindControllValue.meditationSum_Int < 125 && mindControllValue.meditationSum_Int >= 0) {
            thirdPersonController.walkSpeed = 0.1f;
            playerMove.controllPlayerRun_Bool = false;
        }
        else if(mindControllValue.meditationSum_Int < 250 && mindControllValue.meditationSum_Int >= 125) {
            thirdPersonController.walkSpeed = 0.5f;
            playerMove.controllPlayerRun_Bool = false;
        }
        else if(mindControllValue.meditationSum_Int < 375 && mindControllValue.meditationSum_Int >= 250) {
            thirdPersonController.walkSpeed = 1f;
            playerMove.controllPlayerRun_Bool = false;
        }
        else if(mindControllValue.meditationSum_Int < 500 && mindControllValue.meditationSum_Int >= 375) {
            thirdPersonController.walkSpeed = 1.5f;
            playerMove.controllPlayerRun_Bool = false;
        }
        else if(mindControllValue.meditationSum_Int >= 500) {
            thirdPersonController.walkSpeed = 5f;
            playerMove.controllPlayerRun_Bool = true;
        }
    }
    private void MindTakeThing() {
        if(valueTransfer.controllMindTakeThing_Bool == true && nowClickThing_GameObject.transform.parent != Player_GameObject.transform) {
            if(mindControllValue.attentionSum_Int < 250 && mindControllValue.attentionSum_Int >= 0) {
                controllThingMove_Bool = false;
                lowAttentionStatus_Bool = true;
            }
            else if(mindControllValue.attentionSum_Int <= 258 && mindControllValue.attentionSum_Int > 250 && mindSave_Game.saveMindValue[0] <= ( 25 - PlayerPrefs.GetInt("AttentionLevel") )) {
                MindDropThing();
            }
            else if(mindControllValue.attentionSum_Int < 375 && mindControllValue.attentionSum_Int >= 250) {
                controllThingMove_Bool = true;
                lowAttentionStatus_Bool = false;
                thingspeed = 0.01f;
            }
            else if(mindControllValue.attentionSum_Int < 500 && mindControllValue.attentionSum_Int >= 375) {
                controllThingMove_Bool = true;
                lowAttentionStatus_Bool = false;
                thingspeed = 0.25f;
            }
            else if(mindControllValue.attentionSum_Int >= 500) {
                controllThingMove_Bool = true;
                lowAttentionStatus_Bool = false;
                thingspeed = 0.5f;
            }
        }
    }
    private void MindPushThing() {
        if(valueTransfer.controllMindPushThing_Bool == true && nowClickThing_GameObject.transform.parent != Player_GameObject.transform) {
            if(mindControllValue.attentionSum_Int < 250 && mindControllValue.attentionSum_Int >= 0) {
                controllThingPush_Bool = false;
            }
            else if(mindControllValue.attentionSum_Int <= 258 && mindControllValue.attentionSum_Int > 250 && mindSave_Game.saveMindValue[0] <= ( 25 - PlayerPrefs.GetInt("AttentionLevel") )) {
                MindDropThing();
            }
            else if(mindControllValue.attentionSum_Int < 375 && mindControllValue.attentionSum_Int >= 250) {
                controllThingPush_Bool = true;
                thingspeed = 0.01f;
            }
            else if(mindControllValue.attentionSum_Int < 500 && mindControllValue.attentionSum_Int >= 375) {
                controllThingPush_Bool = true;
                thingspeed = 0.25f;
            }
            else if(mindControllValue.attentionSum_Int >= 500) {
                controllThingPush_Bool = true;
                thingspeed = 0.5f;
            }
        }
    }
    private void MouseClick() {
        if(Input.GetMouseButtonUp(0) && controllMouseClick_Bool == false) {
            Vector3 vec = Input.mousePosition;
            Ray ray = player_Camera.ScreenPointToRay(vec);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, 2000.0f) && hitInfo.collider.name == "english(Clone)") {
                nowClickThing_GameObject = hitInfo.collider.gameObject;
                if(lastClickThing_GameObject != null && nowClickThing_GameObject != lastClickThing_GameObject)
                    lastClickThing_GameObject.transform.FindChild("thisthing").gameObject.SetActive(false);
                nowClickThing_GameObject.transform.FindChild("thisthing").gameObject.SetActive(true);
                lastClickThing_GameObject = nowClickThing_GameObject;
            }
        }
    }
    private void MoveUp() {
        if(Input.GetButtonDown("upmove")) {
            moveUp.ControllMoveUp();
        }
    }
    private void StartMindControllThing() {
        if(Input.GetKeyDown(KeyCode.LeftControl) && connectStatus_Text.text == "Connected" && mindSave_Game.saveMindValue[0] > 0) {
            Vector3 playerPosition = new Vector3(playerPutPosition_GameObject.transform.position.x, playerPutPosition_GameObject.transform.position.y, playerPutPosition_GameObject.transform.position.z);
            Vector3 thingDistance = playerPosition - nowClickThing_GameObject.transform.position;
            float thingDistanceLength = thingDistance.magnitude;
            if(thingDistanceLength <= 10) {
                moveUp.controllMoveUp_Bool = true;
                valueTransfer.controllMindTakeThing_Bool = true;
                controllAttentionTimeUpdate_Bool = false;
                thirdPersonController.GetComponent<ThirdPersonController>().enabled = false;
                thirdPersonController.GetComponent<Animator>().enabled = false;
                playerMove.controllPlayerWalk_Bool = true;
                controlLookThingPosition_Bool = true;
            }
            else {
                showMessage.ShowCantCatch(cantCatchFarMessage_Text);
            }
        }
    }
    private void CancelMindControllThing() {
        if(Input.GetKeyDown(KeyCode.RightControl) && nowClickThing_GameObject != null) {
            answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = false;
            thirdPersonController.GetComponent<ThirdPersonController>().enabled = true;
            thirdPersonController.GetComponent<Animator>().enabled = true;
            playerMove.controllPlayerWalk_Bool = false;
            controlLookThingPosition_Bool = false;
            controlLookAnswerPutPosition_Bool = false;
            saveTime_Int = mindControllValue.timer1_Int;
            mindControllValue.timer1_Int = 0;
            valueTransfer.controllMindTakeThing_Bool = false;
            valueTransfer.controllMindPushThing_Bool = false;
            mindControllValue.attentionSum_Int = 0;
            lowAttentionStatus_Bool = false;
            controllMouseClick_Bool = false;
            controllThingPush_Bool = false;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            nowClickThing_GameObject.transform.parent = null;
            controllThingMove_Bool = false;
            closeThingMove_Bool = false;
            controllAttentionTimeUpdate_Bool = true;
            nowClickThing_GameObject.GetComponent<Enemy>().enabled = true;
            nowClickThing_GameObject.GetComponent<Rotator>().enabled = false;
            mindControllValue.attention_Slider.value = 0;
        }
    }
    private void StartMindControllAnswer() {
        if(Input.GetButtonDown("answer") && nowClickThing_GameObject != null && nowClickThing_GameObject.transform.parent == mindControllValue.player.transform) {
            Vector3 answerPosition = new Vector3(answerPutPosition_GameObject.transform.position.x, answerPutPosition_GameObject.transform.position.y, answerPutPosition_GameObject.transform.position.z);
            Vector3 answerDistance = answerPosition - nowClickThing_GameObject.transform.position;
            float answerDistanceLength = answerDistance.magnitude;
            if(answerDistanceLength >= 3 && answerDistanceLength <= 15) {
                valueTransfer.controllMindTakeThing_Bool = true;
                answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = true;
                controlLookAnswerPutPosition_Bool = true;
                thirdPersonController.GetComponent<ThirdPersonController>().enabled = false;
                thirdPersonController.GetComponent<Animator>().enabled = false;
                playerMove.controllPlayerWalk_Bool = true;
                controlLookThingPosition_Bool = true;
                valueTransfer.controllMindPushThing_Bool = true;
                controllAttentionTimeUpdate_Bool = false;
            }
            else if(answerDistanceLength < 3) {
                StartCoroutine(showMessage.ShowMessages(cantCatchNearMessage_Text));
            }
            else if(answerDistanceLength > 15) {
                StartCoroutine(showMessage.ShowMessages(cantCatchFarMessage_Text));
            }
        }
    }
    private void PlayerControll() {
        MouseClick();
        MoveUp();
        if(nowClickThing_GameObject != null) {
            StartMindControllThing();
            CancelMindControllThing();
            StartMindControllAnswer();
        }
    }
    private void LookThing() {
        if(controlLookThingPosition_Bool == true) {
            Vector3 playerPosition = new Vector3(playerPutPosition_GameObject.transform.position.x, playerPutPosition_GameObject.transform.position.y, playerPutPosition_GameObject.transform.position.z);
            Vector3 thingDistance = playerPosition - nowClickThing_GameObject.transform.position;
            Vector3 lookPosition = new Vector3(nowClickThing_GameObject.transform.position.x, mindControllValue.player.transform.position.y, nowClickThing_GameObject.transform.position.z);
            float thingDistanceLength = thingDistance.magnitude;
            if(thingDistanceLength <= 1.5 && thingDistanceLength >= 0.5)
                mindControllValue.player.transform.LookAt(lookPosition);
            else if(thingDistanceLength > 1.5)
                mindControllValue.player.transform.LookAt(nowClickThing_GameObject.transform);
            else if(thingDistanceLength < 0.5)
                controlLookThingPosition_Bool = false;
        }
    }
    private void LookAnswerPlane() {
        if(controlLookAnswerPutPosition_Bool == true) {
            Vector3 answerPosition = new Vector3(answerPutPosition_GameObject.transform.position.x, answerPutPosition_GameObject.transform.position.y, answerPutPosition_GameObject.transform.position.z);
            Vector3 answerDistance = answerPosition - nowClickThing_GameObject.transform.position;
            float answerDistanceLength = answerDistance.magnitude;
            if(answerDistanceLength <= 1.5 && answerDistanceLength >= 0.5)
                mindControllValue.player.transform.LookAt(answerPosition);
            else if(answerDistanceLength > 1.5)
                mindControllValue.player.transform.LookAt(answerPosition);
            else if(answerDistanceLength < 0.5)
                controlLookAnswerPutPosition_Bool = false;
        }
    }
    private void MindMoveThing() {
        if(nowClickThing_GameObject != null && controllThingMove_Bool == true && closeThingMove_Bool == false && lowAttentionStatus_Bool == false) {
            nowClickThing_GameObject.GetComponent<Enemy>().enabled = false;
            nowClickThing_GameObject.GetComponent<Rotator>().enabled = true;
            controllMouseClick_Bool = true;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().useGravity = false;
            Vector3 playerPosition = new Vector3(playerPutPosition_GameObject.transform.position.x, playerPutPosition_GameObject.transform.position.y, playerPutPosition_GameObject.transform.position.z);
            Vector3 thingDistance = playerPosition - nowClickThing_GameObject.transform.position;
            float thingDistanceLength = thingDistance.magnitude;
            thingDistance.Normalize();
            if(thingDistanceLength <= ( thingDistance.magnitude * Time.deltaTime )) {
                nowClickThing_GameObject.transform.position = playerPosition;
                mindControllValue.attention_Slider.value = 0;
                saveAttentionTime_Int = mindControllValue.timer1_Int;
                if(controllAttentionTimeUpdate_Bool == false) {
                    controllAttentionTimeUpdate_Bool = true;
                    StartCoroutine(valueTransfer.AttentionTimeUpdate(saveAttentionTime_Int));
                }
                mindControllValue.timer1_Int = 0;
                valueTransfer.controllMindTakeThing_Bool = false;
                mindControllValue.attentionSum_Int = 0;
                controllThingMove_Bool = false;
                closeThingMove_Bool = true;
                nowClickThing_GameObject.transform.parent = mindControllValue.player.transform;
                nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                thirdPersonController.GetComponent<ThirdPersonController>().enabled = true;
                thirdPersonController.GetComponent<Animator>().enabled = true;
                playerMove.controllPlayerWalk_Bool = false;
                moveUp.controllMoveUp_Bool = false;
                thingDropCount = 0;
            }
            nowClickThing_GameObject.transform.position = nowClickThing_GameObject.transform.position + ( thingDistance * Time.deltaTime * thingspeed );
        }
    }
    private void RandomMoveThing() {
        if(controllRandomThingPush_Bool == true && nowClickThing_GameObject != null) {
            controllMouseClick_Bool = false;
            closeThingMove_Bool = false;
            GameObject nowClickThing = nowClickThing_GameObject.transform.FindChild("thisthing").gameObject;
            nowClickThing.SetActive(false);
            nowClickThing_GameObject.transform.parent = null;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().useGravity = true;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
            Vector3 roomSize = new Vector3(Random.Range(-roomSize_Vector3.x, roomSize_Vector3.x), Random.Range(roomSize_Vector3.y, roomSize_Vector3.y + 25f), Random.Range(-roomSize_Vector3.z, roomSize_Vector3.z));
            Vector3 thingDistance = roomSize - nowClickThing_GameObject.transform.position;
            float thingDistanceLength = thingDistance.magnitude;
            thingDistance.Normalize();
            if(thingDistanceLength >= ( thingDistance.magnitude * Time.deltaTime )) {
                nowClickThing_GameObject.transform.position = roomSize;
                nowClickThing_GameObject.transform.GetComponent<Rigidbody>().useGravity = true;
                nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
                nowClickThing_GameObject.GetComponent<Enemy>().enabled = true;
                nowClickThing_GameObject.GetComponent<Rotator>().enabled = false;
                nowClickThing_GameObject = null;
                controllThingMove_Bool = false;
                controllRandomThingPush_Bool = false;
                lowAttentionStatus_Bool = false;
                controllThingPush_Bool = false;
                answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = false;
                mindControllValue.attention_Slider.value = 0;
            }
            if(nowClickThing_GameObject != null) {
                nowClickThing_GameObject.transform.position = nowClickThing_GameObject.transform.position + ( thingDistance * Time.deltaTime );
            }
        }
    }
    private void MindAnswerMove() {
        if(controllThingPush_Bool == true && nowClickThing_GameObject != null) {
            nowClickThing_GameObject.transform.parent = null;
            controllMouseClick_Bool = true;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().useGravity = false;
            Vector3 answerPosition = new Vector3(answerPutPosition_GameObject.transform.position.x, answerPutPosition_GameObject.transform.position.y, answerPutPosition_GameObject.transform.position.z);
            Vector3 answerdistance = answerPosition - nowClickThing_GameObject.transform.position;
            float answerdistanceLength = answerdistance.magnitude;
            answerdistance.Normalize();
            if(answerdistanceLength <= ( answerdistance.magnitude * Time.deltaTime )) {
                nowClickThing_GameObject.transform.position = answerPosition;
                mindControllValue.attention_Slider.value = 0;
                saveAttentionTime_Int = mindControllValue.timer1_Int;
                if(controllAttentionTimeUpdate_Bool == false) {
                    controllAttentionTimeUpdate_Bool = true;
                    StartCoroutine(valueTransfer.AttentionTimeUpdate(saveAttentionTime_Int));
                }
                mindControllValue.timer1_Int = 0;
                valueTransfer.controllMindPushThing_Bool = false;
                mindControllValue.attentionSum_Int = 0;
                controllThingMove_Bool = false;
                controllRandomThingPush_Bool = false;
                closeThingMove_Bool = false;
                lowAttentionStatus_Bool = false;
                controllMouseClick_Bool = false;
                controllThingPush_Bool = false;
                nowClickThing_GameObject.transform.GetComponent<Rigidbody>().useGravity = true;
                nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                nowClickThing_GameObject.transform.FindChild("thisthing").gameObject.SetActive(false);
                RecycleThing();
                thirdPersonController.GetComponent<ThirdPersonController>().enabled = true;
                thirdPersonController.GetComponent<Animator>().enabled = true;
                playerMove.controllPlayerWalk_Bool = false;
                answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = false;
                moveUp.controllMoveUp_Bool = false;
                thingDropCount = 0;
            }
            if(nowClickThing_GameObject != null) {
                nowClickThing_GameObject.transform.position = nowClickThing_GameObject.transform.position + ( answerdistance * Time.deltaTime * thingspeed );
            }
        }
    }
    private void RecycleThing() {
        if(nowClickThing_GameObject != null) {
            Vector3 roomSize = new Vector3(Random.Range(-roomSize_Vector3.x, roomSize_Vector3.x), Random.Range(roomSize_Vector3.y, roomSize_Vector3.y + 25f), Random.Range(-roomSize_Vector3.z, roomSize_Vector3.z));
            Vector3 thingdistance = roomSize - nowClickThing_GameObject.transform.position;
            float thingdistanceLength = thingdistance.magnitude;
            thingdistance.Normalize();
            if(thingdistanceLength >= ( thingdistance.magnitude * Time.deltaTime )) {
                nowClickThing_GameObject.transform.position = roomSize;
                nowClickThing_GameObject.transform.GetComponent<Rigidbody>().useGravity = true;
                nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
                nowClickThing_GameObject.GetComponent<Enemy>().enabled = true;
                nowClickThing_GameObject.GetComponent<Rotator>().enabled = false;
                nowClickThing_GameObject = null;
                answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = false;
                mindControllValue.attention_Slider.value = 0;
            }
            if(nowClickThing_GameObject != null) {
                nowClickThing_GameObject.transform.position = nowClickThing_GameObject.transform.position + ( thingdistance * Time.deltaTime );
            }
        }
    }
    private void MindDropThing() {
        if(valueTransfer.controllThingDrop_Bool == false) {
            valueTransfer.controllThingDrop_Bool = true;
            allthingDropCount += 1;
            thingDropCount += 1;
            if(thingDropCount != 3) {
                showMessage.ShowCantCatch(cantCatchFailMessage_Text);
                CancelMindControll();
            }
            else if(thingDropCount == 3) {
                showMessage.ShowCantCatch(cantCatchAllFailMessage_Text);
                if(nowClickThing_GameObject != null) {
                    thingDropCount = 0;
                    answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = false;
                    thirdPersonController.GetComponent<ThirdPersonController>().enabled = true;
                    thirdPersonController.GetComponent<Animator>().enabled = true;
                    playerMove.controllPlayerWalk_Bool = false;
                    controlLookThingPosition_Bool = false;
                    controlLookAnswerPutPosition_Bool = false;
                    saveTime_Int = mindControllValue.timer1_Int;
                    mindControllValue.timer1_Int = 0;
                    valueTransfer.controllMindTakeThing_Bool = false;
                    valueTransfer.controllMindPushThing_Bool = false;
                    mindControllValue.attentionSum_Int = 0;
                    controllRandomThingPush_Bool = true;
                    controllAttentionTimeUpdate_Bool = true;
                    mindControllValue.attention_Slider.value = 0;
                }
            }
        }
        StartCoroutine(DelayPostThingDrop());
    }
    public void CancelMindControll() {
        if(nowClickThing_GameObject != null) {
            answerPlane_GameObject.GetComponent<BoxCollider>().isTrigger = false;
            thirdPersonController.GetComponent<ThirdPersonController>().enabled = true;
            thirdPersonController.GetComponent<Animator>().enabled = true;
            playerMove.controllPlayerWalk_Bool = false;
            controlLookThingPosition_Bool = false;
            controlLookAnswerPutPosition_Bool = false;
            saveTime_Int = mindControllValue.timer1_Int;
            mindControllValue.timer1_Int = 0;
            valueTransfer.controllMindTakeThing_Bool = false;
            valueTransfer.controllMindPushThing_Bool = false;
            moveUp.controllMoveUp_Bool = false;
            mindControllValue.attentionSum_Int = 0;
            lowAttentionStatus_Bool = false;
            controllMouseClick_Bool = false;
            controllThingPush_Bool = false;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().useGravity = true;
            nowClickThing_GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            nowClickThing_GameObject.transform.parent = null;
            controllThingMove_Bool = false;
            closeThingMove_Bool = false;
            controllAttentionTimeUpdate_Bool = true;
            nowClickThing_GameObject.GetComponent<Enemy>().enabled = true;
            nowClickThing_GameObject.GetComponent<Rotator>().enabled = false;
            mindControllValue.attention_Slider.value = 0;
        }
    }
    public IEnumerator DelayPostThingDrop() {
        yield return new WaitForSeconds(3f);
        valueTransfer.controllThingDrop_Bool = false;
    }
}
