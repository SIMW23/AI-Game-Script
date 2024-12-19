using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveExecution : MonoBehaviour
{
    private Pathfinding pathfinding;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private CharacterMove characterPathfind;
    // Start is called before the first frame update
    void Start()
    {
        //Getting LevelManager component
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        //Getting CharacterMove component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            characterPathfind = player.GetComponent<CharacterMove>();
        }
        else
        {
            Debug.LogError("Player not found");
        }
        pathfinding = new Pathfinding(levelManager.gridWidth, levelManager.gridHeight);
    }

    private void Update()
    {
         if(Input.GetMouseButtonDown(0))
        {
            //Get the grid and draw a debug line from node 0,0 to the target position
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if(path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            if (characterPathfind != null)
            {
                characterPathfind.SetTargetPosition(mouseWorldPosition);
            }
            else
            {
                Debug.LogError("CharacterMove component not found on Player GameObject.");
            }
        }
        
        //Setting a node as unwalkable
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            //Set walkable state of the node
            pathfinding.GetNode(x,y).SetIsWalkable(!pathfinding.GetNode(x,y).isWalkable);
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
