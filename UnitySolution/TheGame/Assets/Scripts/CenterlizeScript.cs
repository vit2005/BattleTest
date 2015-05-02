using UnityEngine;
using System.Collections;

public class CenterlizeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3((float)598, (float)(336.5), 0);
		//gameObject.transform.position = new Vector3(0, 0, 0);
	}
}
