using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIControll : MonoBehaviour {
	[Tooltip("Input Your UI Name")]
	[SerializeField] public string uiName;
	[Tooltip("0 : OuterLayer, Other : InnerLayer.")]
	[SerializeField] public GameObject[] ui;
	[HideInInspector] public int nowUINumber;

	public void Enter(int UINumber) {
		ui[UINumber].SetActive(true);
		ui[0].SetActive(false);
		nowUINumber = UINumber;
	}
	public void Back(int UINumber) {
		ui[0].SetActive(true);
		ui[UINumber].SetActive(false);
		nowUINumber = 0;
	}
	public void EnterOther(int UINumber) {
		ui[UINumber].SetActive(true);
		ui[nowUINumber].SetActive(false);
		nowUINumber = UINumber;
	}
	public void ExitGame() {
		Application.Quit();
	}
}
