using UnityEngine;
using System.Collections.Generic;
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
		transform.FindChild ("ItemInfoDialog").gameObject.SetActive(false);

		fightButton = transform.FindChild("menu").FindChild("fight").GetComponent<Button>();
		characterButton = transform.FindChild("menu").FindChild("character").GetComponent<Button>();
        Button inventoryButton = transform.FindChild("menu").FindChild("Inventory").GetComponent<Button>();
        inventoryButton.onClick.AddListener(OpenInventoryWindow);

		//characterButton = transform.FindChild("menu").FindChild("Button").GetComponent<Button>();
		characterButton.onClick.AddListener(OpenCharacterSettings);
		fightButton.onClick.AddListener(OpenMapScreen);

        LocalStorage.Load();
		//FillCharacterMenu ();
        SetWinRate();
        SetPointsAviable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetWinRate()
    {
        Transform wr = transform.FindChild("menu").FindChild("wr");
        wr.GetComponent<Text>().text = LocalStorage.last10games.ToString();
		Transform lvl = transform.FindChild("menu").FindChild("lvl");
		lvl.GetComponent<Text>().text = LocalStorage.lvl.ToString();
		Transform exp = transform.FindChild("menu").FindChild("exp");
		exp.GetComponent<Text> ().text = string.Format ("{0}({1})", LocalStorage.exp.ToString (), LocalStorage.lvl_exp [LocalStorage.lvl + 1].ToString ());
    }

    void SetPointsAviable()
    {
        Transform pa = transform.FindChild("menu").FindChild("pts");
        int p = 0;
        foreach (Unit u in LocalStorage.player)
            p += u.points;
        pa.GetComponent<Text>().text = p.ToString();
        transform.FindChild("menu").FindChild("fight_blocker").gameObject.SetActive(p > 0);
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
        characterMenu.FindChild("pts1").GetComponent<Text>().text = LocalStorage.player[0].points.ToString(); ;
        characterMenu.FindChild("pts2").GetComponent<Text>().text = LocalStorage.player[1].points.ToString(); ;
        characterMenu.FindChild("pts3").GetComponent<Text>().text = LocalStorage.player[2].points.ToString(); ;

		characterMenu.FindChild ("cancel_btn").GetComponent<Button> ().onClick.AddListener (OpenMainMenu);
		CharacterMenu.Instance.CheckAviablePoints ();
	}

	public void OpenCharacterSettings() {
		FillCharacterMenu ();
        OpenTratataWindow("characterMenu");
	}

	public void OpenMainMenu()
    {
        SetWinRate();
        SetPointsAviable();
        OpenTratataWindow("menu");
	}

    public void OpenBattleWindow()
    {
        BattleView.Instance.ClearDamage(null);
        BattleController.Instance.Initiate(LocalStorage.player);
        OpenTratataWindow("BattleWindow");
    }

	public void OpenMapScreen()
	{
		OpenTratataWindow("mapScreen");

	}

	public void OpenRegionDescriptionWindow()
	{
		RegionDescription.Instance.Initiate ();
		OpenTratataWindow("RegionDescription");
	}

    public void OpenInventoryWindow()
    {
        InventoryWindowScript.Instance.Initiate();
        OpenTratataWindow("InventoryWindow");
    }

    public void OpenRewardWindow()
    {
        RewardWindowScript.Instance.Initiate();
        OpenTratataWindow("RewardDescription");
    }

    void OpenTratataWindow(string name)
    {
		List<string> windows = new List<string>() { "menu", "characterMenu", "BattleWindow", "mapScreen", "RegionDescription", "InventoryWindow", "RewardDescription" };
        foreach (string s in windows)
            transform.FindChild(s).gameObject.SetActive(s == name);
    }
}
