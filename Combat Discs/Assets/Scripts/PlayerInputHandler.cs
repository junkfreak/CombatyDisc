using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    Player player;
    private PlayerInput input;
    private PlayerMove mover;
    public float playerindx;
    
    public List<PlayerMove> Movers = new List<PlayerMove>();
    public PlayerMove[] moverss;

        private void Awake()
    {
        player = new Player();
        /*player.Gameplay.Shoot.performed += ctx => mover.Shooting();
        player.Gameplay.Jump.performed += ctx => mover.Jumping();*/
        input = GetComponent<PlayerInput>();
        playerindx = input.playerIndex;
        moverss = FindObjectsOfType<PlayerMove>();
        
        mover = moverss[(int)playerindx];
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
