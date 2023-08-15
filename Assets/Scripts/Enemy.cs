using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth, minHealth;

    [Title("Vars")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private GameObject[] skins;
    private bool readyForAttack = true;
    private bool playerInRange;
    private float health;
    private bool died;

    [Title("TriggerSphere")]
    [SerializeField] private string playerTag;
    [SerializeField] private float radius;

    [Title("Components")]
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    [Title("Effects")]
    public GameObject brokenToiletPrefab;


    public delegate void EnemyDeathDelegate();
    public static event EnemyDeathDelegate OnEnemyDeath;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag).transform;

        Customization();
        StartCoroutine(Think());
    }

    private IEnumerator Think()
    {
        while (true)
        {
            if (agent != null)
            {
                if (agent.isOnNavMesh)
                {
                    if (readyForAttack && !Health.Instance.died && !GameData.Instance.GetPauseVar())
                    {
                        agent.SetDestination(player.transform.position);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                    }
                    
                    animator.SetBool("Move", agent.velocity.magnitude > 0.15f);
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(playerTag))
            {
                playerInRange = true;

                if(readyForAttack && !Health.Instance.died)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
            else
            {
                playerInRange = false;
            }
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0 && !died)
        {
            health = 0;
            Die();
        }
    }

    private IEnumerator AttackPlayer()
    {
        readyForAttack = false;
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("Attack");
        if (playerInRange) Health.Instance.Damage(damage);
        
        yield return new WaitForSeconds(attackCooldown);
        readyForAttack = true;
    }

    private void Die()
    {
        died = true;
        Optimization();
        OnEnemyDeath?.Invoke();
        GameObject brokenToilet = Instantiate(brokenToiletPrefab, transform.position, transform.rotation);

        Rigidbody[] toiletFragments = brokenToilet.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody fragment in toiletFragments)
        {
            fragment.AddExplosionForce(Random.Range(5f,20f), transform.position, Random.Range(20f, 50f), Random.Range(.5f, 1f), ForceMode.Impulse);
        }

        Destroy(brokenToilet,5f);
        Destroy(gameObject);
    }
    private void Optimization()
    {
        GameObject[] huynya = GameObject.FindGameObjectsWithTag("Trash");

        if (huynya.Length > 3)
        {
            for (int i = 0; i < huynya.Length - 3; i++)
            {
                Destroy(huynya[i]);
            }
        }
    }

    private void Customization()
    {
        float randomSize = Random.Range(3f, 4f);
        float randomHealth = Random.Range(minHealth, maxHealth);

        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        health = randomHealth;
        skins[Random.Range(0, skins.Length)].SetActive(true);
    }

    public float GetMaxHealth() => maxHealth;
}
