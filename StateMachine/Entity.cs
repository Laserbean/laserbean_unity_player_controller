using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds all the entities. 

using Laserbean.CoreSystem; 

namespace Laserbean.FiniteStateMachine {

public abstract class Entity : MonoBehaviour
{
    public Animator Animator { get; private set; }
	public FiniteStateMachine StateMachine;

    public Core Core; 

	protected virtual void Awake() {
		Core = GetComponentInChildren<Core>();

		Animator = GetComponent<Animator>();
		StateMachine = new FiniteStateMachine();
	}

    private void Update()
    {
		Core.LogicUpdate();

        StateMachine.CurrentState?.OnUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState?.OnFixedUpdate();
    }
}

}


