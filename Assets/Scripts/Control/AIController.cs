using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }

        private void Update()
        {
            if(health.IsDead()) { return; }

            if(InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if(timeSinceLastSawPlayer < suspicionTime) {
                //Suspicion State
                SuspicionBehavior();
            }
            else {
                GuardBehavior();
            }

            timeSinceLastSawPlayer = += Time.deltaTime;
        }

        private void AttackBehavior() {
            fighter.Attack(player);
        }

        private void SuspicionBehavior() {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void GuardBehavior() {
                //fighter.Cancel();
                mover.StartMoveAction(guardPosition);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        private void OnDrawGizmosSelected() {
            //Gizmos.color = new Color(100,100,100);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}