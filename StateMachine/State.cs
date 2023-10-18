using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.CoreSystem; 

namespace Laserbean.FiniteStateMachine {

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected Core core;    

    public float startTime { get; protected set; }

    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.Core;
    }

    public virtual void OnEnter()
    {
        startTime = Time.time;
        entity.Animator?.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void OnExit()
    {
        entity.Animator?.SetBool(animBoolName, false);
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnFixedUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}

}


