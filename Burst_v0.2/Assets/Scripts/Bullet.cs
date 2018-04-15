using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public Transform myTransform;
	public Vector2 velocity;
	public float size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void FixedUpdate ( ) {
		Vector2 newPos = velocity * Time.deltaTime;
		myTransform.Translate( newPos.x, newPos.y, 0f );
	}

	public void Init ( Vector2 vel, float s ) {
		velocity = vel;
		size = s;
	}
}
