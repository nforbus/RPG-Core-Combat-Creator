using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target; //SerializeField is a Unity function that allows for the serialization of 'private' fields, by default is is just public fields.

        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
            
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        //Animates the character
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}