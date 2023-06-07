using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target; //SerializeField is a Unity function that allows for the serialization of 'private' fields, by default is is just public fields.

    //Ray lastRay;

    // Update is called once per frame
    void Update()
    {
        //GetMouseButtonDown detects button presses, and a value of 0 indicates the left mousebutton
        if(Input.GetMouseButtonDown(0)) {
            MoveToCursor();
        }
        UpdateAnimator();
        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
    }

    //Moves the player to the position indicated by the 
    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    //Animates the character
    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
    }

    //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //GetComponent<NavMeshAgent>().destination = target.position;
}
