using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    const int gridSize = 10;
    Vector2Int gridPos;
    [SerializeField] Color exploredColor;
    [SerializeField] Color unexploredColor;

    public bool isExplored = false;
    public Waypoint previousWaypoint;

    private void Update()
    {
        if (isExplored)
        {
            SetTopColor(exploredColor);
        }
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRend = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRend.material.color = color;
    }
}
