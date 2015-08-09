using UnityEngine;
using System.Collections.Generic;

public class LocalStorage
{

    public static List<Unit> player;
    public static bool infoLoaded = false;
    public static int lvl;
    public static int exp;
    public static int last10games;

    public static List<int> lvl_exp;

    public static List<string> items;
    public static Dictionary<string, string> ItemsSet;
    public static List<string> ItemsSetKeys = new List<string>() { "u1a1", "u1a2", "u1a3", "u1s1", "u1s2", "u1s3", 
                                                                    "u2a1", "u2a2", "u2a3", "u2s1", "u2s2", "u2s3",
                                                                    "u3a1", "u3a2", "u3a3", "u3s1", "u3s2", "u3s3"};
    static LocalStorage()
    {
        CalculateConsts();
    }


    public static void Save()
    {
        JSONObject json = new JSONObject();
        if (player == null)
        {
            player = new List<Unit>();
            //player.Add(new Unit(30,50,40,0));
            //player.Add(new Unit(50,40,30,0));
            //player.Add(new Unit(40,30,50,0));
            player.Add(new Unit(0, 0, 0, 10));
            player.Add(new Unit(0, 0, 0, 10));
            player.Add(new Unit(0, 0, 0, 10));
        }
        if (items == null)
            items = new List<string>();

        bool loadItemsSet = ItemsSet != null;

        ItemsSet = new Dictionary<string, string>();
        foreach (string k in ItemsSetKeys)
            ItemsSet.Add(k, string.Empty);

        if (loadItemsSet)
            foreach (string s in Inventory.ItemsSet.Keys)
                ItemsSet[s] = Item.ConvertToString(Inventory.ItemsSet[s]);
        

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
        json.AddField("exp", exp);

        items = Inventory.Instance.ConvertList();

        JSONObject[] intentory_array = new JSONObject[items.Count];

        for (int i = 0; i < items.Count; i++)
        {
            intentory_array[i] = JSONObject.CreateStringObject(items[i]);
        }
        JSONObject inventory_object = new JSONObject(intentory_array);
        //json.AddField("tempArray", inventory);

        JSONObject itemsSet_object = new JSONObject(ItemsSet);

        PlayerPrefs.SetString("player", json.ToString());
        PlayerPrefs.SetString("inventory", inventory_object.ToString());
        PlayerPrefs.SetString("ItemsSet", itemsSet_object.ToString());
        Debug.Log("Saved: " + json);
        Debug.Log("Saved: " + inventory_object);
        Debug.Log("Saved: " + itemsSet_object);
    }

    public static void Load()
    {


        //Save();
        if (!PlayerPrefs.HasKey("player") || !PlayerPrefs.HasKey("inventory") || !PlayerPrefs.HasKey("ItemsSet"))
        {
            lvl = 1;
            exp = 0;
            last10games = 0;

            Save();
            return;
        }

        string savedData = PlayerPrefs.GetString("player");
        JSONObject json = new JSONObject(savedData);
        
        player = new List<Unit>();
        player.Add(new Unit((int)json["atk0"].n, (int)json["def0"].n, (int)json["agi0"].n, (int)json["pts0"].n));
        player.Add(new Unit((int)json["atk1"].n, (int)json["def1"].n, (int)json["agi1"].n, (int)json["pts1"].n));
        player.Add(new Unit((int)json["atk2"].n, (int)json["def2"].n, (int)json["agi2"].n, (int)json["pts2"].n));
        last10games = (int)json["lg"].n;
        exp = (int)json["exp"].n;
        lvl += addedLvl(exp);

        items = new List<string>();
        JSONObject inventory_object = new JSONObject(PlayerPrefs.GetString("inventory"));
        List<JSONObject> inventory_list = inventory_object.list;
        foreach (JSONObject o in inventory_list)
            items.Add(o.ToString());

        JSONObject oItemsSet = new JSONObject(PlayerPrefs.GetString("ItemsSet"));
        ItemsSet = oItemsSet.ToDictionary();
		Inventory.Instance.Initiate ();

        infoLoaded = true;
        Debug.Log("Loaded: " + json);
        Debug.Log("Loaded: " + inventory_object);
        Debug.Log("Loaded: " + oItemsSet);
    }

    public static void SaveBattleResult(bool isVictory)
    {
        CalcMatch(isVictory);

        Save();
    }

    static void CalcMatch(bool isVictory)
    {
        if (isVictory)
        {
            int added_exp = addedExp();

            int added_lvl = addedLvl(added_exp);

            Debug.Log("added_exp=" + added_exp + " added_lvl=" + added_lvl);

            if (added_lvl != 0)
            {
                foreach (Unit u in player)
                    u.points += 5 * (added_lvl);
            }
            lvl += added_lvl;

            exp += added_exp;
        }

        if (isVictory && (last10games < 5))
            last10games++;
        if (!isVictory && (last10games > -5))
            last10games--;
    }

    static public int addedExp()
    {
        int dangeon_lvl = Region.SelectedRegion.lvl;
        int expected_player_lvl = (int)(lvl / 10) + 1;
        double dangeon_lvl_dependency = (double)System.Math.Pow(2, (dangeon_lvl - expected_player_lvl));
        double player_lvl_dependency = (double)System.Math.Pow(lvl, 1.67) * (double)50;
        double added_exp = dangeon_lvl_dependency * player_lvl_dependency;

        return (int)added_exp;
    }

    static public int addedLvl(int added_exp)
    {
        int new_lvl = 0;
        for (int i = 0; i < 200; i++)
        {
            if (exp + added_exp >= lvl_exp[i])
            {
                new_lvl = i;
            }
        }
        if (new_lvl < lvl)
            new_lvl = lvl;
        return new_lvl - lvl;
    }

    static void CalculateConsts()
    {
        lvl_exp = new List<int>();
        lvl_exp.Add(0);
        for (int i = 0; i < 200; i++)
        {
            lvl_exp.Add((int)((double)System.Math.Pow(i, 2.67f) * (double)100));
        }
    }

}
