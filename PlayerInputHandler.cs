using System.Collections;
using System.Collections.Generic;
using Laserbean.CoreSystem;
using Laserbean.General;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    public PlayerInputData playerInputData;

    private void Start()
    {
        playerInputData = new();
    }

    public void OnMove(InputAction.CallbackContext value) => On_Move(value.ReadValue<Vector2>());
    public void OnMove(InputValue value) => On_Move(value.Get<Vector2>());


    void On_Move(Vector2 dir)
    {
        if (GameManager.Instance.IsRunning) {
            playerInputData.MoveDirection = dir;
        }
        else {
            playerInputData.MoveDirection = Vector2.zero;
        }

    }


    public void OnFire(InputAction.CallbackContext value) => OnFire(value.ReadValue<float>());
    public void OnFire(InputValue value) => OnFire(value.Get<float>());


    void OnFire(float value)
    {
        Debug.Log("FIRE".DebugColor(Color.red));
        // playerInputData.MainAttack = value.Get<float>() > 0.5f; 
        if (value > 0.5f) {
            playerInputData.MainAttack.Pressed.SetTrue();
            playerInputData.MainAttack.Released.SetFalse();
        }
        else {
            playerInputData.MainAttack.Pressed.SetFalse();
            playerInputData.MainAttack.Released.SetTrue();
        }

    }

    public void OnMelee(InputAction.CallbackContext value) => OnMelee(value.ReadValue<float>());
    public void OnMelee(InputValue value) => OnMelee(value.Get<float>());

    void OnMelee(float value)
    {
        // playerInputData.MeleeAttack = value.Get<float>() > 0.5f; 
        if (value > 0.5f) {
            playerInputData.MeleeAttack.Pressed.SetTrue();
            playerInputData.MeleeAttack.Released.SetFalse();

        }
        else {
            playerInputData.MeleeAttack.Pressed.SetFalse();
            playerInputData.MeleeAttack.Released.SetTrue();
        }
    }

    // public void OnFireCharged(InputValue value) {
    //     playerInputData.MainAttack = true; 
    // }

    public void OnJump(InputAction.CallbackContext value) => OnJump();
    public void OnJump(InputValue value) => OnJump();

    public void OnJump()
    {
        playerInputData.Jump.SetTrue();
    }

    public void OnDash(InputAction.CallbackContext value) => OnDash();
    public void OnDash(InputValue value) => OnDash();

    void OnDash()
    {
        playerInputData.Dash.SetTrue();
    }

    public void OnReload(InputAction.CallbackContext value) => OnReload();
    public void OnReload(InputValue value) => OnReload();

    public void OnReload()
    {
        playerInputData.Reload.SetTrue();
    }


    public void OnPee(InputAction.CallbackContext value) => OnPee();
    public void OnPee(InputValue value) => OnPee();

    void OnPee()
    {
        Debug.Log("PEE".DebugColor(Color.yellow)); 
    }



}


public class PlayerInputData
{
    public Vector2 MoveDirection = Vector2.zero;

    public InputBool Jump = new(); //unused for topdown.
    public InputBool Dash = new();
    public InputBool Reload = new();


    public InputEdgeBool MainAttack = new();
    public InputEdgeBool SecondAttack = new();
    public InputEdgeBool MeleeAttack = new();

    public Vector2 Look = Vector2.zero;

    public InputBool Interact = new();
    public InputBool Use = new();


}

public class InputBool
{
    public bool Value { get; set; }

    public InputBool(bool initial = false)
    {
        Value = initial;
    }

    public void SetTrue()
    {
        Value = true;
    }

    public void SetFalse()
    {
        Value = false;
    }


    // public void Use() {
    //     Value = false; 
    // }
    public static implicit operator bool(InputBool curinputbool)
    {
        var curbool = curinputbool.Value;
        curinputbool.Value = false;

        return curinputbool != null && curbool;


        // return curinputbool != null && curinputbool.Value;
    }


}


public class InputEdgeBool
{
    public InputBool Pressed { get; set; }
    public InputBool Released { get; set; }

    public InputEdgeBool()
    {
        Pressed = new();
        Released = new();
    }
}

