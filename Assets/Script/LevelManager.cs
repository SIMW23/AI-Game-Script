using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int SpawnX;
    [SerializeField] private int SpawnY;
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 gridOrigin;
    [SerializeField] private GameObject tilePrefab;
    public int gridWidth;
    public int gridHeight;
    [SerializeField] private float cellSize;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAtCell(SpawnX, SpawnY);
        // pathfinding = new Pathfinding(gridWidth, gridHeight);
        // cellSize = gridWidth*gridHeight;
        SpawnTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAtCell(int x, int y)
    {
        Vector3 worldPosition = GetWorldPosition(x, y);
        Instantiate(player, worldPosition, Quaternion.identity);
    }

    private void SpawnTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPosition = GetWorldPosition(x, y);
                GameObject tile = Instantiate(tilePrefab, worldPosition, Quaternion.identity);
                tile.transform.parent = transform; 
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        float worldX = x * cellSize + gridOrigin.x;
        float worldY = y * cellSize + gridOrigin.y;
        return new Vector3(worldX, worldY, 0);
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

