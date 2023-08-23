using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

using System.Linq;

using Laserbean.General; 


namespace Laserbean.PlayerControl {


public class PlayerMovementTD : MonoBehaviour
{
    Rigidbody2D rigidbody2d; 

    Vector2 velocityToMove; 
    Vector2 velocityWantToMove; 

    bool movementEnabled = true; 
    [SerializeField] float maxspeed = 5f;

    [SerializeField] bool move_player_direction = false; 


    public Animator animator;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>(); // attaches from the thing 
    }

    #region tomove

    // bool useMouse = false; 
    // private void OnEnable() {
    //     Actions.OnSettingsChange += OnSettingsChange;
    // }

    // private void OnDisable() {
    //     Actions.OnSettingsChange -= OnSettingsChange;
    // }
    // void OnSettingsChange() {
    //     if (SettingsManager.Instance.Ready) {
    //         useMouse = SettingsManager.Instance.GetBool("mouse"); 
    //     }
    // }

    #endregion
    
    public void OnMove(InputValue value) {
        Vector2 diirection = value.Get<Vector2>();
        float cur_max_speed = maxspeed;

        if (GameManager.Instance.IsRunning) {
            velocityWantToMove = diirection * cur_max_speed; 

            if (movementEnabled) velocityToMove = velocityWantToMove; 
        } else {
            velocityToMove = Vector2.zero; 
        }



        // Debug.Log("dirr" + diirection); 
    }


    void FixedUpdate() {
        if (animator && GameManager.Instance.IsRunning) {
            // animator.SetFloat("Player_speed", Mathf.Abs(Vector3.Magnitude(velocity))); //TODO
        }

        if (move_player_direction) velocityToMove.Rotate(this.transform.rotation.eulerAngles.z); 
        rigidbody2d.AddForce(velocityToMove); 
        // velocityToMove = Vector2.zero; 
    }



    public void DisenableMovement(bool boo) {
        movementEnabled = boo; 

        if(!boo) {
            velocityToMove = Vector2.zero; 
        } else {
            if (GameManager.Instance.IsRunning) {
                velocityToMove = velocityWantToMove; 
            } else {
                velocityToMove = Vector2.zero; 
            }
        }
    }

    public void EnableMovement() {
        movementEnabled = true; 
    }

    public void DisableMovement() {
        movementEnabled = false; 
    }

    public void Stun(float time) {
        movementEnabled = false; 
        Invoke("EnableMovement", time);
    }

    public void DoMovement(Vector2 move) {
        rigidbody2d.AddForce(move, ForceMode2D.Impulse); 
    }


}
}