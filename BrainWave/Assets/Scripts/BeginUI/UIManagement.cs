using UnityEngine;


public class UIManagement : MonoBehaviour {
	[SerializeField] UIControll startUI;
 	[SerializeField] UIControll[] secondUI;
	[SerializeField] UIControll[] thirdUI;
	[SerializeField] UIControll[] fourthUI;
	[TextArea(5, 10)]
	[SerializeField] string showUIStatus;
	string temp;
	void Update() {
		UIStatus();
	}
	private void UIStatus() {
		temp += "UI Page : " + startUI.nowUINumber + " UI Name : " + startUI.uiName + "\n";
		UIData(secondUI);
		UIData(thirdUI);
		UIData(fourthUI);
		showUIStatus = temp;
		temp = "";

	}
	private void UIData(UIControll[] uiLevel) {
		for (int i = 0; i < uiLevel.Length; i++) {
			temp += "UI Page : " + uiLevel[i].nowUINumber + " UI Name : " + uiLevel[i].uiName + "\n";
		}
	}
}
