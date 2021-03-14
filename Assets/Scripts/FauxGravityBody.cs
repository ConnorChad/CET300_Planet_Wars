using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAttractor attractor;
    private Transform myTransform;
    public Rigidbody rigidbody;

    private void Awake()
    {
        attractor = GameObject.FindGameObjectWithTag("Moon1").GetComponent<FauxGravityAttractor>();
    }
    private void Start()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
        myTransform = transform;
    }
    private void Update()
    {
        attractor.Attract(myTransform);
    }
}
