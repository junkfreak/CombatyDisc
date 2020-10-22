using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    public float timer, maxTimer, minTimer;
    public float dmg, floppyDMG, basicDMG, explosionDMG, sawDMG;
    public Vector3 velocityDisc;
    public float speed, maxSpeed;
    public bool floppy, basic, explosion, saw;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;

        velocityDisc = this.GetComponent<Rigidbody>().velocity;
        speed = velocityDisc.magnitude;
        if(maxSpeed <= speed)
        {
            
            maxSpeed = speed;
            Debug.Log(maxSpeed);
        }
        if(timer <= 0)
        {

          //  this.gameObject.SetActive(false);
            timer = maxTimer;
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject. tag == "Player")
        {
            if(speed >= maxSpeed)
            {
               

                if (basic == true)
                {
                    other.gameObject.GetComponent<PlayerMove>().health -= basicDMG;
                    Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                }

                if (floppy == true)
                {
                    other.gameObject.GetComponent<PlayerMove>().health -= floppyDMG;
                    Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                }

                if (saw == true)
                {
                    other.gameObject.GetComponent<PlayerMove>().health -= sawDMG;
                    Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                }

                if (explosion == true)
                {
                    other.gameObject.GetComponent<PlayerMove>().health -= explosionDMG;
                    Debug.Log(other.gameObject.GetComponent<PlayerMove>().health);
                }
            }
        }
    }
}
