using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Animator playeranimator;
    public float maxspeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playeranimator.SetFloat("Blend",rigidbody.velocity.magnitude / maxspeed);
    }
}
