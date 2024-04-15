using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{

    public float moveSpeed;
    public float move;
    public float moveVertical;

    public int rotSpeed;
    public float rotate;
    public float rotHorizon;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 12f;
        rotSpeed = 100;

    }

    // Update is called once per frame
    void Update()
    {
        move = moveSpeed * Time.deltaTime;
        rotate = rotSpeed * Time.deltaTime;

        moveVertical = Input.GetAxis("Vertical");
        rotHorizon = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * move * moveVertical);
        transform.Rotate(new Vector3(0.0f, rotate * rotHorizon, 0.0f));
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<Collider>().tag == "itemFast")
        {
            Destroy(coll.gameObject);
            moveSpeed = 12f*2.5f;
        }

        if (coll.GetComponent<Collider>().tag == "itemSlow")
        {
            Destroy(coll.gameObject);
            moveSpeed = 0.8f;
        }

    }
}
