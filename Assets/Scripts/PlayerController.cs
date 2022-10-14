using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float oX = Input.GetAxis("Horizontal");
        float oZ = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(oX, 0, oZ));
        Debug.Log(oX + " " + oZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
