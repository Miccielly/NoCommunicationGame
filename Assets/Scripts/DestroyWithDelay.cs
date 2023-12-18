using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    [SerializeField] private float delay;
    void Start()
    {
        Invoke(nameof(DestroyThisObject), delay);
    }

    private void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
