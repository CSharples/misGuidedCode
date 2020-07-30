using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    Rigidbody2D rb1;
    Rigidbody2D rb2;
    Renderer b1Rend;
    Renderer b2Rend;  
    public float backgroundSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        rb1 = background1.GetComponent<Rigidbody2D>();
        rb2 = background2.GetComponent<Rigidbody2D>();
        b1Rend = background1.GetComponent<Renderer>();
        b2Rend = background2.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb1.velocity = -transform.right * backgroundSpeed * Time.deltaTime;
        rb2.velocity = -transform.right * backgroundSpeed * Time.deltaTime;
        if (b1Rend.isVisible == false && background1.transform.position.x < -5)
        {
            background1.transform.position = background1.transform.position + new Vector3(2*24.55f, 0, 0);
        }
        if (b2Rend.isVisible == false && background2.transform.position.x < -5)
        {
            background2.transform.position = background2.transform.position + new Vector3(2*24.55f, 0, 0);
        }
    }
}
