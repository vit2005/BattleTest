using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

	private static CharacterMenu _instance;
	public static CharacterMenu Instance{
		get{return _instance;}
	}


	// Use this for initialization
	void Start () {
		_instance = this;
		InitiateButtons ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitiateButtons()
	{
		//List<string> stats = new List<string>(){"atk","def","agi"};
		//List<string> plusminus = new List<string>(){"plus","minus"};
		//Button b;
		//for (int i = 1; i<4; i++) 
		//	for (int j = 0; j<3; j++)
		//		for (int k = 0; k<2; k++) {
		//			b = transform.FindChild("unit"+i.ToString()).FindChild(stats[j]+"_"+plusminus[k]).GetComponent<Button>();
		//			b.onClick.AddListener(delegate{ChangeStat(stats[j],i,k == 0);});
		//		}
		transform.FindChild ("unit1").FindChild ("atk_plus").GetComponent<Button> ().onClick.AddListener (atk1plus);
		transform.FindChild ("unit2").FindChild ("atk_plus").GetComponent<Button> ().onClick.AddListener (atk2plus);
		transform.FindChild ("unit3").FindChild ("atk_plus").GetComponent<Button> ().onClick.AddListener (atk3plus);
		transform.FindChild ("unit1").FindChild ("atk_minus").GetComponent<Button> ().onClick.AddListener (atk1minus);
		transform.FindChild ("unit2").FindChild ("atk_minus").GetComponent<Button> ().onClick.AddListener (atk2minus);
		transform.FindChild ("unit3").FindChild ("atk_minus").GetComponent<Button> ().onClick.AddListener (atk3minus);
		transform.FindChild ("unit1").FindChild ("def_plus").GetComponent<Button> ().onClick.AddListener (def1plus);
		transform.FindChild ("unit2").FindChild ("def_plus").GetComponent<Button> ().onClick.AddListener (def2plus);
		transform.FindChild ("unit3").FindChild ("def_plus").GetComponent<Button> ().onClick.AddListener (def3plus);
		transform.FindChild ("unit1").FindChild ("def_minus").GetComponent<Button> ().onClick.AddListener (def1minus);
		transform.FindChild ("unit2").FindChild ("def_minus").GetComponent<Button> ().onClick.AddListener (def2minus);
		transform.FindChild ("unit3").FindChild ("def_minus").GetComponent<Button> ().onClick.AddListener (def3minus);
		transform.FindChild ("unit1").FindChild ("agi_plus").GetComponent<Button> ().onClick.AddListener (agi1plus);
		transform.FindChild ("unit2").FindChild ("agi_plus").GetComponent<Button> ().onClick.AddListener (agi2plus);
		transform.FindChild ("unit3").FindChild ("agi_plus").GetComponent<Button> ().onClick.AddListener (agi3plus);
		transform.FindChild ("unit1").FindChild ("agi_minus").GetComponent<Button> ().onClick.AddListener (agi1minus);
		transform.FindChild ("unit2").FindChild ("agi_minus").GetComponent<Button> ().onClick.AddListener (agi2minus);
		transform.FindChild ("unit3").FindChild ("agi_minus").GetComponent<Button> ().onClick.AddListener (agi3minus);

		transform.FindChild ("save_btn").GetComponent<Button> ().onClick.AddListener (save_btn_click);
	}

	public void CheckAviablePoints()
	{
		for (int i = 1; i<4; i++) {
			Text a = transform.FindChild("pts"+i.ToString()).GetComponent<Text>();
			if (a.text == "0")
				ChangePlusButtonsStatus(i.ToString(),false);
			else
				ChangePlusButtonsStatus(i.ToString(),true);

			CheckMinusButtonsStatus(transform.FindChild("unit"+i.ToString()));
		}
	}

	void ChangePlusButtonsStatus(string unit, bool state)
	{
		Transform u = transform.FindChild("unit"+unit);
		u.FindChild("atk_plus").gameObject.SetActive(state);
		u.FindChild("def_plus").gameObject.SetActive(state);
		u.FindChild("agi_plus").gameObject.SetActive(state);
	}

	void CheckMinusButtonsStatus(Transform unit)
	{
        unit.FindChild("atk_minus").gameObject.SetActive(unit.FindChild("atk").GetComponent<Text>().text != "0");
        unit.FindChild("def_minus").gameObject.SetActive(unit.FindChild("def").GetComponent<Text>().text != "0");
        unit.FindChild("agi_minus").gameObject.SetActive(unit.FindChild("agi").GetComponent<Text>().text != "0");
	}

	void ChangeStat(string parameter, int unit_number, bool increase)
	{
		Debug.Log ("Click");
		Text a = transform.FindChild("pts"+unit_number.ToString()).GetComponent<Text>();
		if ((a.text == "0") && (increase))
			return;
		Transform unit = transform.FindChild("unit"+unit_number.ToString());
		Text t = unit.FindChild (parameter).GetComponent<Text> ();
		if ((t.text == "0") && (!increase))
			return;
		t.text = (increase) ? (int.Parse (t.text) + 1).ToString () : (int.Parse (t.text) - 1).ToString ();
		a.text = (!increase) ? (int.Parse (a.text) + 1).ToString () : (int.Parse (a.text) - 1).ToString ();
		CheckAviablePoints ();
	}

	void atk1plus()	{	ChangeStat("atk",1,true);	}
	void atk2plus()	{	ChangeStat("atk",2,true);	}
	void atk3plus()	{	ChangeStat("atk",3,true);	}
	void atk1minus()	{	ChangeStat("atk",1,false);	}
	void atk2minus()	{	ChangeStat("atk",2,false);	}
	void atk3minus()	{	ChangeStat("atk",3,false);	}
	void def1plus()	{	ChangeStat("def",1,true);	}
	void def2plus()	{	ChangeStat("def",2,true);	}
	void def3plus()	{	ChangeStat("def",3,true);	}
	void def1minus()	{	ChangeStat("def",1,false);	}
	void def2minus()	{	ChangeStat("def",2,false);	}
	void def3minus()	{	ChangeStat("def",3,false);	}
	void agi1plus()	{	ChangeStat("agi",1,true);	}
	void agi2plus()	{	ChangeStat("agi",2,true);	}
	void agi3plus()	{	ChangeStat("agi",3,true);	}
	void agi1minus()	{	ChangeStat("agi",1,false);	}
	void agi2minus()	{	ChangeStat("agi",2,false);	}
	void agi3minus()	{	ChangeStat("agi",3,false);	}

	void save_btn_click(){	
		int atk, def, agi, pts;
		LocalStorage.player = new List<Unit> ();
		atk = int.Parse(transform.FindChild ("unit1").FindChild ("atk").GetComponent<Text> ().text);
		def = int.Parse(transform.FindChild ("unit1").FindChild ("def").GetComponent<Text> ().text);
		agi = int.Parse(transform.FindChild ("unit1").FindChild ("agi").GetComponent<Text> ().text);
		pts = int.Parse(transform.FindChild ("pts1").GetComponent<Text> ().text);
		LocalStorage.player.Add(new Unit (atk, def, agi, pts));
		atk = int.Parse(transform.FindChild ("unit2").FindChild ("atk").GetComponent<Text> ().text);
		def = int.Parse(transform.FindChild ("unit2").FindChild ("def").GetComponent<Text> ().text);
		agi = int.Parse(transform.FindChild ("unit2").FindChild ("agi").GetComponent<Text> ().text);
		pts = int.Parse(transform.FindChild ("pts2").GetComponent<Text> ().text);
		LocalStorage.player.Add(new Unit (atk, def, agi, pts));
		atk = int.Parse(transform.FindChild ("unit3").FindChild ("atk").GetComponent<Text> ().text);
		def = int.Parse(transform.FindChild ("unit3").FindChild ("def").GetComponent<Text> ().text);
		agi = int.Parse(transform.FindChild ("unit3").FindChild ("agi").GetComponent<Text> ().text);
		pts = int.Parse(transform.FindChild ("pts3").GetComponent<Text> ().text);
		LocalStorage.player.Add(new Unit (atk, def, agi, pts));
		LocalStorage.Save ();
		Main.Instance.OpenMainMenu ();
	}
}
