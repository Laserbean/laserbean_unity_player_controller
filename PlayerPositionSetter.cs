using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPositionSetter : MonoBehaviour
{
    private void Awake()
    {
        PlayerPosition.PlayerTransform = this.transform;
    }

}



public static class PlayerPosition
{
    public static Transform PlayerTransform;


    public static Vector3 Position {
        get => PlayerTransform.position;
    }

}
