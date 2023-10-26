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
        playerInputData.MainAttack.SetTrue(); 
    }

    public void OnMelee(InputValue value) {
        playerInputData.MeleeAttack.SetTrue(); 
    }

    // public void OnFireCharged(InputValue value) {
    //     playerInputData.MainAttack = true; 
    // }

    public void OnJump(InputValue value) {
        playerInputData.Jump.SetTrue(); 
    }

    public void OnDash(InputValue value) {
        playerInputData.Dash.SetTrue(); 
    }

    
}


public class PlayerInputData
{
    public Vector2 MoveDirection = Vector2.zero; 

    public InputBool Jump = new(); //unused for topdown.
    public InputBool Dash = new();
    

    public InputBool MainAttack = new();
    public InputBool SecondAttack = new();
    public InputBool MeleeAttack = new();

    public Vector2 Look = Vector2.zero; 

    public InputBool Interact = new();
    public InputBool Use = new();


}

public class InputBool {
    public bool Value { get; set; }

    public InputBool(bool initial = false) {
        Value = initial;
    }

    public void SetTrue() {
        Value = true; 
    }

    public void SetFalse() {
        Value = false; 
    }


    public static implicit operator bool(InputBool curinputbool) {
        var curbool = curinputbool.Value; 
        curinputbool.Value = false; 

        return curinputbool != null && curbool;
    }
}

