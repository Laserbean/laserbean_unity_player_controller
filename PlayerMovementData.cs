using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "PlayerMovementData", order = 0)]
public class PlayerMovementData : ScriptableObject {    

    [field: SerializeField] public int MoveSpeed {get; private set;}

    [field: SerializeField] public int DashSpeed {get; private set;}
    [field: SerializeField] public float DashTime {get; private set;}
    [field: SerializeField] public float DashCooldown {get; private set;}
    
    [field: SerializeField] public int JumpSpeed {get; private set;}
    [field: SerializeField] public float JumpTime {get; private set;}



}