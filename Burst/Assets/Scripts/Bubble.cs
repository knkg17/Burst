using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
	public Transform graphic, myTransform;
	//public Rigidbody2D myBody;
	public Vector3 rotation;
	public float velocity, maxVelocity, delta;
	public float direction, size, maxSize;

	private Vector2 _dv, _velocity;
	// Use this for initialization
	void Start () {
		rotation = new Vector3( 0f, 0f, 0f );
		direction = Random.Range( 40f, 50f );
		//direction = 45f;

		_velocity = new Vector2( Mathf.Cos( Mathf.Deg2Rad * direction ) * velocity, Mathf.Sin( Mathf.Deg2Rad * direction ) * velocity );
		_dv = new Vector2( Mathf.Cos( Mathf.Deg2Rad * direction ) * delta, Mathf.Sin( Mathf.Deg2Rad * direction ) * delta );

	}
	
	// Update is called once per frame
	void Update () {
		rotation.z += Random.Range( -0.1f, 1.1f );
		graphic.eulerAngles = rotation;

		_velocity += _dv;
	}

	void LateUpdate ( ) {
		if( _velocity.magnitude > maxVelocity ) {
			_velocity = _velocity.normalized * maxVelocity;
		}
		myTransform.Translate( _velocity.x, _velocity.y, 0f );
	}

	void OnCollisionEnter2D ( Collision2D collision ) {
		if( collision.gameObject.tag == "FrameTop" || collision.gameObject.tag == "FrameBottom" ) {
			_velocity.y *= -1f;
			_dv.y *= -1f;
			CalculateDirection();

		}
		if( collision.gameObject.tag == "FrameLeft" || collision.gameObject.tag == "FrameRight" ) {
			_velocity.x *= -1f;
			_dv.x *= -1f;
			CalculateDirection();
		}
	}

	private void CalculateDirection ( ) {
		direction = Vector2.Angle( Vector2.zero, _velocity );
	}

	public void Enlarge ( float val ) {
		float speedRedux = size / ( size + val );
		size += val;
		if( size > maxSize ) {
			size = maxSize;
		}
		myTransform.localScale = new Vector3( size, size, 1f );

	}
}
