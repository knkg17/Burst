using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
	public Transform graphic, myTransform;
	public Rigidbody2D myBody;
	public Vector3 rotation;
	public float velocity, maxVelocity, delta;
	public float direction, size, maxSize;
	public float health;

	private Vector2 _dv, _velocity;
	// Use this for initialization
	void Start () {
		rotation = new Vector3( 0f, 0f, 0f );
		direction = Random.Range( 40f, 50f );
		//direction = 45f;

		_velocity = new Vector2( Mathf.Cos( Mathf.Deg2Rad * direction ) * velocity, Mathf.Sin( Mathf.Deg2Rad * direction ) * velocity );
		_dv = new Vector2( Mathf.Cos( Mathf.Deg2Rad * direction ) * delta, Mathf.Sin( Mathf.Deg2Rad * direction ) * delta );
		health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		myBody.velocity = Vector2.zero;
		rotation.z += Random.Range( -0.1f, 1.1f );
		graphic.eulerAngles = rotation;

		_velocity += _dv;
	}

	void LateUpdate ( ) {
		if( _velocity.magnitude > maxVelocity ) {
			_velocity = _velocity.normalized * maxVelocity;
		}
		myTransform.Translate( _velocity.x, _velocity.y, 0f );
		//myBody.velocity = Vector2.zero;
	}

	private float _vertCollTimer = 0f;
	private float _horzCollTimer = 0f;

	void OnCollisionEnter2D ( Collision2D collision ) {
		if( collision.gameObject.tag == "FrameTop" || collision.gameObject.tag == "FrameBottom" ) {
			if( _vertCollTimer == 0f ) {
				_velocity.y *= -1f;
				_dv.y *= -1f;
				health -= Random.Range( 0.5f, 2.51f );
				CalculateDirection();
				//_vertCollTimer += Time.deltaTime;
				Debug.Log( "Switching v-direction! - " + _dv.y );
			} else if( _vertCollTimer > 0.1f ) {
				_vertCollTimer = 0f;
			} else {
				_vertCollTimer += Time.deltaTime;
			}

		}
		if( collision.gameObject.tag == "FrameLeft" || collision.gameObject.tag == "FrameRight" ) {
			if( _horzCollTimer == 0f ) {
				_velocity.x *= -1f;
				_dv.x *= -1f;
				health -= Random.Range( 0.5f, 2.51f );
				CalculateDirection();
				//_horzCollTimer += Time.deltaTime;
				Debug.Log( "Switching h-direction! - " + _dv.x );
			} else if( _vertCollTimer > 0.1f ) {
				_horzCollTimer = 0f;
			} else {
				_horzCollTimer += Time.deltaTime;
			}
		}

		if( collision.gameObject.tag == "Bullet" ) {
			//Bullet bb = collision.gameObject.GetComponent<Bullet>();
			float multi = ( collision.gameObject.transform.localScale.x / 10f ) + 1f;
			if( myTransform.localScale.x < 5f )
				myTransform.localScale = myTransform.localScale * multi;
			health += Random.Range( multi/4f*3f, multi * 1.51f );
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
