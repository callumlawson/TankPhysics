using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody OurRigidbody;
    public float ForceMagnitude = 100;
    public float DragFactor = 10;
    public float TorqueFactor = 8;

    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector3 engineForce;
    [SerializeField] private float lateralMovementFactor;
    private Vector3 forwardDirectionOnPlane;
    private Vector3 forceApplicationPosition;

    private void Start()
    {
        OurRigidbody.centerOfMass += Vector3.down;
    }

    private void FixedUpdate()
    {
        CastGroundRay();
        ApplyEngineForce();
        ApplyDrag();
    }

    private void ApplyDrag()
    {
        if (isGrounded)
        {
            lateralMovementFactor = 1 -
                                    Mathf.Abs(Quaternion.Dot(transform.rotation,
                                        Quaternion.LookRotation(OurRigidbody.velocity)));
            var dragForce = -1*OurRigidbody.velocity*lateralMovementFactor;
            OurRigidbody.AddForce(dragForce*DragFactor, ForceMode.Acceleration);
        }
    }

    private void ApplyEngineForce()
    {
        forceApplicationPosition = transform.position + (transform.rotation*new Vector3(0, 0.05f, 0.05f));

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            engineForce = forwardDirectionOnPlane*ForceMagnitude;
            OurRigidbody.AddForceAtPosition(engineForce, transform.position, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            engineForce = -forwardDirectionOnPlane*ForceMagnitude;
            OurRigidbody.AddForceAtPosition(engineForce, transform.position, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            OurRigidbody.AddRelativeTorque(new Vector3(0, TorqueFactor, 0), ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            OurRigidbody.AddRelativeTorque(new Vector3(0, -TorqueFactor, 0), ForceMode.Acceleration);
        }
    }

    private void CastGroundRay()
    {
        var down = Vector3.down;
        var ray = new Ray(transform.position, down);
        RaycastHit raycastHit;
        isGrounded = Physics.Raycast(ray, out raycastHit, 3);
        forwardDirectionOnPlane = Math3d.ProjectVectorOnPlane(raycastHit.normal, transform.forward).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(OurRigidbody.worldCenterOfMass, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(forceApplicationPosition, 0.1f);
    }
}