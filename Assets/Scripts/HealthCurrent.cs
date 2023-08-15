using UnityEngine;
using UnityEngine.UI;

public class HealthCurrent : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        healthBar.maxValue = Health.Instance.GetMaxHealth();
    }

    private void Update()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, Health.Instance.HealthValue, 5f * Time.deltaTime);
    }
}
