using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Region : MonoBehaviour {

    public static List<Region> Regions = new List<Region>();
	public static Region SelectedRegion;
	public string oblast;
	public string distinct;
    public int lvl;

	// Use this for initialization
	void Start () {
		Button b = transform.FindChild("go").GetComponent<Button>();
		b.onClick.AddListener(GoLvl);

        Regions.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GoLvl()
	{
		SelectedRegion = this;
		Main.Instance.OpenRegionDescriptionWindow ();
	}
}
