using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class for states
public abstract class State 
{
    protected FiniteStateMachine stateMachine;
    
    public State(FiniteStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
