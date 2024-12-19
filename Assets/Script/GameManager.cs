using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private FiniteStateMachine stateMachine;

    [SerializeField] private CharacterMove characterMove;
    void Start()
    {
        //Get FiniteStateMachine component
        stateMachine = GetComponent<FiniteStateMachine>();
        if(stateMachine == null)
        {
            stateMachine = gameObject.AddComponent<FiniteStateMachine>();
        }
        //Change state to IdleState since by default the player is idle
        //stateMachine.ChangeState(new IdleState(stateMachine));
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateState();
    }

    private void UpdateState()
    {
        if(!characterMove.isMoving)
        {
            stateMachine.ChangeState(new RunState(stateMachine));
            Debug.Log("Change to idle state");
        }
        else if(characterMove.isMoving)
        {
            stateMachine.ChangeState(new IdleState(stateMachine));
            Debug.Log("Change to run state");
        }
    }
}
