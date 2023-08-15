using UnityEngine;

public class Hitboxes : MonoBehaviour
{
    [SerializeField] private float damage;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void Hit(bool deadly)
    {
        if (deadly)
        {
           enemy.Damage(enemy.GetMaxHealth());
            return;
        }

        enemy.Damage(damage);
    }
}
