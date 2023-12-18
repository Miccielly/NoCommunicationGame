using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem impactParticles;

    private bool destroyCalled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!destroyCalled)
        {
            Instantiate(impactParticles.gameObject, transform.position, Quaternion.identity);
            rb.isKinematic = true;
            destroyCalled = true;
            Invoke(nameof(DestroyThisObject), 1f);
        }
    }

    private void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
