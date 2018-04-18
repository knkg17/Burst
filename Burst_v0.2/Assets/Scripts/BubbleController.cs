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
	public float maxVelocity, maxSize, size;
	public Transform myTransform;
	public Transform bubbleGraphic;
	public float bubbleTurnRate;
	private Vector3 bgEulerAngles;

	public float health;

	// Use this for initialization
	void Start () {
		bgEulerAngles = bubbleGraphic.localEulerAngles;
		if(myTransform == null ) {
			myTransform = gameObject.transform;
		}

		health = 100f;

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
			bubbleTurnRate = ( bubbleTurnRate + Random.Range( -4, 4 ) ) * -1f;
			health -= (float)( Random.Range( 5, 10 ) );
		}
		if( bdY == Bouncedirection.NORTH || bdY == Bouncedirection.SOUTH ) {
			temp.y = temp.y * -1f;
			velocity.y = velocity.y * -1f;
			bubbleTurnRate = ( bubbleTurnRate + Random.Range( -4, 4 ) ) * -1f;
			health -= (float)( Random.Range( 5, 10 ) );
		}
		/*
		if( bdX != Bouncedirection.NULL || bdY != Bouncedirection.NULL )
			Debug.Log( "Bounce! " + bdX.ToString() + " or " + bdY.ToString() );
		//*/
		myTransform.Translate( temp.x, temp.y, 0f );
		bgEulerAngles.z = ( bgEulerAngles.z + ( bubbleTurnRate * Time.deltaTime ) ) % 360;
		bubbleGraphic.localEulerAngles = bgEulerAngles;
		FixXPos();
		FixYPos();
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

	public void AddSpeed ( float s ) {
		velocity = velocity.normalized * ( velocity.magnitude + s );
		if( velocity.magnitude > maxVelocity ) {
			velocity = velocity.normalized * maxVelocity;
		}
	}

	public void IncreaseSize ( float x ) {
		float tempSize = x + myTransform.localScale.x;
		if( tempSize > maxSize ) {
			myTransform.localScale = new Vector3( maxSize, maxSize, 1f );
			size = maxSize;
		} else {
			myTransform.localScale = new Vector3( tempSize, tempSize, 1f );
			size = tempSize;
		}
	}

	private void FixXPos ( ) {
		float x = GetPos().x;
		if( x - ( size / 2f ) < 0f ) {
			myTransform.position = new Vector3( size / 2f, myTransform.position.y, myTransform.position.z );
			return;
		}
		if( x > gameBounds.width - ( size / 2f ) ) {
			myTransform.position = new Vector3( gameBounds.width - ( size / 2f ), myTransform.position.y, myTransform.position.z );
			return;
		}
	}

	private void FixYPos ( ) {
		float y = GetPos().x;
		if( y - ( size / 2f ) < 0f ) {
			myTransform.position = new Vector3( myTransform.position.x, size / 2f, myTransform.position.z );
			return;
		}
		if( y > gameBounds.height - ( size / 2f ) ) {
			myTransform.position = new Vector3( myTransform.position.x, gameBounds.height - ( size / 2f ), myTransform.position.z );
			return;
		}
	}
}
