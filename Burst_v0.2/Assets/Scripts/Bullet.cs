using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public Transform myTransform;
	public Vector2 velocity;
	public float size;
	private float lifeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( lifeTime > 0f ) {
			lifeTime -= Time.deltaTime;
		} else {
			Destroy( this );
		}
	}

	void LateUpdate ( ) {
		Vector2 newPos = velocity * Time.deltaTime;
		myTransform.Translate( newPos.x, newPos.y, 0f );
	}

	void FixedUpdate ( ) {
		//Vector2 newPos = velocity * Time.deltaTime;
		//myTransform.Translate( newPos.x, newPos.y, 0f );
	}

	public void Init ( Vector2 vel, float s ) {
		velocity = vel;
		size = s;
		myTransform.localScale = new Vector3( s, s, 1 );
		lifeTime = (float)Random.Range( 5, 10 );
		//Debug.Log( "Finished initializing bullet " + velocity + ", " + myTransform.localScale + ", " + lifeTime );
	}
}
