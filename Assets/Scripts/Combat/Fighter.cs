using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;  //attack speed

        Transform target;
        float timeSinceLastAttack = 0;

        private void Update()
        {
            //Goes off always
            timeSinceLastAttack += Time.deltaTime;

            //If we don't have a target
            if(!target) { return; }

            //If we have a target but its not close enough to attack
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else //If we're close enough to attack the target
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            print("I'm Attacking");
        }


        public void Cancel()
        {
            target = null;
        }

        //Animation event, public/private not needed
        void Hit()
        {

        }
    }
}