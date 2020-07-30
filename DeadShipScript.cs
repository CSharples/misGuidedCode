using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadShipScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 0f;
    void Start()
    {
        Destroy (gameObject, Mathf.Max(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length,this.GetComponent<AudioSource>().clip.length) + delay); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
