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
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = 0;

        private void Update()
        {
            //Goes off always
            timeSinceLastAttack += Time.deltaTime;

            //If we don't have a target
            if(!target) { return; }

            //If target is dead
            if(target.IsDead()) { return; }

            //If we have a target but its not close enough to attack
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else //If we're close enough to attack the target
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttack();
            }
        }

        private void TriggerAttack()
        {
            // This will trigger the Hit() event
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0;
        }

        //Animation event, public/private not needed
        void Hit()
        {
            //Health healthComponent = target.GetComponent<Health>();
            //healthComponent.TakeDamage(weaponDamage);
            if(!target) { return; }
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if(!combatTarget) { return false; }

            Health targetToCheck = combatTarget.GetComponent<Health>();
            
            //If target exists and is not dead, return true
            return targetToCheck != null && !targetToCheck.IsDead();

        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            print("I'm Attacking");
        }


        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}