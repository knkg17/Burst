using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bouncedirection {
	NORTH = 1,
	SOUTH = 2,
	WEST  = 3,
	EAST  = 4,
	NULL  = 0
}

public class BubbleController : MonoBehaviour {
	public Rect gameBounds;
	public Vector3 origo;

	public Vector2 velocity;
	public float maxVelocity, maxSize;
	public Transform myTransform;
	// Use this for initialization
	void Start () {
		if(myTransform == null ) {
			myTransform = gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate ( ) {
		Vector2 temp = velocity * Time.deltaTime;
		
		Bouncedirection bd = InGameBound( GetPos() + temp );
		switch( bd ) {
			case Bouncedirection.NORTH:
				temp.y = temp.y * -1f;
				velocity.y = velocity.y * -1;
				break;
			case Bouncedirection.SOUTH:
				temp.y = temp.y * -1f;
				velocity.y = velocity.y * -1;
				break;
			case Bouncedirection.WEST:
				temp.x = temp.x * -1f;
				velocity.x = velocity.x * -1;
				break;
			case Bouncedirection.EAST:
				temp.x = temp.x * -1f;
				velocity.x = velocity.x * -1;
				break;
			default:
				break;
		}
		if( bd != Bouncedirection.NULL )
			Debug.Log( "Bounce! " + bd.ToString() );
		myTransform.Translate( temp.x, temp.y, 0f );
	}

	private Bouncedirection InGameBound ( Vector2 newPos ) {
		if( newPos.x + ( myTransform.localScale.x / 2f ) > origo.x + gameBounds.width )
			return Bouncedirection.EAST;
		if( newPos.x - ( myTransform.localScale.x / 2f ) < origo.x + gameBounds.x )
			return Bouncedirection.WEST;
		if( newPos.y + ( myTransform.localScale.y / 2f ) > origo.y + gameBounds.height )
			return Bouncedirection.NORTH;
		if( newPos.y - ( myTransform.localScale.y / 2f ) < origo.y + gameBounds.y )
			return Bouncedirection.SOUTH;
		return Bouncedirection.NULL;
	}

	private Vector2 GetPos ( ) {
		return new Vector2( myTransform.position.x, myTransform.position.y );
	}

	public void AddSpeed ( Vector2 s ) {
		velocity += s;
		if( velocity.magnitude > maxVelocity ) {
			velocity = velocity.normalized * maxVelocity;
		}
	}

	public void IncreaseSize ( float x ) {
		float tempSize = x + myTransform.localScale.x;
		if( tempSize > maxSize ) {
			myTransform.localScale = new Vector3( maxSize, maxSize, 1f );
		} else {
			myTransform.localScale = new Vector3( tempSize, tempSize, 1f );
		}
	}
}
