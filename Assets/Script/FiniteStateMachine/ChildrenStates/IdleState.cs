using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State 
{
    public IdleState(FiniteStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        stateMachine.SetStateText("Idling");
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(new RunState(stateMachine));
        }
        Debug.Log("Updating Idle State");
        stateMachine.SetStateText("Idling");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
