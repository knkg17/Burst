using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour {
	public Transform bulletLauncher;
	public GameObject bulletPrefab;
	public GunDirection gd;
	public Gauge gunGauge, chargeGauge;
	public float chargeTime, chargeMultiplier, chargeIncrement;
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
			_chargeSize = chargeIncrement;
		}


		if( Input.GetMouseButtonUp(0) && _shooting ) {
			_shooting = false;
			//Vector3 pos = gameObject.transform.position + new Vector3(0f, 2.5f, 2f);
			GameObject bullet = (GameObject)Instantiate( bulletPrefab, bulletLauncher.position, Quaternion.identity );
			Bullet b = bullet.GetComponent<Bullet>();
			float s = gunGauge.Subtract( _chargeSize * gunGauge.maxGaugeValue );
			if( s < _chargeSize * gunGauge.maxGaugeValue ) {
				_chargeSize = s / gunGauge.maxGaugeValue;
			}
			b.SetSize( _chargeSize * 1.5f );
			// gd.myTransform.eulerAngles.z
			b.SetVelocity( gd.GetXYAngle() * chargeMultiplier * _chargeSize * 3f );
			
			b.InitCountdown( Random.Range( 5f, 10.1f ) );
			gunGauge.Emptying();
		}
	}

	void LateUpdate ( ) {
		if( Input.GetMouseButton( 0 ) && _shooting ) {
			if( _chargeTimer > 0f ) {
				_chargeTimer -= Time.deltaTime;
			} else {
				_chargeTimer = chargeTime;
				if( _chargeSize < chargeIncrement * 5f ) {
					_chargeSize += chargeIncrement;
					float s = chargeGauge.Subtract( gunGauge.maxGaugeValue / 10f );
					if( s > 0 ) {
						gunGauge.Increase( s, chargeTime * 0.9f );
					}
				}
			}
		}

	}
}
