using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Congig", fileName = "New Wave Config")]
public class WaveConfigSo : ScriptableObject
{
    [SerializeField] List <GameObject> EnemyPrefabs;
    [SerializeField] Transform PathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;
    public int GetEnemyCount()
    {
        return EnemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return EnemyPrefabs[index];
    }
    public Transform GetStartingWaypoint()
    {
        return PathPrefab.GetChild(0);
    }
    public List<Transform> GetWaypoints()
    {
        List <Transform> Waypoints = new List<Transform>();
        foreach (Transform child in PathPrefab)
        {
            Waypoints.Add(child);
        }
        return Waypoints;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }


    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenSpawns - spawnTimeVariance,timeBetweenSpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime,minimumSpawnTime,float.MaxValue);
    }
}
