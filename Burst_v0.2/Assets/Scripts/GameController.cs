using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public BubbleController bc;
	public GameObject bulletPrefab;
	public Transform bulletSpawner;
	public List<Bullet> bullets;

	public Gauge ammoGauge, chargeGauge;
	public float ammoRechargeTimer, chargeTimer;
	public float ammoRechargeRate, chargeRate;
	public float ammoRechargeAmount;

	// Use this for initialization
	void Start () {
		_ammoGaugeTimer = ammoRechargeTimer;
	}

	private float _ammoGaugeTimer;
	// Update is called once per frame
	void Update () {
		if( _ammoGaugeTimer > 0f ) {
			_ammoGaugeTimer -= Time.deltaTime;
		} else {
			_ammoGaugeTimer = ammoRechargeTimer;
			ammoGauge.AddAmount( ammoRechargeAmount, ammoRechargeRate );
			//Debug.Log( Time.timeSinceLevelLoad );
		}
	}

}
