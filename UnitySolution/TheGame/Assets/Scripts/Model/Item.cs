using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Item {

    public string name;
    public string id;
    public string description;
    public Rarity rarity;
    public ItemType itrmType;
    public int spriteName;
	public int lvl;
	public Atribute atribute;
	public int amount;

    public enum Rarity
    {
        common = 1,
        rare = 2,
        very_rare = 3
    }

    public enum ItemType
    {
        armor = 1,
        skill = 2,
		skill_part = 3,
        other = 0
    }

	public enum Atribute
	{
		ATK = 1,
		DEF = 2,
		AGI = 3,
		Damage = 10,
		Heal = 11,
		Debuff = 12,
		Buff = 13,
		Control = 14
	}
	

    public static Item ConvertToItem(string s)
    {
        string[] param = s.Replace(@"\","").Replace("\"","").Split(new char[] { '_' });
        Item i = new Item();
        i.name = param[0];
        i.description = param[1];
		int rarity = Convert.ToInt32 (param [2]);
		i.rarity = (Rarity)rarity;
		int type = Convert.ToInt32 (param [3]);
		i.itrmType = (ItemType)type;
        i.spriteName = Convert.ToInt32(param[4]);
		i.lvl = Convert.ToInt32 (param [5]);
		int attr = Convert.ToInt32 (param [6]);
		i.atribute = (Atribute)attr;
		i.amount = Convert.ToInt32 (param [7]);
        return i;
    }

    public static string ConvertToString(Item i)
    {
		return string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}", 
		                     i.name, i.description, ((int)i.rarity).ToString(), ((int)i.itrmType).ToString(), i.spriteName.ToString(), i.lvl.ToString(), ((int)i.atribute).ToString(), i.amount.ToString());
    }
}
