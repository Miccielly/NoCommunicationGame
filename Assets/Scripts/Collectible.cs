using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : FloatingObject
{
    //[SerializeField] PhysicMaterial flying;
    [SerializeField] PhysicMaterial falling;
    private Collider col;
    private bool shotdown = false;

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!shotdown && collision.gameObject.CompareTag("Projectile"))
        {
            shotdown = true;
            player.Score++;
            rb.useGravity = true;
            col.material = falling;
        }
    }
}
