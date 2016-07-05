using UnityEngine;
using System.Collections;

public class TrackContactBehaviour : MonoBehaviour
{
    public Transform ContactPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        RaycastHit raycastHit;
        var didHit = Physics.Raycast(new Ray(ContactPoint.transform.position, Vector3.down), out raycastHit, 1f);

        if (didHit)
        {
//            var hitPoint = raycastHit.point;
//            var normal = raycastHit.normal;

            var vectorToGround = (raycastHit.point - ContactPoint.transform.position);

            transform.position = transform.position + vectorToGround - (vectorToGround*0.1f);
//
//            var forwardOnPlane = Math3d.ProjectVectorOnPlane(raycastHit.normal, transform.forward);
//
//            Debug.DrawRay(
//                raycastHit.point,
//                forwardOnPlane,
//                Color.red
//                );
        }
        else
        {
            
        }

    }
}
