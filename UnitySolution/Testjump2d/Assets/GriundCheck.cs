using UnityEngine;
using System.Collections;

public class GriundCheck : MonoBehaviour {

	private PlayerScript ps;

	public void Start()
	{
		ps = gameObject.GetComponentInParent<PlayerScript>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		ps.grounded = true;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		ps.grounded = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		ps.grounded = false;
	}
}
