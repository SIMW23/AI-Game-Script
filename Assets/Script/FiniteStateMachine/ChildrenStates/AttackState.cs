using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State 
{
    public AttackState(FiniteStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
        stateMachine.SetStateText("Attacking");
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(new RunState(stateMachine));
        }
        Debug.Log("Updating Attack State");
        stateMachine.SetStateText("Attacking");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}
