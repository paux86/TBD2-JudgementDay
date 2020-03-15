﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] bool isComplete = false; // for debugging



    // Start is called before the first frame update
    IEnumerator Start()
    {

        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        int numEnemies = waveConfig.GetNumberOfEnemies();
        for (int i = 0; i < numEnemies; i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
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

    public bool isWavesComplete()
    {
        return this.isComplete;
    }

    public void SetWaveComplete(bool isComplete)
    {
        this.isComplete = isComplete;
    }
}
