using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Fighter fighter;
        Health health;
        GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if(health.IsDead()) { return; }

            if(InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                /*
                if (gameObject.name == "Enemy") {
                    //if you want to print only stuff for this enemy.
                    //can also just use if(gameObject.tag == "Player")
                }
                */
                //print(gameObject.name + " Should chase");

                fighter.Attack(player);
            }
            else {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer <= chaseDistance;
        }
    }
}