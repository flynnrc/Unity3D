using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]//basically sets it up so the script will run in edit mode as it would in play mode
[SelectionBase]//sets this object as the base object for clicking, handy to avoid clicking children when all you want is the parent
public class CubeEditor : MonoBehaviour {

    [SerializeField] [Range(1f, 20f)] float gridSize = 10f;

    TextMesh textMesh;

    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = "xx,xx";
    }

    void Update () {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);

        string labelText = snapPos.x / gridSize + "," + snapPos.z / gridSize;
        textMesh.text = labelText;
        gameObject.name = "Cube " + labelText;

	}
}
