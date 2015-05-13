using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mapScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button b = transform.FindChild("map").FindChild("lvl1").GetComponent<Button>();
		b.onClick.AddListener(GoLvl);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GoLvl()
	{
		Main.Instance.OpenBattleWindow ();
	}
}
