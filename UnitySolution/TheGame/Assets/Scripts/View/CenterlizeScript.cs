using UnityEngine;
using System.Collections;

public class CenterlizeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.transform.position = new Vector3(gameObject.transform.parent.position.x, gameObject.transform.parent.position.y, 0);
		//gameObject.transform.position = new Vector3(0, 0, 0);
	}
}
