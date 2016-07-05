using UnityEngine;

public class SuspensionPoint : MonoBehaviour
{
    public float MaxLengthMeters = 0.7f;
    public Rigidbody OurRigidbody;
    public float SuspensionForce = 100;

    public GameObject Track;
    private Vector3 InitialTrackPosition;

    private float SuspensionLengthMeters;

    // Use this for initialization
    private void Start()
    {
        if (Track)
        {
            InitialTrackPosition = Track.GetComponent<Transform>().localPosition;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Track)
        {
            UpdateTrackPosition(SuspensionLengthMeters/2 - 0.2f); //LOL
        }
    }

    private void UpdateTrackPosition(float offset)
    {
        Track.transform.localPosition = InitialTrackPosition + Vector3.down*offset;
    }

    private void FixedUpdate()
    {
        var down = transform.rotation*Vector3.forward;
        var ray = new Ray(transform.position, down);

        RaycastHit raycastHit;
        var didHit = Physics.Raycast(ray, out raycastHit, MaxLengthMeters);

        if (didHit)
        {
            var suspensionVector = raycastHit.point - transform.position;
            SuspensionLengthMeters = suspensionVector.magnitude;
            var suspensionForce = SuspensionForce*(1 - SuspensionLengthMeters / MaxLengthMeters);
//            Debug.Log(suspensionForce);
            OurRigidbody.AddForceAtPosition(Vector3.up * suspensionForce, transform.position, ForceMode.Acceleration);
            Debug.DrawRay(
                transform.position,
                suspensionVector,
                Color.green
            );
        }
        else
        {
            SuspensionLengthMeters = MaxLengthMeters;

            Debug.DrawRay(
               transform.position,
               down * MaxLengthMeters,
               Color.red
           );
        }
    }
}