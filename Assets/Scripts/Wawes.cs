using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wawes : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    private Animation waveAnimation;
    private int enemies;
    private int waveCount;

    private void OnEnable() => Enemy.OnEnemyDeath += OnEnemyDeath;
    private void OnDisable() => Enemy.OnEnemyDeath -= OnEnemyDeath;

    private void Start()
    {
        waveAnimation = GameObject.FindGameObjectWithTag("WaveCount").GetComponent<Animation>();

        StartCoroutine(StartNewWave());
    }

    public IEnumerator SpawnEnemys(Transform[] points, int amount)
    {
        List<Transform> pointArray = new List<Transform>(points);

        for (int i = 0; i < amount; i++)
        {
            if (i > pointArray.Count)
            {
                pointArray = new List<Transform>(points);
            }

            int randomIndex = Random.Range(0, pointArray.Count);
            Instantiate(enemyPrefab, pointArray[randomIndex].position, Quaternion.identity);
            pointArray.RemoveAt(randomIndex);
            enemies++;

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnEnemyDeath()
    {
        enemies--;

        if(enemies == 0)
        {
            StartCoroutine(StartNewWave());
        }
    }

    private IEnumerator StartNewWave()
    {
        yield return new WaitForSeconds(3f);
        waveCount++;

        waveAnimation.GetComponentInChildren<TextMeshProUGUI>().text = $"Волна {waveCount}";
        waveAnimation.Play();
        
        FindObjectOfType<Character>().ResetGrenades();
        StartCoroutine(SpawnEnemys(spawnPoints, 2 + waveCount));
    }
}
