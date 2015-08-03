using UnityEngine;
using System.Collections.Generic;

public class LocalStorage {

	public static List<Unit> player;
	public static bool infoLoaded = false;
    public static int lvl = 1;
    public static int exp = 0;
    public static int last10games = 0;

	public static List<int> lvl_exp;
	static LocalStorage()
	{
		CalculateConsts ();
	}


	public static void Save() 
	{
		JSONObject json = new JSONObject();
		if (player == null) {
			player = new List<Unit>();
			//player.Add(new Unit(30,50,40,0));
			//player.Add(new Unit(50,40,30,0));
			//player.Add(new Unit(40,30,50,0));
            player.Add(new Unit(0, 0, 0, 10));
            player.Add(new Unit(0, 0, 0, 10));
            player.Add(new Unit(0, 0, 0, 10));
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
        json.AddField("lg", last10games);
        json.AddField("lvl", lvl);
        json.AddField("exp", exp);

		PlayerPrefs.SetString("player", json.ToString());
	}

	public static void Load()
	{
		//Save();
        if (!PlayerPrefs.HasKey("player"))
        {
            Save();
            return;
        }
            
		string savedData = PlayerPrefs.GetString("player");
		JSONObject json = new JSONObject(savedData);
		Debug.Log(json);
		player = new List<Unit> ();
		player.Add(new Unit((int)json["atk0"].n,(int)json["def0"].n,(int)json["agi0"].n, (int)json["pts0"].n));
		player.Add(new Unit((int)json["atk1"].n,(int)json["def1"].n,(int)json["agi1"].n, (int)json["pts1"].n));
		player.Add(new Unit((int)json["atk2"].n,(int)json["def2"].n,(int)json["agi2"].n, (int)json["pts2"].n));
        last10games = (int)json["lg"].n;
        lvl = (int)json["lvl"].n;
        exp = (int)json["exp"].n;

		infoLoaded = true;
	}

    public static void SaveBattleResult(bool isVictory)
    {
		CalcMatch (isVictory);

        Save();
    }

	static void CalcMatch(bool isVictory)
	{
        if (isVictory)
        {
            int dangeon_lvl = Region.SelectedRegion.lvl;
            int expected_player_lvl = (int)(lvl / 10) + 1;

            int added_exp = (int)(((double)System.Math.Pow(lvl, 1.67) * (double)50) * System.Math.Pow(10, (dangeon_lvl - expected_player_lvl)));

            int new_lvl = 0;
            for (int i = 0; i < 200; i++)
            {
                if (exp + added_exp >= lvl_exp[i])
                {
                    new_lvl = i;
                }
            }
            Debug.Log("lvl=" + lvl + " new_lvl=" + new_lvl);

            if (lvl != new_lvl)
            {
                foreach (Unit u in player)
                    u.points += 5 * (new_lvl - lvl);
            }
            lvl = new_lvl;

            exp += added_exp;
        }

		if (isVictory && (last10games < 5))
			last10games++;
		if (!isVictory && (last10games > -5))
			last10games--;
	}

	static void CalculateConsts()
	{
		lvl_exp = new List<int>();
		lvl_exp.Add(0);
		for (int i = 0; i<200; i++)
		{
			lvl_exp.Add((int)((double)System.Math.Pow(i, 2.67f) * (double)100));
		}
	}

}
