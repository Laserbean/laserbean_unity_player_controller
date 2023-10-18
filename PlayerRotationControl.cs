using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem; 

using Laserbean.General; 

namespace Laserbean.PlayerControl {

public class PlayerRotationControl : MonoBehaviour
{
    [SerializeField] float MaxCameraDistanceFromPlayer= 2f; 
    [SerializeField] GameObject camerafocus; 
    [SerializeField] bool isFake3d; 
    [SerializeField]  bool useMouse = false; 

    private void Awake() {
        if (playerObject == null) playerObject = this.gameObject; 
    }

    #if UNITY_EDITOR
    [ShowOnly]
    #endif
    [SerializeField]  bool rotationLocked = false; 

    public void DisenableRotation(bool canrot) {
        rotationLocked = !canrot; 
    }


    private void OnEnable() {
        Actions.OnSettingsChange += OnSettingsChange;
    }

    private void OnDisable() {
        Actions.OnSettingsChange -= OnSettingsChange;
        
    }

    void OnSettingsChange() {
        if (SettingsManager.Instance.Ready) {
            useMouse = SettingsManager.Instance.GetBool("mouse"); 
        }
    }

    public void OnPoint(InputValue value) {
        if (useMouse && !rotationLocked) {
            Vector3 mousepos = value.Get<Vector2>().ToVector3(); 
            // // // Debug.Log(mousepos);
            Vector3 playerpos = this.transform.position;
            playerpos = Camera.main.WorldToScreenPoint(this.transform.position);
            float angle = Vector3.SignedAngle(mousepos-playerpos, Vector3.up, Vector3.back);

            Aim(angle, false); 

            float camerafocuspos = Mathf.Min((Vector3.Magnitude(mousepos-playerpos))/100, 2f);
            float angg = isFake3d ? angle : 0f; 

            camerafocus.transform.localPosition = new Vector3(0f, Mathf.Clamp(camerafocuspos, 0f, MaxCameraDistanceFromPlayer) ,0f).Rotate(angg);
        }
    }

    
    public void OnLook(InputValue value) {
        float look_angle =0f; 
        if (!useMouse && !rotationLocked) {
            Vector3 dir = value.Get<Vector2>().ToVector3(); 
            if (dir.magnitude > 0.01f) {
                look_angle = Vector3.SignedAngle(dir, Vector3.up, Vector3.back);

                Aim(look_angle, false); 

                float camerafocuspos = Mathf.Min((Vector3.Magnitude(dir))*2f, 2f);

                float angg = isFake3d ? look_angle : 0f; 
                camerafocus.transform.localPosition = new Vector3(0f, Mathf.Clamp(camerafocuspos, 0f, MaxCameraDistanceFromPlayer) ,0f).Rotate(angg);
            }
        } 
    }

    private float rotateTarget;
    public float rotateSensitivity =1f;


    public void Aim(float angle, bool isdx) {
        if (GameManager.Instance.IsRunning) {
            if (isdx) {
                rotateTarget = (-angle * rotateSensitivity/10f) + this.transform.rotation.eulerAngles.z; 
                SetRotation(angle);
            } else {
                SetRotation(angle);
            }
        }
    }

    public float Rotation { get; private set;}

    [SerializeField] GameObject playerObject; 

    [SerializeField] Animator animator; 
    void SetRotation(float angle) {
        Rotation = angle; 

        if (!isFake3d) {
            playerObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        } else {
            playerObject.transform.rotation = Quaternion.identity;
        }
        
        Vector2 lookdir = Vector2.up.Rotate(angle);
        if (animator == null)   return; 
        animator?.SetFloat("rotation", angle); 
        animator?.SetFloat("Horizontal", lookdir.x);
        animator?.SetFloat("Vertical", lookdir.y);

    }


}


}