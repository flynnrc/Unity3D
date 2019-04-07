using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        private bool _isAlive;

        public bool isAlive
        {
            get { return _isAlive; }
            private set { _isAlive = value; }
        }

        private void Start()
        {
            isAlive = true;
        }

        public void TakeDamage(float damage)
        {
            if (!isAlive) { return; }

            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0 && isAlive)
            {
                DieBehavior();
            }
        }

        private void DieBehavior()
        {
            isAlive = false;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
