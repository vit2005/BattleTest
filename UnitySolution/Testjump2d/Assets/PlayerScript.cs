using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private Rigidbody2D rb2d;
	public bool grounded = true;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	void Update()
	{
		if (Input.GetButtonDown ("Jump") && grounded)
			rb2d.AddForce (Vector2.up * 250);

	}

	void FixedUpdate () {

		//if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) 
		//	rb2d.velocity = new Vector2(rb2d.velocity.x,3);

		//if (Input.GetKeyDown(KeyCode.D)) 
		//	rb2d.velocity = new Vector2(3,rb2d.velocity.y);

		//if (Input.GetKeyDown(KeyCode.A)) 
		//	rb2d.velocity = new Vector2(-3,rb2d.velocity.y);

		//if (rb2d.position.y < -3)
		//	rb2d.position = new Vector2 (-6.31f, -0.3f);

		float h = Input.GetAxis ("Horizontal");
		rb2d.AddForce (Vector2.right * h * 30);

		if (rb2d.velocity.x > 3)
			rb2d.velocity = new Vector2 (3, rb2d.velocity.y);
		if (rb2d.velocity.x < -3)
			rb2d.velocity = new Vector2 (-3, rb2d.velocity.y);
	}
}
