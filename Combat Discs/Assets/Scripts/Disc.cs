using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    public float timer, maxTimer, minTimer;
    public float dmg, floppyDMG, basicDMG, explosionDMG, sawDMG;
    public Vector3 velocityDisc;
    public float speed, maxSpeed;
    public bool floppy, basic, explosion, saw, hit;

    //explosion
    public float exploSpeed, exploSize, exploMaxSize;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true)
        {
            timer -= Time.deltaTime;
       
           if (explosion == true)
            {
                this.gameObject.transform.GetChild(0).GetComponent<SphereCollider>().enabled = true;
                if (this.gameObject.transform.GetChild(0).GetComponent<SphereCollider>().radius < exploMaxSize)
                {
                    this.gameObject.transform.GetChild(0).GetComponent<SphereCollider>().radius += exploSpeed * Time.deltaTime;
                }

            }
        }


        velocityDisc = this.GetComponent<Rigidbody>().velocity;
        speed = velocityDisc.magnitude;
        if (maxSpeed <= speed)
        {

            maxSpeed = speed;
            Debug.Log(maxSpeed);
        }
        if (timer <= 0)
        {

            this.gameObject.SetActive(false);
            timer = maxTimer;
            hit = false;

            if (explosion == true)
            {
                this.gameObject.transform.GetChild(0).GetComponent<SphereCollider>().enabled = false;
                this.gameObject.transform.GetChild(0).GetComponent<SphereCollider>().radius = exploSize;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
       // hit = true;
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "props")
        {
            if(speed >= maxSpeed)
            {

                hit = true;
                if (basic == true)
                {
                    if (other.gameObject.tag == "Player")
                    {
                        other.gameObject.GetComponent<PlayerMove>().health -= basicDMG;
                        //other.transform.GetComponentInParent<PlayerMove>().health -= basicDMG;
                        Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                    }
                   
                }

                if (floppy == true)
                {
                    if (other.gameObject.tag == "Player")
                    {
                        other.gameObject.GetComponent<PlayerMove>().health -= floppyDMG;
                        //other.transform.GetComponentInParent<PlayerMove>().health -= floppyDMG;
                        Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                    }
                }

                if (saw == true)
                {
                    if (other.gameObject.tag == "Player")
                    {
                        //other.gameObject.GetComponent<PlayerMove>().health -= sawDMG;
                        other.transform.GetComponentInParent<PlayerMove>().health -= sawDMG;
                        Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                    }
                }

                if (explosion == true)
                {
                    if (other.gameObject.tag == "Player")
                    {
                        other.gameObject.GetComponent<PlayerMove>().health -= explosionDMG;
                       // other.transform.GetComponentInParent<PlayerMove>().health -= explosionDMG;
                        Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                    }
                }
            }
        }
    }
}
