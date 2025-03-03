using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthFill;

        public void SetHealth(int currentHealth, int maxHealth)
        {
            Debug.Log($"{currentHealth} {maxHealth}");
            healthFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}