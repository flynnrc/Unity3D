using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]//basically sets it up so the script will run in edit mode as it would in play mode
[SelectionBase]//sets this object as the base object for clicking, handy to avoid clicking children when all you want is the parent
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour {

    Waypoint waypoint;
    TextMesh textMesh;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = "xx,xx";
    }

    void Update ()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.GetGridSize();
        string labelText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        textMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize, 
            0f, 
            waypoint.GetGridPos().y * gridSize);
    }
}
