using UnityEngine;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RegionDescription : MonoBehaviour {


    static private RegionDescription _instance;
    static public RegionDescription Instance
    {
        get { return _instance; }
    }




	// Use this for initialization
	void Start () {
		_instance = this;
		transform.FindChild ("fight_btn").gameObject.SetActive (true);
		transform.FindChild("fight_btn").GetComponent<Button>().onClick.AddListener(onFight);
		transform.FindChild("cancel_btn").GetComponent<Button>().onClick.AddListener(onCancel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initiate()
    {
		transform.FindChild ("oblast").GetComponent<Text> ().text = Region.SelectedRegion.oblast;
		transform.FindChild ("district").GetComponent<Text> ().text = Region.SelectedRegion.distinct;
		transform.FindChild ("lvl").GetComponent<Text> ().text = GetRecomendedLvl(Region.SelectedRegion.lvl);
		int added_exp = LocalStorage.addedExp ();
        string s_added_exp = (added_exp > 0) ? added_exp.ToString("#,#", CultureInfo.InvariantCulture).Replace(",","'") : "0";
        string exp = LocalStorage.exp.ToString("#,#", CultureInfo.InvariantCulture).Replace(",", "'");
        string added_lvl = LocalStorage.addedLvl(added_exp).ToString();
        string next_lvl_exp = LocalStorage.lvl_exp [LocalStorage.lvl + 1].ToString ();
        transform.FindChild("exp").GetComponent<Text>().text = String.Format("{2} [+{0}] / {3} (+{1}lvl)", s_added_exp, added_lvl, exp, next_lvl_exp);
		transform.FindChild ("fight_btn").gameObject.SetActive(added_exp > 0);

		//Region.SelectedRegion.lvl

    }

	private string GetRecomendedLvl(int lvl)
	{
		if (lvl == 1)
			return "1..9";
		else
			return string.Format ("{0}..{1}", (lvl - 1) * 10, (lvl * 10) - 1);
		
	}

    void onFight()
    {
        Main.Instance.OpenBattleWindow();
    }

	void onCancel()
	{
		Main.Instance.OpenMapScreen();
	}
}
