using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    bool isInDirectMode = false;//todo consider making static later if more than this code depends on it

    [SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    //todo fix issue with click to move and WSAD conflicting

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G)) // G for gamepad. 
        {
            //print("g");
            //TODO allow player to map later or add to a menu
            isInDirectMode = !isInDirectMode;//toggle
            currentClickTarget = transform.position; //clear click target when switching to gamepad mode
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();////mouse movement
        }
    }

    private void ProcessDirectMovement()
    {
        // read inputs
        //float h = CrossPlatformInputManager.GetAxis("Horizontal");//if building cross platform
        //float v = CrossPlatformInputManager.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        print(h + "<-h v-> " + v);

        // calculate camera relative direction to move:
        Vector3 cameraForwardVector = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForwardVector + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            //print("Cursor raycast hit" + cameraRaycaster.layerHit);
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;
                case Layer.Enemy:
                    print("Enemy - do nothing");
                    break;
                default:
                    print("Unexpected layer found...Default - do nothing");
                    break;
            }
        }

        var playerToClickPoint = currentClickTarget - transform.position;//Target - Source = the path to get there
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            thirdPersonCharacter.Move(currentClickTarget - transform.position, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);//if it's close enough, then stop (prevents jiggy animations near the end)
        }
    }
}

