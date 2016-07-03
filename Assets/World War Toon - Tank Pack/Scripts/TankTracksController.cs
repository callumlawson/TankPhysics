using UnityEngine;
using System.Collections;

public class TankTracksController : MonoBehaviour 
{
	public TrackController leftTrack;
	public TrackController rightTrack;
	public float leftSpeed;
	public bool  leftFreeWheel;
	public float rightSpeed;
	public bool  rightFreeWheel;
	public bool  showDebug;

	// Use this for initialization
	void Start () 
	{
		if (leftTrack != null)
		{
			leftTrack.Init();
		}

		if (rightTrack != null)
		{
			rightTrack.Init();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (leftTrack != null)
		{
			leftTrack.Update(leftSpeed, leftFreeWheel);
		}
		
		if (rightTrack != null)
		{
			rightTrack.Update(rightSpeed, rightFreeWheel);
		}
	}

	public void OnDrawGizmos()
	{
		if ( showDebug )
		{
			if ( leftTrack != null )
			{
				leftTrack.OnDrawGizmos();
			}

			if ( rightTrack != null )
			{
				rightTrack.OnDrawGizmos();
			}
		}
	}
}
