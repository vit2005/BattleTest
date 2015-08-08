using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    static private Inventory _instance = new Inventory();
    static public Inventory Instance
    {
        get { return _instance; }
    }

    public static List<Item> Items = new List<Item>();
    public static Dictionary<string, Item> ItemsSet = new Dictionary<string, Item>();

    public void Initiate()
    {
        Debug.Log("Initiate inventary. Items:" + Items.Count);
        Items = new List<Item>();
        foreach(string s in LocalStorage.items) {
            Items.Add(Item.ConvertToItem(s));
        }

		ItemsSet = new Dictionary<string, Item>();
        foreach (string s in LocalStorage.ItemsSet.Keys)
        {
            if (!string.IsNullOrEmpty(LocalStorage.ItemsSet[s]))
                ItemsSet.Add(s, Item.ConvertToItem(LocalStorage.ItemsSet[s]));
        }

        InitiateUnitsItems();
    }

	public List<string> ConvertList()
	{
		List<string> result = new List<string> ();
		foreach (Item i in Items) {
			result.Add(Item.ConvertToString(i));
		}
		return result;
	}

    void InitiateUnitsItems()
    {
        foreach(string s in ItemsSet.Keys)
        {
            if (s.Contains("u1") && s.Contains("a"))
                LocalStorage.player[0].Items.Add(ItemsSet[s]);
            else if (s.Contains("u2") && s.Contains("a"))
                LocalStorage.player[0].Items.Add(ItemsSet[s]);
            else if (s.Contains("u3") && s.Contains("a"))
                LocalStorage.player[0].Items.Add(ItemsSet[s]);
        }
    }
}
