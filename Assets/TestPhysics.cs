using UnityEngine;
using System.Collections;

public class TestPhysics : MonoBehaviour
{

    public float ForceMagnitude = 10;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(GetComponent<Rigidbody>().worldCenterOfMass, transform.forward * ForceMagnitude);
    }
}
