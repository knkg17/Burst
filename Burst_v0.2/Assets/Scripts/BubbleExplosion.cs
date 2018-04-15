using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleExplosion : MonoBehaviour {
	private List<GameObject> _bubbles;
	private List<Vector2> _vectors;
	public GameObject bubblePrefab;
	// Update is called once per frame
	void Update () {
		
	}

	private float _power, _duration;
	private bool _explode = false;
	public void InstantiateExplosion ( float power, float duration ) {
		_explode = true;
		int nobs = Random.Range( 10, 21 );
		if( _bubbles == null ) {
			_bubbles = new List<GameObject>();
			_vectors = new List<Vector2>();
		} else {
			for(int i = 0; i < _bubbles.Count;i++ ) {
				Destroy( _bubbles[ i ] );
			}
			_bubbles.Clear();
			_vectors.Clear();
		}

		for( int i = 0; i < nobs; i++ ) {
			GameObject go = Instantiate( bubblePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform );
			go.transform.eulerAngles = new Vector3( 0f, 180f, 0f );
			float s = Random.Range( 2, 6 ) / 10f;
			go.transform.localScale = new Vector3( s, s, 1f );
			_bubbles.Add( go );
			_vectors.Add( new Vector2( Random.Range( -5, 6 ), Random.Range( -5, 6 ) ) );
		}
	}
}
