using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    public CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] Vector2 hotSpot = Vector2.zero;

    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (cameraRaycaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, hotSpot, cursorMode);
                UpdateCursorTexture(walkCursor);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(targetCursor, hotSpot, cursorMode);
                UpdateCursorTexture(targetCursor);
                break;
            case Layer.RaycastEndStop:
                UpdateCursorTexture(unknownCursor);
                break;
            default:
                Debug.Log("don't know what cursor to show");
            return;
        }
    }

    private void UpdateCursorTexture(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotSpot, cursorMode);
    }

    //void OnMouseEnter()
    //{
    //    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, cursorMode);
    //}
}
