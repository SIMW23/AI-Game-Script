using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            Debug.Log("Shoot out ray");

            if(hit.collider != null && hit.transform == this.transform)
            {
                spriteRenderer.color = Color.red;
                Debug.Log("Hit the tile");
            }
        }
    }
}
