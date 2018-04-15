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

		myTransform.position = new Vector3( Random.Range( gameBounds.x + origo.x + myTransform.localScale.x / 2f, gameBounds.width + origo.x - myTransform.localScale.x / 2f ),
											Random.Range( gameBounds.y + origo.y + myTransform.localScale.x / 2f, gameBounds.height + origo.y - myTransform.localScale.x / 2f ),
											0f );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate ( ) {
		Vector2 temp = velocity * Time.deltaTime;
		
		Bouncedirection bdX = InGameBoundX( GetPos() + temp );
		Bouncedirection bdY = InGameBoundY( GetPos() + temp );
		if( bdX == Bouncedirection.EAST || bdX == Bouncedirection.WEST ) {
			temp.x = temp.x * -1f;
			velocity.x = velocity.x * -1f;
		}
		if( bdY == Bouncedirection.NORTH || bdY == Bouncedirection.SOUTH ) {
			temp.y = temp.y * -1f;
			velocity.y = velocity.y * -1f;
		}
		/*
		if( bdX != Bouncedirection.NULL || bdY != Bouncedirection.NULL )
			Debug.Log( "Bounce! " + bdX.ToString() + " or " + bdY.ToString() );
		//*/
		myTransform.Translate( temp.x, temp.y, 0f );
	}

	private Bouncedirection InGameBoundX ( Vector2 newPos ) {
		if( newPos.x + ( myTransform.localScale.x / 2f ) > origo.x + gameBounds.width )
			return Bouncedirection.EAST;
		if( newPos.x - ( myTransform.localScale.x / 2f ) < origo.x + gameBounds.x )
			return Bouncedirection.WEST;
		return Bouncedirection.NULL;
	}
	private Bouncedirection InGameBoundY ( Vector2 newPos ) {
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
