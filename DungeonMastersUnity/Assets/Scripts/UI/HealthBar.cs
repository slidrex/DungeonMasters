using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthFill;

        public void SetHealth(int currentHealth, int maxHealth)
        {
            healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
        }
    }
}