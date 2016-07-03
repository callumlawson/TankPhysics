using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    public Rigidbody OurRigidbody;
    public float ForceMagnitude = 100;

    // Use this for initialization
    void Start()
    {
        OurRigidbody.centerOfMass += Vector3.down;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         

        var down = Vector3.down;
        var ray = new Ray(transform.position, down);
        RaycastHit raycastHit;
        var didHit = Physics.Raycast(ray, out raycastHit, 3);

        if (didHit)
        {
            var forwardOnPlane = Math3d.ProjectVectorOnPlane(raycastHit.normal, transform.forward);

            Debug.DrawRay(
                raycastHit.point,
                forwardOnPlane,
                Color.red
                );

//            Debug.DrawRay(
//                raycastHit.point,
//                transform.forward,
//                Color.green
//                );
//
//            Debug.DrawRay(
//                raycastHit.point,
//                raycastHit.normal,
//                Color.blue
//                );
        }

//        OurRigidbody.AddForceAtPosition(transform.forward * ForceMagnitude, OurRigidbody.worldCenterOfMass);

        if (Input.GetKey(KeyCode.W) && didHit)
        {
            var forwardOnPlane = Math3d.ProjectVectorOnPlane(raycastHit.normal, transform.forward).normalized;

            Debug.DrawRay(
             OurRigidbody.worldCenterOfMass,
             transform.forward * 10,
             Color.blue
             );

            OurRigidbody.AddForceAtPosition(forwardOnPlane * ForceMagnitude, OurRigidbody.worldCenterOfMass + (Vector3.forward + Vector3.up) * 0.1f, ForceMode.Acceleration);

//            OurRigidbody.AddForce(forwardOnPlane * ForceMagnitude, ForceMode.Acceleration);
        }

        if(Input.GetKey(KeyCode.A))
        {
            OurRigidbody.AddRelativeTorque(new Vector3(0, 10 ,0), ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.D))
        {
            OurRigidbody.AddRelativeTorque(new Vector3(0, -10, 0), ForceMode.Acceleration);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(OurRigidbody.worldCenterOfMass, 0.1f);
    }
}