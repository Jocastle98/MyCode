using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class FireCtrl : MonoBehaviour
{


    public GameObject Bullet;
    public Transform firePos;

    public AudioClip fireSfx;
    private new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
       audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Fire();

        }
    }

    void Fire()
    {

        Instantiate(Bullet, firePos.position, firePos.rotation);
        audio.PlayOneShot(fireSfx, 0.1f);

    }

}