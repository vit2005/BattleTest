using UnityEngine;
using System.Collections.Generic;

public class ItemsDatabase : MonoBehaviour {

	static private ItemsDatabase _instance;
	static public ItemsDatabase Instance
	{
		get { return _instance; }
	}

	public List<Item> ItemsList = new List<Item>();

	void Start () {
		_instance = this;
	}
}
