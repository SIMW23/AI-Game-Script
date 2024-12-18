using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State 
{
    public RunState(FiniteStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Entering Run State");
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(new IdleState(stateMachine));
        }
        Debug.Log("Updating Run State");
        stateMachine.SetStateText("Moving");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Run State");
    }
}
