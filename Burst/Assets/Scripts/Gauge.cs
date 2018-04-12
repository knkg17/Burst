using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour {
	public Transform load;
	public float width, height, maxWidth;
	public float gaugeValue, maxGaugeValue;
	// Use this for initialization
	void Start () {
		width = 0f;
		height = 0.8f;
		maxWidth = 0.98f;
		load.localScale = new Vector3( width, height, 1f );
		//gaugeValue = 0f;
		//maxGaugeValue = 100f;
	}

	private float _charge = 1f;
	// Update is called once per frame
	void Update () {
		if( _addWidth ) {
			float add = Time.deltaTime / _chargeTime * _amount;
			//Debug.Log( add.ToString( "0.00" ) );
			_steps -= add;
			gaugeValue += add;

			UpdateWidth();
			if( _steps <= 0f ) {
				_addWidth = false;
				_chargeTime = 0f;
			}
		}
	}

	private float _chargeAmount = 0f;
	private float _chargeTime = 0f;
	private float _previousGaugeValue;
	private bool _addWidth = false;
	private float _steps = 0f;
	private float _amount;

	public void Increase ( float val, float time ) {
		if(gaugeValue + val <= maxGaugeValue ) {
			_chargeTime = time;
			_addWidth = true;
			_steps = val;
			_amount = val;
		}
	}

	public float Subtract ( float val ) {
		//Debug.Log( "Subtracting<" + gameObject.name + ">..." );
		if( gaugeValue > 0 ) {
			if( gaugeValue - val > 0 ) {
				gaugeValue -= val;
				UpdateWidth();
				return val;
			}
			float v = gaugeValue;
			gaugeValue = 0f;
			UpdateWidth();
			return v;
		}
		return -1;
	}

	private void UpdateWidth ( ) {
		float newWidth = gaugeValue / maxGaugeValue;
		if( gaugeValue >= maxGaugeValue )
			newWidth = 1f;
		width = maxWidth * newWidth;
		load.localScale = new Vector3( width, height, 1f );
	}

	public float Emptying ( ) {
		float v = gaugeValue;
		gaugeValue = 0f;
		UpdateWidth();
		return v;
	}
}
