using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{
    public Rigidbody OurRigidbody;
    public float ForceMagnitude;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        OurRigidbody.AddForce(Vector3.down * ForceMagnitude, ForceMode.Acceleration);    
    }
}
