using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterControll : MonoBehaviour {
    [SerializeField] ValueTransfer valueTransfer;
    [SerializeField] MindControllValue mindControllValue;
    [SerializeField] ShowMessage showMessage;
    [SerializeField] MindControllAction mindControllAction;
    [SerializeField] Text beAttackedMessage_Text;
    [SerializeField] Text playerDeadMessage_Text;
    [SerializeField] GameObject[] monster;
    [SerializeField] GameObject[] monsterPoint;
    void Awake() {
        beAttackedMessage_Text.GetComponent<Text>().enabled = false;
        playerDeadMessage_Text.GetComponent<Text>().enabled = false;
    }
    void Update() {
        MonsterAction(monster[0], monsterPoint[0]);
        MonsterAction(monster[1], monsterPoint[1]);
        MonsterAction(monster[2], monsterPoint[2]);
    }
    public void MonsterAction(GameObject monster, GameObject monsterPoint) {
        Vector3 LookPlayerPosition = new Vector3(mindControllValue.player.transform.position.x, monsterPoint.transform.position.y, mindControllValue.player.transform.position.z);
        Vector3 AttackPlayerPosition = new Vector3(mindControllValue.player.transform.position.x, mindControllValue.player.transform.position.y, mindControllValue.player.transform.position.z);
        Vector3 LookBackPosition = new Vector3(monsterPoint.transform.position.x, monsterPoint.transform.position.y, monsterPoint.transform.position.z);
        Vector3 PlayerHeight = new Vector3(monsterPoint.transform.position.x, mindControllValue.player.transform.position.y, monsterPoint.transform.position.z);
        Vector3 MonsterHeight = new Vector3(monsterPoint.transform.position.x, monsterPoint.transform.position.y, monsterPoint.transform.position.z);

        if(Vector3.Distance(MonsterHeight, PlayerHeight) < 6) {
            if(Vector3.Distance(monsterPoint.transform.position, mindControllValue.player.transform.position) < 13) {
                if(mindControllValue.meditationSum_Int < 125) {
                    MonsterLook(monster, LookPlayerPosition, AttackPlayerPosition, 0.7f);
                    if(mindControllValue.meditationSum_Int <= 10 && Vector3.Distance(monster.transform.position, AttackPlayerPosition) <= 0.7f) {
                        if(valueTransfer.controllHpCost_Bool == false) {
                            valueTransfer.controllHpCost_Bool = true;
                            StartCoroutine(showMessage.ShowMessages(beAttackedMessage_Text));
                            mindControllAction.CancelMindControll();
                            mindControllValue.player.transform.Translate(Vector3.forward * Time.deltaTime * -25f);
                            mindControllValue.playerHp_Slider.value--;
                            mindControllValue.playerHp_Int--;
                            StartCoroutine(DelayPostHpCost());
                        }
                        if(mindControllValue.playerHp_Int == 0) {
                            beAttackedMessage_Text.GetComponent<Text>().enabled = false;
                            valueTransfer.controllHpCost_Bool = true;
                            mindControllAction.CancelMindControll();
                            Vector3 startPosition = new Vector3(1.325f, 10.69f, -1f);
                            mindControllValue.player.transform.position = startPosition;
                            StartCoroutine(showMessage.ShowMessages(playerDeadMessage_Text));
                            mindControllValue.playerDead_Int += 1;
                            mindControllValue.playerHp_Slider.value = 10;
                            mindControllValue.playerHp_Int = 10;
                        }
                    }
                }
                else
                    MonsterLook(monster, LookBackPosition, LookBackPosition, 1f);
            }
            else
                MonsterLook(monster, LookBackPosition, LookBackPosition, 1f);
        }
    }
    private void MonsterLook(GameObject monster, Vector3 lookAtPosition, Vector3 goToPosition, float distance) {
        monster.transform.LookAt(lookAtPosition);
        if(Vector3.Distance(monster.transform.position, goToPosition) > distance) {
            monster.transform.Rotate(new Vector3(-90, 0, 0));
            monster.transform.Translate(Vector3.forward * Time.deltaTime * 3f);
        }
        else if(Vector3.Distance(monster.transform.position, goToPosition) <= distance) {
            monster.transform.Rotate(new Vector3(-90, 0, 0));
        }
    }
    public IEnumerator DelayPostHpCost() {
        yield return new WaitForSeconds(1f);
        valueTransfer.controllHpCost_Bool = false;
    }
}
