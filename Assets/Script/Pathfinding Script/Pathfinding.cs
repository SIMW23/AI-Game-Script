using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    //Convert world position to grid position
    public List<Vector3> FindPath(Vector3 startWorldPostion, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPostion, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);
        List<PathNode> path = FindPath(startX, startY, endX, endY);

        if(path == null)
        {
            return null;
        }
        else
        {
            //convert list of pathnode to list of vector3
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * 0.5f);
            }
            return vectorPath;
        }
    }

    //Instance of pathfinding, used to create a grid with variable size
    public Pathfinding(int width, int height)
    {
        Instance = this;
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) 
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        // Debug.Log(startNode + " " + endNode);

        if (startNode == null || endNode == null) 
        {
            // Invalid Path
            Debug.Log("No path to be found");
            return null;
        }

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++) 
        {
            for (int y = 0; y < grid.GetHeight(); y++) 
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = 99999999;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();
        

        while (openList.Count > 0) {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode) 
            {
                // Reached final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) 
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable) {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }


    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        //Left direction
        if(currentNode.x - 1 >= 0)
        {
            //Left node
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            //Left Down node
            if(currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            //Left Up node
            if(currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }

        //Right direction
        if(currentNode.x + 1 < grid.GetWidth())
        {
            //Right node
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //Right Down node
            if(currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            //Right Up node
            if(currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }

        //Down node
        if(currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));

        //Up node
        if(currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    //calculate path
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

   
    
    private PathNode GetLowestFCostNode(List<PathNode> pathNodesList)
    {
        PathNode lowestFCostNode = pathNodesList[0];
        for(int i = 1; i < pathNodesList.Count; i++)
        {
            if(pathNodesList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodesList[i];
            }
        }
        return lowestFCostNode;
    }   

}

