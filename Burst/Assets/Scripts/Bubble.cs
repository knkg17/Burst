using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
	public Transform graphic, myTransform;
	//public Rigidbody2D myBody;
	public Vector3 rotation;
	public float vx, vy, velocity, maxVelocity, delta;
	public float direction;

	private float _dx, _dy;
	// Use this for initialization
	void Start () {
		rotation = new Vector3( 0f, 0f, 0f );
		//direction = Random.Range( 40f, 50f );
		direction = 45f;
		vx = Mathf.Cos( Mathf.Deg2Rad * direction ) * velocity;
		vy = Mathf.Sin( Mathf.Deg2Rad * direction ) * velocity;
		_dx = Mathf.Cos( Mathf.Deg2Rad * direction ) * delta;
		_dy = Mathf.Sin( Mathf.Deg2Rad * direction ) * delta;
		//myBody.velocity = new Vector2( vx, vy );
		//myBody.AddForce( new Vector2( vx, vy ), ForceMode2D.Impulse );

	}
	
	// Update is called once per frame
	void Update () {
		rotation.z += Random.Range( -0.1f, 1.1f );
		graphic.eulerAngles = rotation;

		velocity = new Vector2(vx,vy).magnitude;

		//vx = Mathf.Cos( Mathf.Deg2Rad * direction ) * velocity;
		//vy = Mathf.Sin( Mathf.Deg2Rad * direction ) * velocity;
		vx += _dx;
		vy += _dy;
	}

	void LateUpdate ( ) {
		Vector2 newVelocity = new Vector2( vx, vy );
		if( newVelocity.magnitude > maxVelocity ) {
			newVelocity = newVelocity.normalized * maxVelocity;
			vx = newVelocity.x;
			vy = newVelocity.y;
		}
		myTransform.Translate( vx, vy, 0f );
		//Debug.Log( myTransform.position.ToString() );
	}

	void OnCollisionEnter2D ( Collision2D collision ) {
		//Debug.Log( "Collided!" );
		if( collision.gameObject.tag == "FrameTop" || collision.gameObject.tag == "FrameBottom" ) {
			vy *= -1f;
			_dy *= -1f;
			CalculateDirection();

		}
		if( collision.gameObject.tag == "FrameLeft" || collision.gameObject.tag == "FrameRight" ) {
			vx *= -1f;
			_dx *= -1f;
			CalculateDirection();
		}
		//Debug.Log( collision.gameObject.tag + ", vx: " + vx.ToString("0.0000") + ", vy: " + vy.ToString("0.0000") );
	}

	private void CalculateDirection ( ) {
		/*
		float atan = Mathf.Atan( vx / vy );
		direction = Mathf.Rad2Deg * atan;
		//*/
		direction = Vector2.Angle( Vector2.zero, new Vector2( vx, vy ) );
	}
}
