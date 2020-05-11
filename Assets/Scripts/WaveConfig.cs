using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab = default;
    [SerializeField] GameObject pathPrefab = default;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberofEnemies = 5;
    [SerializeField] float moveSeped = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public void SetEnemyPrefab(GameObject enemy)
    {
        this.enemyPrefab = enemy;
    }

    public GameObject GetPathPrefab()
    {
        return pathPrefab;
    }

    public void SetPathPrefab(GameObject path)
    {
        this.pathPrefab = path;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public int GetNumberOfEnemies()
    {
        return numberofEnemies;
    }

    public void SetNumberOfEnemies(int number)
    {
        this.numberofEnemies = number;
    }

    public float GetMoveSpeed()
    {
        return moveSeped;
    }

    public List<Transform> GetWayPoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
}
