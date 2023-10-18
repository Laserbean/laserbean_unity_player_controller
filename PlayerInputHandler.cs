using System.Collections;
using System.Collections.Generic;
using Laserbean.CoreSystem;
using UnityEngine;

using UnityEngine.InputSystem; 

public class PlayerInputHandler : MonoBehaviour
{

    public PlayerInputData playerInputData; 

    private void Start() {
        playerInputData = new(); 
    }

    public void OnMove(InputValue value) {
        Vector2 diirection = value.Get<Vector2>();
        if (GameManager.Instance.IsRunning) {
            playerInputData.MoveDirection = diirection;  
        } else {
            playerInputData.MoveDirection = Vector2.zero;  
        }
    }

    public void OnFire(InputValue value) {
        playerInputData.MainAttack = true; 
    }

    public void OnUse(InputValue value) {
        playerInputData.SecondAttack = true; 
    }

    // public void OnFireCharged(InputValue value) {
    //     playerInputData.MainAttack = true; 
    // }

    public void OnJump(InputValue value) {
        playerInputData.Jump = true; 
    }

    public void OnDash(InputValue value) {
        playerInputData.Dash = true; 
    }

    
}


public class PlayerInputData
{
    public Vector2 MoveDirection = Vector2.zero; 

    public bool Jump = false; //unused for topdown.
    public bool JumpPressed() {
        var jj = Jump; 
        Jump = false; 
        return jj;    
    }

    public bool Dash = false;
    public bool DashPressed() {
        var jj = Dash; 
        Dash = false; 
        return jj;    
    }

    public bool MainAttack = false; 
    public bool SecondAttack = false; 
    public bool Melee = false; 

    public Vector2 Look = Vector2.zero; 

    public bool Interact = false; 
    public bool Use = false; 


}