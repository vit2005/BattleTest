﻿using UnityEngine;
using System.Collections.Generic;

public class LocalStorage {

	public static List<Unit> player;
	public static bool infoLoaded = false;

	public static void Save() 
	{
		JSONObject json = new JSONObject();
		if (player == null) {
			player = new List<Unit>();
			player.Add(new Unit(30,50,40,0));
			player.Add(new Unit(50,40,30,0));
			player.Add(new Unit(40,30,50,0));
		}
		json.AddField("atk0", player[0].ATK);
		json.AddField("atk1", player[1].ATK);
		json.AddField("atk2", player[2].ATK);
		json.AddField("def0", player[0].DEF);
		json.AddField("def1", player[1].DEF);
		json.AddField("def2", player[2].DEF);
		json.AddField("agi0", player[0].AGI);
		json.AddField("agi1", player[1].AGI);
		json.AddField("agi2", player[2].AGI);
		json.AddField("pts0", player[0].points);
		json.AddField("pts1", player[1].points);
		json.AddField("pts2", player[2].points);


		PlayerPrefs.SetString("player", json.ToString());
	}

	public static void Load()
	{
		string savedData = PlayerPrefs.GetString("player");
		JSONObject json = new JSONObject(savedData);
		Debug.Log(json);
		player = new List<Unit> ();
		player.Add(new Unit((int)json["atk0"].n,(int)json["def0"].n,(int)json["agi0"].n, (int)json["pts0"].n));
		player.Add(new Unit((int)json["atk1"].n,(int)json["def1"].n,(int)json["agi1"].n, (int)json["pts1"].n));
		player.Add(new Unit((int)json["atk2"].n,(int)json["def2"].n,(int)json["agi2"].n, (int)json["pts2"].n));
		infoLoaded = true;
	}

}
