using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Cinemachine;

public class PlayerInputHandler : MonoBehaviour
{
    Player player;
    private PlayerInput input;
    private PlayerMove mover;
    public float playerindx;
    public GameObject virtual1, virtual2;
    
    public List<PlayerMove> Movers = new List<PlayerMove>();
    public PlayerMove[] moverss;

        public void Awake()
    {
        
        player = new Player();
        /*player.Gameplay.Shoot.performed += ctx => mover.Shooting();
        player.Gameplay.Jump.performed += ctx => mover.Jumping();*/
        input = GetComponent<PlayerInput>();
        playerindx = input.playerIndex;
        moverss = FindObjectsOfType<PlayerMove>();
        
        mover = moverss[(int)playerindx];
        
        // input.camera = mover.camy;

        if (playerindx == 0)
        {
            virtual1 = GameObject.FindGameObjectWithTag("cinemachine1");
            virtual1.GetComponent<CinemachineFreeLook>().enabled = true;
            mover.cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            mover.camy = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            virtual1.GetComponent<CinemachineVirtualCameraBase>().Follow = mover.gameObject.transform;
            virtual1.GetComponent<CinemachineVirtualCameraBase>().LookAt = mover.gameObject.transform;
            virtual1.GetComponent<CinemachineInputProvider>().PlayerIndex = (int)playerindx;
            
        }
        if (playerindx == 1)
        {
            virtual2 = GameObject.FindGameObjectWithTag("cinemachine2");
            virtual2.GetComponent<CinemachineFreeLook>().enabled = true;
            mover.cam = GameObject.FindGameObjectWithTag("MainCamera1").transform;
            mover.camy = GameObject.FindGameObjectWithTag("MainCamera1").GetComponent<Camera>();
            virtual2.GetComponent<CinemachineVirtualCameraBase>().Follow = mover.gameObject.transform;
            virtual2.GetComponent<CinemachineVirtualCameraBase>().LookAt = mover.gameObject.transform;
            virtual2.GetComponent<CinemachineInputProvider>().PlayerIndex = (int)playerindx;
            
        }

    }

    public void Start()
    {
       /* if (playerindx == 0)
        {
            Debug.Log("find cinemachines1");
            virtual1 = GameObject.FindGameObjectWithTag("cinemachine1").GetComponent<CinemachineVirtualCameraBase>();
            virtual1.gameObject.SetActive(true);
           
            virtual1.Follow = mover.gameObject.transform;
            virtual1.LookAt = mover.gameObject.transform;
            virtual1.GetComponent<CinemachineInputProvider>().PlayerIndex = (int)playerindx;

        }
        if (playerindx == 1)
        {
            Debug.Log("find cinemachines2");
            virtual2 = GameObject.FindGameObjectWithTag("cinemachine2").GetComponent<CinemachineVirtualCameraBase>();
            virtual2.gameObject.SetActive(true);
           
            virtual2.Follow = mover.gameObject.transform;
            virtual2.LookAt = mover.gameObject.transform;
            virtual2.GetComponent<CinemachineInputProvider>().PlayerIndex = (int)playerindx;

        }*/
    }

    public void OnMove(CallbackContext cntxt)
    {
       mover.xAxis = cntxt.ReadValue<Vector2>().x;
       mover.yAxis = cntxt.ReadValue<Vector2>().y;
    }

    public void OnJump(CallbackContext cntxt)
    {
        mover.Jumping();
    }

    public void OnShoot(CallbackContext cntxt)
    {
        mover.Shooting();
    }

}
