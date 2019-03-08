using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] GameObject target;
    private NavMeshAgent navMeshAgent;

    Ray lastRay;

    // Start is called before the first frame update
    void Start()
    {
        this.navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);//100 extends ray
        navMeshAgent.destination = target.transform.position;
    }
}
