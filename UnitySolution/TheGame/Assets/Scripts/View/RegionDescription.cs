using UnityEngine;
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
