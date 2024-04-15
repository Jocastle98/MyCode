using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{

    public GameObject sparkEffect;
    public GameObject Bullet;
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Bullet")
        {

            ContactPoint cp = coll.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            GameObject spa = Instantiate(sparkEffect, cp.point, rot) as GameObject;

            Destroy(spa, 0.5f);
            Destroy(Bullet, 5f);
            Destroy(coll.gameObject);
        }
    }
}
