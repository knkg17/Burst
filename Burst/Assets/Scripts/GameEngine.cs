using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {
	public Gauge gauge, gunGauge;
	// Use this for initialization
	void Start () {
		
	}

	private float _timer1 = 1f;
	// Update is called once per frame
	void Update () {
		if( _timer1 > 0 ) {
			_timer1 -= Time.deltaTime;
		} else {
			_timer1 = 1f;
			gauge.Increase( 10f, 1f );
		}
	}

	public void Decrease ( ) {
		gauge.Subtract( 5f );
	}
}
