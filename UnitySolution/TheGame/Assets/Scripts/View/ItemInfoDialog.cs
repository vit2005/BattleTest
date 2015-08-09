using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfoDialog : MonoBehaviour {

	static private ItemInfoDialog _instance;
	static public ItemInfoDialog Instance
	{
		get { return _instance; }
	}

	// Use this for initialization
	void Start () {
		_instance = this;
		transform.FindChild("cancel_btn").GetComponent<Button>().onClick.AddListener(Cancel);
	}

	void Cancel()
	{
		gameObject.SetActive (false);
	}
	
	public void Initiate()
	{
		gameObject.SetActive (true);
		Item i = InventoryDraggedItem.ItemInfo;
		transform.FindChild ("name").GetComponent<Text> ().text = string.Format ("{0} [{1}]",i.name,i.rarity.ToString());
		transform.FindChild ("stat").GetComponent<Text> ().text = string.Format ("+{0} {1}",i.amount,i.atribute.ToString());
	}
}
