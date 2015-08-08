using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RewardWindowScript : MonoBehaviour {

    static private RewardWindowScript _instance;
    static public RewardWindowScript Instance
    {
        get { return _instance; }
    }

	public static bool isVictory;

	// Use this for initialization
	void Start () {
        _instance = this;
        transform.FindChild("save_btn").GetComponent<Button>().onClick.AddListener(Close);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Close()
    {
        Main.Instance.OpenMainMenu();
    }

    public void Initiate()
    {
		if (!isVictory) {
			PrintNoItem();
			return;
		}

        //int willHaveItem = Random.Range(0, 4);
        int willHaveItem = 0;
        Debug.Log("willHaveItem=" + willHaveItem);
        if (willHaveItem != 0) {
            PrintNoItem();
            return;
        }

        List<Item> itemsInLvl = new List<Item>();
        foreach (Item i in ItemsDatabase.Instance.ItemsList)
            if (i.lvl == Region.SelectedRegion.lvl)
                itemsInLvl.Add(i);

        int rarity = Random.Range(0, 9);
        Item.Rarity r = (rarity < 6) ? Item.Rarity.common : (rarity == 9) ? Item.Rarity.very_rare : Item.Rarity.rare;

        List<Item> itemsInRarity = new List<Item>();
        foreach (Item i in itemsInLvl)
            if (i.rarity == r)
                itemsInRarity.Add(i);

        if (itemsInRarity.Count == 0)
        {
            PrintNoItem();
            return;
        } else {
            int index = Random.Range(0, itemsInRarity.Count);
			Item i = itemsInRarity[index];
			int m = Region.SelectedRegion.lvl;
			i.amount = (i.rarity == Item.Rarity.common)? Random.Range(1,2) * m : (i.rarity == Item.Rarity.rare)? Random.Range(2,3) * m : Random.Range(3,4) * m;
            i.id = Item.ConvertToString(i);
			Inventory.Items.Add(i);

            transform.FindChild("item").GetComponent<Text>().text = string.Format("{0} [{1}]", i.name, r.ToString());
            transform.FindChild("random").GetComponent<Text>().text = string.Format("willHaveItem={0} rarity={1} index={2}", willHaveItem, rarity, index);
        }
        
		LocalStorage.Save ();
    }

    void PrintNoItem()
    {
        transform.FindChild("item").GetComponent<Text>().text = "No item";
        transform.FindChild("random").GetComponent<Text>().text = "not 0";
    }
}
