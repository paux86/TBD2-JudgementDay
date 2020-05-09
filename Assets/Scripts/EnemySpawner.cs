using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameState gameState;
    private int bossesDefeated;
    [SerializeField] private int bossDifficultyModifier;
    private int MIN_ENEMY_NUMBER = 2;
    [SerializeField] List<WaveConfig> waveConfigs = default;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] bool isComplete = false; // for debugging



    // Start is called before the first frame update
    IEnumerator Start()
    {
        gameState = FindObjectOfType<GameState>();
        bossesDefeated = gameState.GetBossesDefeated();
        this.bossDifficultyModifier = gameState.bossDifficultyMod;
        Debug.Log("bossDifficultyModifier: " + bossDifficultyModifier);

        do
        {
            yield return StartCoroutine(RandomizeEnemyNumbers());
        } while (looping);
    }

    private IEnumerator RandomizeEnemyNumbers()
    {
        int minRandomEnemyNumber = (MIN_ENEMY_NUMBER + bossesDefeated + (int)(bossDifficultyModifier / 2));
        int maxRandomEnemyNumber = (4 + (bossesDefeated * bossDifficultyModifier));
        int randomEnemyNumber = Random.Range(minRandomEnemyNumber, maxRandomEnemyNumber);
        Debug.Log("randomEnemyNumber: " + randomEnemyNumber);

        for(int i = 0; i < waveConfigs.Count; i++)
        {
            waveConfigs[i].SetNumberOfEnemies(randomEnemyNumber);
        }

        yield return StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        if(waveConfigs != null)
        {
            for (int i = startingWave; i < waveConfigs.Count; i++)
            {
                var currentWave = waveConfigs[i];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            }
            isComplete = true;
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        int numEnemies = waveConfig.GetNumberOfEnemies();
        for (int i = 0; i < numEnemies; i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().maxHealth += (25 * bossDifficultyModifier);
            newEnemy.GetComponent<Enemy>().money = Random.Range(0,5);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    public bool isWavesComplete()
    {
        return this.isComplete;
    }

    public void SetWaveComplete(bool isComplete)
    {
        this.isComplete = isComplete;
    }
}
