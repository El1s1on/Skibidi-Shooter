using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health Instance { get; private set; }

    [Title("Health")]
    [SerializeField] private float maxHealth;
    private float health;
    public bool died { get; private set; }

    private void Awake()
    {
        Instance = this;
        health = maxHealth;
    }

    public float HealthValue
    {
        get
        {
            return health;
        }
        private set
        {
            if (value > maxHealth) health = maxHealth;
            else if (health < 0)
            {
                health = 0;
            } 

            health = value;
        }
    }

    public void Damage(float amount) 
    {
        if (died) return;
        HealthValue -= amount;

        if(HealthValue <= 0)
        {
            Death();
        }
    }

    public void ResetHealth()
    {
        HealthValue = maxHealth;
        died = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float GetMaxHealth() => maxHealth;

    private void Death()
    {
        died = true;
        FindObjectOfType<DieScreen>().Show();
    }
}
