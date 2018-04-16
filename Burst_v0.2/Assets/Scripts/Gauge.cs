using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour {
	public Vector2 loadingSize;
	public float gaugeLevel, maxGauge, fillRate;
	public bool autoLoad;
	public Transform loading;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( autoLoad ) {
			UpdateLoading();
		} else if( _fillGauge ) {
			UpdateWithAddedAmount();
		}
	}


	private void FixedUpdate ( ) {
	}

	private float _newGaugeLevel;
	private float _newFillrate;
	private bool _fillGauge = false;
	public void AddAmount ( float amount, float fr ) {
		if( gaugeLevel >= maxGauge )
			return;

		_newGaugeLevel = gaugeLevel + amount;
		if( _newGaugeLevel > maxGauge )
			_newGaugeLevel = maxGauge;
		else if( _newGaugeLevel < 0 ) {
			_newGaugeLevel = 0;
			if( fr >= 0 ) {
				Debug.LogError( "Fillrate does not match delta, should be negative when new level is lower than current level." );
				return;
			}
		} else if( _newGaugeLevel < gaugeLevel ) {

			if( fr >= 0 ) {
				Debug.LogError( "Fillrate does not match delta, should be negative when new level is lower than current level." );
				return;
			}
		}

		_newFillrate = fr;
		_fillGauge = true;
	}

	public float GetAmount ( float amount ) {
		float returnAmount = 0f;
		if( gaugeLevel >= amount ) {
			returnAmount = amount;
			gaugeLevel -= amount;
		} else {
			returnAmount = gaugeLevel;
			gaugeLevel = 0f;
		}

		DrawGauge();

		return returnAmount;
	}

	private void UpdateWithAddedAmount ( ) {
		if( _newFillrate > 0f ) {
			float fillAdd = Time.deltaTime * _newFillrate;
			if( gaugeLevel + fillAdd <= _newGaugeLevel ) {
				gaugeLevel += fillAdd;
			} else if( gaugeLevel < _newGaugeLevel ) {
				gaugeLevel = _newGaugeLevel;
			} else {
				_fillGauge = false;
				return;
			}
		} else if( _newFillrate < 0f ) {
			float fillAdd = Time.deltaTime * _newFillrate;
			if( gaugeLevel - fillAdd >= _newGaugeLevel ) {
				gaugeLevel += fillAdd;
			} else if( gaugeLevel > _newGaugeLevel ) {
				gaugeLevel = _newGaugeLevel;
			} else {
				_fillGauge = false;
				return;
			}
		} else {
			return;
		}
		DrawGauge();
	}


	private void UpdateLoading ( ) {
		float fillAdd = Time.deltaTime * fillRate;
		if( gaugeLevel + fillAdd <= maxGauge ) {
			gaugeLevel += fillAdd;
		} else if( gaugeLevel < maxGauge ) {
			gaugeLevel = maxGauge;
		} else {
			return;
		}
		DrawGauge();
	}

	private void DrawGauge ( ) {
		Vector3 gaugeGraphic = new Vector3( loadingSize.x * GetGaugePercent(), loadingSize.y, 1f );
		loading.localScale = gaugeGraphic;
	}

	public float GetGaugePercent ( ) {
		return gaugeLevel / maxGauge;
	}

	public bool IsFull ( ) {
		if( gaugeLevel >= maxGauge )
			return true;
		return false;
	}

	public void Empty ( ) {
		gaugeLevel = 0;
		DrawGauge();
	}
}
