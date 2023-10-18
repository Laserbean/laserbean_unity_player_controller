using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.FiniteStateMachine;
using Laserbean.CoreSystem;
using Cinemachine.Utility;
using System.Data;

public class PlayerEntity : Entity
{

    public PlayerAirState InAirState {get; private set;}

    public PlayerDeadState DeadState {get; private set;}
    public PlayerIdleGround IdleGroundState {get; private set;}
    public PlayerWalkGround WalkGroundState {get; private set;}
    public PlayerDashGround DashGroundState {get; private set;}


    public PlayerMovementData movementData; 
    protected override void Awake() {
        base.Awake();

        InAirState      = new PlayerAirState(this, StateMachine, movementData, "inAir");

        DeadState       = new PlayerDeadState(this, StateMachine, movementData, "dead");

        IdleGroundState = new PlayerIdleGround(this, StateMachine, movementData, "idle");
        WalkGroundState = new PlayerWalkGround(this, StateMachine, movementData, "idle");
        DashGroundState = new PlayerDashGround(this, StateMachine, movementData, "idle");

        StateMachine.Initialize(IdleGroundState);
    }
}


public abstract class PlayerState : State
{
    protected PlayerEntity playerEntity; 
    protected Movement2D movement2D; 
    protected PlayerInputHandler playerinput; 
    protected PlayerMovementData movementData; 

    protected StatusController status; 

    public PlayerState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
        playerEntity = entity; 
        movement2D = entity.Core.GetComponentInChildren<Movement2D>(); 
        playerinput = entity.Core.GetComponentInParent<PlayerInputHandler>(); 
        movementData = playerdata; 
        status = entity.Core.GetCoreComponent<StatusController>();
    }
    
    
    public override void OnFixedUpdate() {
        base.OnFixedUpdate();

        if (status.Health.Value <= status.Health.MinValue) {
            stateMachine.ChangeState(playerEntity.DeadState);
        }
    } 
}

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
    {}

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    } 

}



public abstract class PlayerGround : PlayerState
{

    public PlayerGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
    {}

    public override void OnEnter() {
        base.OnEnter();
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();

        if (playerinput.playerInputData.JumpPressed()) {
            stateMachine.ChangeState(playerEntity.InAirState); 
        }
    }
}

public class PlayerAirState : PlayerState
{

    public PlayerAirState(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
    {}

    float start_time = 0f; 
    public override void OnEnter() {
        base.OnEnter();
        start_time = Time.time; 
        Debug.Log("Air state");
    }


    public override void OnFixedUpdate() {
        base.OnFixedUpdate();

        if (Time.time - startTime > movementData.JumpTime) {
            stateMachine.ChangeState(playerEntity.IdleGroundState); 
        }
    }
}

public class PlayerIdleGround : PlayerGround
{
    public PlayerIdleGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
    {}

    public override void OnEnter() {
        base.OnEnter();
        movement2D?.SetVelocityZero(); 
        Debug.Log("idle");

    }


    public override void OnFixedUpdate() {
        base.OnFixedUpdate();

        if (playerinput.playerInputData.MoveDirection.sqrMagnitude > 0) {
            movement2D.SetVelocity(playerinput.playerInputData.MoveDirection * movementData.MoveSpeed); 
            stateMachine.ChangeState(playerEntity.WalkGroundState);
        }
    }
}

public class PlayerWalkGround : PlayerGround
{
    public PlayerWalkGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
    {}

    public override void OnEnter() {
        base.OnEnter();
        Debug.Log("walk");
    }


    public override void OnFixedUpdate() {
        base.OnFixedUpdate();

        if (playerinput.playerInputData.MoveDirection.sqrMagnitude == 0) {
            stateMachine.ChangeState(playerEntity.IdleGroundState);
            return; 
        }
        if (playerinput.playerInputData.DashPressed() && playerEntity.DashGroundState.CanDash()) {
            Debug.Log("Befoer change to dash");
            stateMachine.ChangeState(playerEntity.DashGroundState); 
        }


        movement2D.SetVelocity(playerinput.playerInputData.MoveDirection * movementData.MoveSpeed);         
    }
}

public class PlayerDashGround : PlayerGround
{
    public PlayerDashGround(PlayerEntity entity, FiniteStateMachine stateMachine, PlayerMovementData playerdata, string animBoolName) : base(entity, stateMachine, playerdata, animBoolName)
    {}

    float start_time = 0f; 
    public override void OnEnter() {
        base.OnEnter();
        Debug.Log("Dash");

        start_time = Time.time; 
         
    }


    public override void OnFixedUpdate() {
        base.OnFixedUpdate();

        movement2D.SetVelocity(movement2D.LastDirection * movementData.DashSpeed);
        if (Time.time > start_time + movementData.DashTime) {
            stateMachine.ChangeState(playerEntity.IdleGroundState);
            return; 
        }
    }

    public bool CanDash() {
        return Time.time > start_time + movementData.DashCooldown + movementData.DashTime; 
    }
}