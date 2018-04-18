using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
	public BubbleController bc;
	public GameObject bulletPrefab;
	public Transform bulletSpawner;
	public List<Bullet> bullets;

	public Gauge ammoGauge, chargeGauge;
	public float ammoRechargeTimer, chargeTimer;
	public float ammoRechargeRate, chargeRate;
	public float ammoRechargeAmount;

	public Transform gun;
	public float baseBulletSpeed, baseBulletSize;
	public GunController gunc;

	public TextMeshProUGUI score, health;

	// Use this for initialization
	void Start () {
		_ammoGaugeTimer = ammoRechargeTimer;
		score.text = "SCORE: 0";
		health.text = "BUBBLE HEALTH: 100";
	}

	// Update is called once per frame
	void Update () {
		FillAmmoGauge();
		Charge();
		health.text = "BUBBLE HEALTH: " + bc.health;
	}

	private float _bubbleSpeedTimer = 0f;

	private void BubbleSpeed ( ) {
		if( _bubbleSpeedTimer > 1f ) {

		}
	}

	private bool _charging = false;
	//private bool _charging = true;
	private float _chargeTimer = 0f;
	private void Charge ( ) {
		if( Input.GetMouseButton( 0 ) && _charging ) {
			if( _chargeTimer < chargeTimer ) {
				_chargeTimer += Time.deltaTime;
			} else {
				_chargeTimer = 0f;
				if( chargeGauge.IsFull() == false ) {
					float t = ammoGauge.GetAmount( 10f );
					chargeGauge.AddAmount( t, chargeRate );
				}
			}
		}
		//*
		if( Input.GetMouseButtonDown( 0 ) ) {
			_charging = true;
			_chargeTimer = chargeTimer;
		}
		if( Input.GetMouseButtonUp( 0 ) && _charging && _shootTimer == 0f ) {
			_charging = false;
			Shoot();
		}//*/
		if( _shootTimer > 0f ) {
			if( _shootTimer > 0.1f ) {
				_shootTimer = 0f;
			} else {
				_shootTimer += Time.deltaTime;
			}
		}
	}

	private void LateUpdate ( ) {
		CheckCollision();
	}

	private void CheckCollision ( ) {
		for( int i = 0; i < bullets.Count; i++ ) {
			float collDist = ( bullets[ i ].size + bc.size ) / 2;
			Vector2 collVector = TransformToV2( bullets[ i ].myTransform.position ) - TransformToV2( bc.myTransform.position );
			Debug.Log( collVector.magnitude + ", " + collDist );
			if( collDist >= collVector.magnitude ) {
				Bullet b = bullets[ i ];
				bullets.Remove( b );
				bc.IncreaseSize( GetActualIncrease( bc.size, b.size ) );
				bc.AddSpeed( b.velocity.magnitude / 10f );
				Destroy( b.gameObject );
			}
 		}
	}

	private Vector2 TransformToV2 ( Vector3 v ) {
		return new Vector2( v.x, v.y );
	}

	private float GetActualIncrease ( float a, float b ) {
		float area = ( a * a * 3.14f / 4 ) + b;
		float dia = Mathf.Sqrt( area / 3.14f );
		Debug.Log( dia );
		return dia/10f;
	}

	private float _shootTimer = 0f;
	private void Shoot ( ) {
		if( _shootTimer == 0f )	{
			//Debug.Log( "Shooting!" );
			float v = chargeGauge.GetGaugePercent() + 0.5f;
			GameObject go = Instantiate( bulletPrefab, bulletSpawner.position, Quaternion.identity );
			Bullet b = go.GetComponent<Bullet>();
			Vector2 bv = gunc.GetXYAngle();
			//Debug.Log( "bullet vector: " + bv.ToString() + ", " + v );
			bv = bv.normalized * ( baseBulletSpeed * v * 2f );
			b.Init( bv, v );
			bullets.Add( b );
			chargeGauge.Empty();
			_shootTimer += Time.deltaTime;
		}
	}



	private float _ammoGaugeTimer;
	private void FillAmmoGauge ( ) {
		if( _ammoGaugeTimer > 0f ) {
			_ammoGaugeTimer -= Time.deltaTime;
		} else {
			_ammoGaugeTimer = ammoRechargeTimer;
			ammoGauge.AddAmount( ammoRechargeAmount, ammoRechargeRate );
			//Debug.Log( Time.timeSinceLevelLoad );
		}
	}

}
