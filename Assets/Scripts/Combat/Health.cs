using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public void TakeDamage(float damage) 
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            //print("healthPoints remaining: " + healthPoints);
            if(healthPoints == 0) //we can say ==0 instead of <= 0 because the above line guarantees it won't go below 0.
            {
                Die();
            }
        }

        private void Die()
        {
            if(isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}