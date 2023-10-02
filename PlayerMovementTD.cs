#define USING_ANIMATOR

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


#if USING_ANIMATOR
    Animator animator;
#endif

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>(); // attaches from the thing 

        PlayerPosition.PlayerTransform = this.transform; 


#if USING_ANIMATOR
    animator = this.GetComponent<Animator>(); 
#endif

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
        
#if USING_ANIMATOR
        if (animator && GameManager.Instance.IsRunning) {
            var velocity = velocityToMove.Rotate(-transform.rotation.eulerAngles.z);
            animator.SetFloat("SpeedX", velocity.x);
            animator.SetFloat("SpeedY", velocity.y);

            animator.SetBool("IsMoving", Mathf.Abs(Vector3.Magnitude(velocityToMove)) > 0.1f);
        }
#endif

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

    public void DoMovement(Vector3 move) {
        rigidbody2d.AddForce(move, ForceMode2D.Impulse); 
    }


}
}