using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target; //SerializeField is a Unity function that allows for the serialization of 'private' fields, by default is is just public fields.

    Ray lastRay;

    // Update is called once per frame
    void Update()
    {
        //GetMouseButtonDown detects button presses, and a value of 0 indicates the left mousebutton
        if(Input.GetMouseButtonDown(0)) {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
