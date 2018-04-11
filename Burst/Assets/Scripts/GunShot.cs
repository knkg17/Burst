using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour {
	public GameObject bulletPrefab;
	public GunDirection gd;
	public Gauge gunGauge, chargeGauge;
	public float chargeTime;
	// Use this for initialization
	void Start () {
		
	}

	private bool _shooting = false;
	private float _chargeTimer = 0f;
	private float _chargeSize = 0f;
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown( 0 ) && !_shooting ) {
			_shooting = true;
			_chargeTimer = chargeTime;
			_chargeSize = 0f;
		}


		if( Input.GetMouseButtonUp(0) && _shooting ) {
			_shooting = false;
			Vector3 pos = gameObject.transform.position;
			pos.z += 1f;
			pos.y += 2.5f;
			GameObject bullet = (GameObject)Instantiate( bulletPrefab, pos, Quaternion.identity );
			Bullet b = bullet.GetComponent<Bullet>();
			float s = gunGauge.Subtract( _chargeSize * gunGauge.maxGaugeValue );
			if( s < _chargeSize * gunGauge.maxGaugeValue ) {
				_chargeSize = s / gunGauge.maxGaugeValue;
			}
			b.SetSize( _chargeSize * 1.5f );
			// gd.myTransform.eulerAngles.z
			b.SetVelocity( gd.GetXYAngle() * 8f * _chargeSize );
			
			b.InitCountdown( Random.Range( 5f, 10.1f ) );
		}
	}

	void LateUpdate ( ) {
		if( Input.GetMouseButton( 0 ) && _shooting ) {
			if( _chargeTimer > 0f ) {
				_chargeTimer -= Time.deltaTime;
			} else {
				_chargeTimer = chargeTime;
				_chargeSize += 0.1f;
				if( _chargeSize > 0.5f )
					_chargeSize = 0.5f;
				float s = chargeGauge.Subtract( gunGauge.maxGaugeValue / 10f );
				if( s > 0 ) {
					gunGauge.Increase( s, chargeTime * 0.9f );
				}
			}
		}

	}
}
