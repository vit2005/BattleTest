using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryWindowScript : MonoBehaviour {

    static private InventoryWindowScript _instance;
    static public InventoryWindowScript Instance
    {
        get { return _instance; }
    }

    public GameObject DefaultItem;
    public List<Sprite> sprites = new List<Sprite>();

	// Use this for initialization
	void Start () {
        _instance = this;
		transform.FindChild("save_btn").GetComponent<Button>().onClick.AddListener(Save);
        transform.FindChild("cancel_btn").GetComponent<Button>().onClick.AddListener(Cancel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Save()
	{
        Inventory.Items = new List<Item>();
        Inventory.ItemsSet = new Dictionary<string, Item>();
        foreach(Transform t in transform.FindChild("InventoryTable"))
        {
            if (t.childCount > 0)
                Inventory.Items.Add(t.GetChild(0).GetComponent<InventoryDraggedItem>().item);
        }
        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                Transform a_slot = transform.FindChild("unit" + i.ToString()).FindChild("armor_slot" + j.ToString());
                if (a_slot.childCount > 0)
                    Inventory.ItemsSet.Add(string.Format("u{0}a{1}", i, j), a_slot.GetChild(0).GetComponent<InventoryDraggedItem>().item);

                Transform s_slot = transform.FindChild("unit" + i.ToString()).FindChild("skill_slot" + j.ToString());
                if (s_slot.childCount > 0)
                    Inventory.ItemsSet.Add(string.Format("u{0}s{1}", i, j), a_slot.GetChild(0).GetComponent<InventoryDraggedItem>().item);
            }
        }
		Inventory.Instance.Initiate ();
        clearAll();
        LocalStorage.Save();
        Main.Instance.OpenMainMenu();
	}

    void Cancel()
    {
        clearAll();
        Main.Instance.OpenMainMenu();
    }

    public void Initiate()
    {
        Inventory.Instance.Initiate();
        clearAll();
        //GameObject wreckClone = (GameObject) Instantiate(Items[0], new Vector3(36.85f,-36.85f,0f), transform.rotation);
        //wreckClone.transform.SetParent(transform.FindChild("InventoryTable").FindChild("inventory_slot1").transform);
        //wreckClone.transform.localScale = new Vector3(1, 1, 1);
        //wreckClone.GetComponent<Image>().sprite = sprites[0];

        for (int i=0; i < Inventory.Items.Count; i++)
        {
            GameObject wreckClone = (GameObject)Instantiate(DefaultItem, new Vector3(36.85f, -36.85f, 0f), transform.rotation);
            wreckClone.transform.SetParent(transform.FindChild("InventoryTable").FindChild("inventory_slot"+i.ToString()).transform);
            wreckClone.transform.localScale = new Vector3(1, 1, 1);
            wreckClone.GetComponent<Image>().sprite = sprites[Inventory.Items[i].spriteName];
			wreckClone.GetComponent<InventoryDraggedItem>().item = Inventory.Items[i];
        }

        foreach(string s in Inventory.ItemsSet.Keys)
        {
            string unit = (s.Contains("u1")) ? "unit1" : (s.Contains("u2")) ? "unit2" : "unit3";
            string slot = (s.Contains("a1")) ? "armor_slot1" : (s.Contains("a2")) ? "armor_slot2" : (s.Contains("a3")) ? "armor_slot3" :
                (s.Contains("s1")) ? "skill_slot1" : (s.Contains("s2")) ? "skill_slot2" : "skill_slot3";

            GameObject wreckClone = (GameObject)Instantiate(DefaultItem, new Vector3(36.85f, -36.85f, 0f), transform.rotation);
            wreckClone.transform.SetParent(transform.FindChild(unit).FindChild(slot).transform);
            wreckClone.transform.localScale = new Vector3(1, 1, 1);
            wreckClone.GetComponent<Image>().sprite = sprites[Inventory.ItemsSet[s].spriteName];
            wreckClone.GetComponent<InventoryDraggedItem>().item = Inventory.ItemsSet[s];
        }

        FillStats();
    }

    public void clearAll()
    {
        Debug.Log("Clear All");
        for (int i = 0; i < transform.FindChild("InventoryTable").childCount; i++)
        {
            if (transform.FindChild("InventoryTable").GetChild(i).childCount > 0)
                Destroy(transform.FindChild("InventoryTable").GetChild(i).GetChild(0).gameObject);
        }
        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                Transform a_slot = transform.FindChild("unit" + i.ToString()).FindChild("armor_slot" + j.ToString());
                if (a_slot.childCount > 0)
                    Destroy(a_slot.GetChild(0).gameObject);

                Transform s_slot = transform.FindChild("unit" + i.ToString()).FindChild("skill_slot" + j.ToString());
                if (s_slot.childCount > 0)
					Destroy(s_slot.GetChild(0).gameObject);
            }
        }
    }

    public void FillStats()
    {
        for (int i = 1; i < 4; i++)
        {
            int atk = 0, def = 0, agi = 0;
            for (int j = 1; j < 4; j++)
            {
                Transform a_slot = transform.FindChild("unit" + i.ToString()).FindChild("armor_slot" + j.ToString());
                if (a_slot.childCount > 0)
                {
                    Item item = a_slot.GetChild(0).GetComponent<InventoryDraggedItem>().item;
                    switch (item.atribute)
                    {
                        case Item.Atribute.ATK:
                            atk += item.amount;
                            break;
                        case Item.Atribute.DEF:
                            def += item.amount;
                            break;
                        case Item.Atribute.AGI:
                            agi += item.amount;
                            break;
                    }
                }
            }
            transform.FindChild("unit" + i.ToString()).FindChild("ATK").GetComponent<Text>().text =
                string.Format("ATK: {0}(+{1})",LocalStorage.player[i-1].ATK,atk);
            transform.FindChild("unit" + i.ToString()).FindChild("DEF").GetComponent<Text>().text =
                string.Format("DEF: {0}(+{1})", LocalStorage.player[i - 1].DEF, def);
            transform.FindChild("unit" + i.ToString()).FindChild("AGI").GetComponent<Text>().text =
                string.Format("AGI: {0}(+{1})", LocalStorage.player[i - 1].AGI, agi);
        }
    }
}
