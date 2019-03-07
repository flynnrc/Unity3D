using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    private Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable,
        Layer.Water
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit raycastHit;
    public RaycastHit hit
    {
        get { return raycastHit; }
    }

    private Layer _layerHit;
    public Layer layerHit
    {
        get { return _layerHit; }
        set { _layerHit = value; }
    }

    #region layer changed delegate
    public delegate void OnLayerChange(Layer newLayer);// declare new delegate type
    public event OnLayerChange layerChangeObservers; //instantiate observer set
    #endregion

    void Start()
    {
        viewCamera = Camera.main;
        //layerChangeObservers += SomeLayerChangeHandler;//add to set of handling functions
        //layerChangeObservers(); //call all delegates
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)//todo other layers
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                print(layer);
                raycastHit = hit.Value;
                if (layerHit != layer)
                {
                    layerHit = layer;
                    layerChangeObservers(layer); //call all delegates
                }
                layerHit = layer;
                return;
            }
        }

        if (nameof(layerHit) != nameof(Layer.RaycastEndStop))
        {
            raycastHit.distance = distanceToBackground;
            layerHit = Layer.RaycastEndStop;
            layerChangeObservers(layerHit);
        }

        // Otherwise return background hit
        //raycastHit.distance = distanceToBackground;
        //layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);//form ray from camera to mouse point

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
