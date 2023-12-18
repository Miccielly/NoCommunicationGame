using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private ParticleSystem flash;

    public void Shoot()
    {
        flash.Play();
        Instantiate(projectile, transform.GetChild(0).GetChild(0).position,
           Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x + 90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)));  //+90f because of rotation of projectile
    }
}
