using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanHazard : MonoBehaviour
{
    public GameObject windtunnel;
    public GameObject fan;
    public float speed, power;
    public bool powered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (powered)
        {

            fan.transform.Rotate(0, 5 * speed * Time.deltaTime, 0);
        }
        

    }

    private void OnTriggerStay(Collider other)
    {
        if (powered)
        {
            if(other.gameObject.tag == "Player")
            {
                Vector3 offset = fan.transform.position - other.transform.position;
                offset = offset.normalized * power;
                other.GetComponent<CharacterController>().Move(offset*Time.deltaTime);
            }
            if (other.gameObject.GetComponent<Rigidbody>())
            {
                Vector3 offset = fan.transform.position - other.transform.position;
                other.GetComponent<Rigidbody>().AddForce(offset * (power * 2));
            }
        }
    }
}
