using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDirection : MonoBehaviour {
	public Vector3 direction;
	public Transform myTransform;
	// Use this for initialization
	void Start () {
		_main = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		SetAngle();
	}
	private Camera _main;
	private void SetAngle ( ) {
		Vector3 aim = _main.ScreenToWorldPoint( Input.mousePosition );
		float angle;
		Vector2 a = new Vector2( myTransform.position.x, myTransform.position.y );
		Vector2 b = new Vector2( aim.x, aim.y );
		//Debug.Log( a.ToString() + ", " + b.ToString() );
		float abangle = Vector2.Angle( a, b );
		float baangle = Vector2.Angle( b, a );
		float origoangle = Vector2.Angle( new Vector2(0f,0f), b );

		//Debug.Log( "angles, ab: " + abangle.ToString( "00.00" + ", ba: " + baangle.ToString( "00.00" ) + ",  oa: " + origoangle.ToString( "00.00" ) ));
		//angle = Vector2.Angle( ConvertV3( myTransform.position ), ConvertV3( aim ) );

		angle = GetAngle( a, b );
		/*
		if( angle < 90f )
			angle = 90f - angle;
		else if( angle > 90f )
			angle = angle - 90f;
		else
			angle = 0f;
		//*/
		//Debug.Log( aim.ToString() + ", " + myTransform.position.ToString() + " - " + angle.ToString("0000.0000") );
		myTransform.eulerAngles = new Vector3( 0f, 0f, angle );
	}

	private float GetAngle(Vector2 a, Vector2 b ) {
		float x = b.x - a.x;
		float y = b.y - a.y;

		float arctan = Mathf.Atan( x / y ) * Mathf.Rad2Deg;
		//Debug.Log( arctan );
		return arctan * -1f;
	}

	private Vector2 ConvertV3( Vector3 v ) {
		return new Vector2( v.x, v.y );
	}
}
