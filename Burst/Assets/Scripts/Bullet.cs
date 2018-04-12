using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public Rigidbody2D myBody;
	public Transform myTransform;
	public Transform graphic;
	// Use this for initialization
	void Start () {
		float angle = Random.Range( 0f, 360f );
		graphic.eulerAngles = new Vector3( 0f, 0f, angle );
	}
	
	// Update is called once per frame
	void Update () {
		if( _cntr ) {
			if( _timer > 0f ) {
				_timer -= Time.deltaTime;
			} else {
				_cntr = false;
				Destroy( gameObject );
			}
		}
	}

	public void SetSize ( float scale ) {
		myTransform.localScale = new Vector3( scale, scale, 0f );
	}

	public void SetVelocity ( Vector2 v ) {

		myBody.velocity = v;
	}

	private bool _cntr = false;
	private float _timer = 0f;
	public void InitCountdown ( float val ) {
		_cntr = true;
		_timer = val;
	}

	void OnCollisionEnter2D ( Collision2D collision ) {
		if(collision.gameObject.tag == "Bubble" ) {
			Destroy( gameObject );
		}
	}
}
