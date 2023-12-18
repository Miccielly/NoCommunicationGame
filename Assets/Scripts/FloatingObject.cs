using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    //private Arena arena;
    protected Rigidbody rb;
    protected PlayerAttributes player;

    [Range(10f, 18f)][SerializeField] protected float speed;

    protected virtual void Awake()
    {
        //arena = transform.parent.parent.GetComponent<Arena>();
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>().GetPlayerAttributes();    //not best way to get reference !Must be in start as PlayerAttributes constructor is in awake!

        float minSpeed = -speed;
        float maxSpeed = speed;

        rb.velocity = new Vector3(GetRandom(minSpeed, maxSpeed), GetRandom(minSpeed, maxSpeed), GetRandom(minSpeed, maxSpeed));
    }

    private float GetRandom(float min, float max)
    {
        return Random.Range(min, max);
    }
}
