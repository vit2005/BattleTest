using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mapScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.FindChild("cancel_btn").GetComponent<Button>().onClick.AddListener(onCancel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onCancel()
    {
		Main.Instance.OpenMainMenu();
    }

}
