using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputProfileEditorSwitcher : MonoBehaviour
{

    [SerializeField] PlayerInput playerInput;
    private void OnValidate()
    {
        playerInput ??= GetComponent<PlayerInput>();
    }

    [SerializeField] string profile1;
    [EasyButtons.Button]
    public void SwitchProfile1() => playerInput.SwitchCurrentActionMap(profile1);

    [SerializeField] string profile2;
    [EasyButtons.Button]
    public void SwitchProfile2() => playerInput.SwitchCurrentActionMap(profile2);


}

#endif