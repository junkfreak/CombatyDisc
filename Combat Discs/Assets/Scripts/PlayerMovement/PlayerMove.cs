using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Player player;
    //movement
    Vector2 movement;
    public CharacterController charCont;
    public float movementSpeed;
    public float turnSmoothTime;
    public float turnSmoothVelocity;
    public Transform cam, discHome;
    public float jumpHeight;

    public float xAxis, yAxis;

    //gravity
    public float gravity;
    Vector3 velocity;

    //Groundcheck
    public float groundDistance;
    public float sphereCastSize;
    public bool grounded;

    //shooting
    public float shootingSpeed;
    public GameObject projectile;
    public Transform home;
    public float shootTimer, shootMaxTimer;
    public bool loaded;

    //multiplayer
    public int playerindx;

    private void Awake()
    {
        player = new Player();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //player.Gameplay.Shoot.performed += ctx => Shooting();
       // player.Gameplay.Jump.performed += ctx => Jumping();
        player.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>() ;
  
        player.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        // gravity applied
        velocity.y += gravity * Time.deltaTime;
        charCont.Move(velocity * Time.deltaTime);

        //groundcheck part
        Vector3 p1 = transform.position + charCont.center;
        RaycastHit hit;

        if(Physics.SphereCast(p1, sphereCastSize, -transform.up, out hit , groundDistance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        //movement part
        MovePlayer();
        
       
      
    }
  
    public int GetPlayerIndex()
    {
        return playerindx;
    }
    public void MovePlayer()
    {
        /* xAxis = movement.x;
         yAxis = movement.y;*/


        Vector3 m = new Vector3(xAxis, 0f, yAxis).normalized;

        if(m.magnitude >= 0.1f)
        {
            float targetAngl = Mathf.Atan2(m.x, m.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngl, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngl, 0f) * Vector3.forward;




            charCont.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
        }

        
    }
    public void Jumping()
    {
        if (grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
       
    }
    public void Shooting()
    {
        if(loaded == true)
        {
            
            projectile.transform.SetParent(null);
            projectile.GetComponent<MeshCollider>().isTrigger = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().AddForce(discHome.transform.forward * shootingSpeed);
            projectile = null;
            loaded = false;


        }
     
    }

    private void OnEnable()
    {
        player.Gameplay.Enable();
    }
    private void OnDisable()
    {
        player.Gameplay.Disable();
    }
}
