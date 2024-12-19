using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    private Vector3 lastPosition;
    public bool isMoving;

    [SerializeField] private float speed = 5f;
    [SerializeField] private FiniteStateMachine stateMachine;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        stateMachine = GetComponent<FiniteStateMachine>();
        if(stateMachine == null)
        {
            stateMachine = gameObject.AddComponent<FiniteStateMachine>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    private void UpdateMovement()
    {
        
        if (pathVectorList != null) 
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];

            if (Vector3.Distance(transform.position, targetPosition) > 1f) 
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                isMoving = true;
                if (!(stateMachine.currentState is RunState))
                {
                    stateMachine.ChangeState(new RunState(stateMachine));
                }
            }

            else 
            {   
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) 
                {
                    pathVectorList = null;
                    isMoving = false;
                    if (!(stateMachine.currentState is IdleState))
                    {
                        stateMachine.ChangeState(new IdleState(stateMachine));
                    }
                }
            }
        }
    }



//All of these are for getting the mouse position in the world, with z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
