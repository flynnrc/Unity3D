using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
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
        cameraRaycaster.layerChangeObservers += OnLayerChanged;
	}
	
	// Update is called once per frame
	void OnLayerChanged(Layer newLayer) {//todo convert this to use observer pattern to notify of a change rather than checking per frame
        print("layer changed called");
        switch (newLayer)
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

    //todo consider de registering from event on leaving all game scenes

    private void UpdateCursorTexture(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotSpot, cursorMode);
    }
}
