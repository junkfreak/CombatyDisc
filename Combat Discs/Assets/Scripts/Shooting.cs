using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed;
    public GameObject projectile;
    public Transform home;
    public float timer, maxTimer;

    public GameObject def, saw, bounce, explo, discs;
    public List<GameObject> types = new List<GameObject>();

    public bool spawned;
    public float typeNR;

    GameObject disc;

    //player collision
    public Transform playerHome;
    public PlayerMove playMove;

    
    void Start()
    {
       /*def = GameObject.FindGameObjectWithTag("defaultSpawn");
        types.Add(def);
        saw = GameObject.FindGameObjectWithTag("sawSpawn");
        types.Add(saw);
        bounce = GameObject.FindGameObjectWithTag("bounceSpawn");
        types.Add(bounce);
        explo = GameObject.FindGameObjectWithTag("explosionSpawn");
        types.Add(explo);*/
        discs = GameObject.FindGameObjectWithTag("defaultSpawn");

        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
       
        if(spawned == true)
        {
            projectile.transform.Rotate(5 * speed * Time.deltaTime, 5 * speed * Time.deltaTime, 0);
        }
        
        if(spawned == false)
        {
            shooting();
        }
    }

    public void shooting()
    {
        if(spawned == false)
        {
            typeNR = Random.Range(0, 5);

        }

        if(typeNR == 1)
        {
             disc = ObjectPooler.SharedInstance.GetPooledObject("defaultDisc" /*add tag of disc here*/);
        }
        if (typeNR == 2)
        {
             disc = ObjectPooler.SharedInstance.GetPooledObject("sawDisc" /*add tag of disc here*/);
        }
        if (typeNR == 3)
        {
             disc = ObjectPooler.SharedInstance.GetPooledObject("bounceDisc" /*add tag of disc here*/);
        }
        if (typeNR == 4)
        {
             disc = ObjectPooler.SharedInstance.GetPooledObject("explosionDisc" /*add tag of disc here*/);
        }




        if (disc != null)
        {
            disc.transform.position = home.transform.position;
            disc.transform.rotation = Quaternion.Euler(0,0,90);
            projectile = disc;
            disc.SetActive(true);
            disc.GetComponent<Rigidbody>().isKinematic = true;
            disc.GetComponent<MeshCollider>().isTrigger = true;
            spawned = true;
        }
        timer = maxTimer;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (spawned)
        {
            if (other.gameObject.tag == "Player")
            {
                playMove = other.GetComponent<PlayerMove>();
                playerHome = other.gameObject.transform.GetChild(0);



                if (playMove.loaded == false)
                {
                    disc.transform.position = playerHome.position;
                    disc.transform.rotation = playerHome.rotation;
                    disc.transform.SetParent(playerHome);
                    playMove.projectile = projectile;
                    disc = null;
                    projectile = null;
                    playMove.loaded = true;
                    spawned = false;
                }

            }
        }
    }
       
}
