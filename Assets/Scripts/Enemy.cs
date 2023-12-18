using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FloatingObject
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Health--;
        }
    }
}
