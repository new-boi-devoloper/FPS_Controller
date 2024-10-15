using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [field: Header("Player Stats")]
    [field:SerializeField] public float PlayerSpeed { get; private set; }
    
    [field: Header("Ground Section")]
    [field: SerializeField] private float groundedOffset;
    [field: SerializeField] private float groundedRadius;
    [field: SerializeField] private LayerMask groundLayer;

    //Dependencies
    public Rigidbody PlayerRb { get; private set; }

    public bool Grounded { get; set; }

    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundedCheck();
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    //For debug; To check whether staying player object on ground or not
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x,
                transform.position.y - groundedOffset,
                transform.position.z),
                groundedRadius);
    }
}