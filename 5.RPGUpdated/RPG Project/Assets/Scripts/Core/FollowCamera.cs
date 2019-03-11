using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform followTarget = null;

    // Update is called once per frame
    void LateUpdate()//camera should move after animations (https://docs.unity3d.com/Manual/ExecutionOrder.html)
    {
        transform.position = followTarget.position;
    }
}
