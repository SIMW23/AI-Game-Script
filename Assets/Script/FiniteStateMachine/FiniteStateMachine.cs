using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField] private TMP_Text stateText;
    public State currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentState = new IdleState(this);
        currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }

    public void SetStateText(string text)
    {
        if(stateText != null)
        {
            stateText.text = text;
        }
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        Debug.Log("exit state");
     
    }   
}
