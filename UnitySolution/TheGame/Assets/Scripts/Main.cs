using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public Button fightButton;
	public Button characterButton;


	// Use this for initialization
	void Start () {
		fightButton = transform.FindChild("menu").FindChild("fight").GetComponent<Button>();
		characterButton = transform.FindChild("menu").FindChild("character").GetComponent<Button>();
		//characterButton = transform.FindChild("menu").FindChild("Button").GetComponent<Button>();
		characterButton.onClick.AddListener(OpenCharacterSettings);

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OpenCharacterSettings() {
		transform.FindChild ("menu").gameObject.SetActive (false);
		transform.FindChild("characterMenu").gameObject.SetActive (true);
	}
}
