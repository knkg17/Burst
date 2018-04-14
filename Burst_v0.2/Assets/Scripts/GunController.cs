using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
	public Vector3 direction;
	public Transform myTransform;
	// Use this for initialization
	void Start ( ) {
		_main = Camera.main;
	}

	// Update is called once per frame
	void Update ( ) {
		SetAngle();
	}
	private Camera _main;
	private void SetAngle ( ) {
		Vector3 aim = _main.ScreenToWorldPoint( Input.mousePosition );
		float angle;
		Vector2 a = new Vector2( myTransform.position.x, myTransform.position.y );
		Vector2 b = new Vector2( aim.x, aim.y );

		angle = GetAngle( a, b );
		myTransform.eulerAngles = new Vector3( 0f, 0f, angle );
	}

	private float _ax, _ay;
	private float GetAngle ( Vector2 a, Vector2 b ) {
		_ax = b.x - a.x;
		_ay = b.y - a.y;

		float arctan = Mathf.Atan( _ax / _ay ) * Mathf.Rad2Deg;

		return arctan * -1f;
	}

	public Vector2 GetXYAngle ( ) {
		Vector2 returnVal = new Vector2( _ax, _ay );
		return returnVal.normalized;
	}
}
