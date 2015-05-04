using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	private static Main _instance;
	public static Main Instance{
		get{return _instance;}
	}

	public Button fightButton;
	public Button characterButton;


	// Use this for initialization
	void Start () {
		_instance = this;
		fightButton = transform.FindChild("menu").FindChild("fight").GetComponent<Button>();
		characterButton = transform.FindChild("menu").FindChild("character").GetComponent<Button>();
		//characterButton = transform.FindChild("menu").FindChild("Button").GetComponent<Button>();
		characterButton.onClick.AddListener(OpenCharacterSettings);

		LoadPlayer ();
		FillCharacterMenu ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadPlayer()
	{
		if (PlayerPrefs.HasKey ("player")) {
			LocalStorage.Load();
		} else {
			LocalStorage.Save();
		}
	}

	void FillCharacterMenu()
	{
		Transform characterMenu = transform.FindChild ("characterMenu");
		Transform unit1 = characterMenu.FindChild ("unit1");
		Transform unit2 = characterMenu.FindChild ("unit2");
		Transform unit3 = characterMenu.FindChild ("unit3");
		unit1.FindChild ("atk").GetComponent<Text> ().text = LocalStorage.player [0].ATK.ToString();
		unit1.FindChild ("def").GetComponent<Text> ().text = LocalStorage.player [0].DEF.ToString();
		unit1.FindChild ("agi").GetComponent<Text> ().text = LocalStorage.player [0].AGI.ToString();
		unit2.FindChild ("atk").GetComponent<Text> ().text = LocalStorage.player [1].ATK.ToString();
		unit2.FindChild ("def").GetComponent<Text> ().text = LocalStorage.player [1].DEF.ToString();
		unit2.FindChild ("agi").GetComponent<Text> ().text = LocalStorage.player [1].AGI.ToString();
		unit3.FindChild ("atk").GetComponent<Text> ().text = LocalStorage.player [2].ATK.ToString();
		unit3.FindChild ("def").GetComponent<Text> ().text = LocalStorage.player [2].DEF.ToString();
		unit3.FindChild ("agi").GetComponent<Text> ().text = LocalStorage.player [2].AGI.ToString();
		characterMenu.FindChild ("cancel_btn").GetComponent<Button> ().onClick.AddListener (OpenMainMenu);
		CharacterMenu.Instance.CheckAviablePoints ();
	}

	public void OpenCharacterSettings() {
		FillCharacterMenu ();
		transform.FindChild("menu").gameObject.SetActive (false);
		transform.FindChild("characterMenu").gameObject.SetActive (true);
	}

	public void OpenMainMenu()
	{
		transform.FindChild("menu").gameObject.SetActive (true);
		transform.FindChild("characterMenu").gameObject.SetActive (false);
	}
}
