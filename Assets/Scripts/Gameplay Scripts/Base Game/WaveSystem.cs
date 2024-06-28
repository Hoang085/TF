using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]   
public class WaveSystem : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public float timeBetweenWaves = 30f;
    private bool waveIsDone = true;

    public int enemyCount;

    public GameObject enemy;
    [SerializeField] List<Unit> unitList = new List<Unit>();    
    private void Update()
    {
        if (waveIsDone)
        {
            StartCoroutine(WaveSpawner());
        }
    }
    IEnumerator WaveSpawner()
    {
        waveIsDone = false;

        for(int i = 0; i < enemyCount; i++)
        {
            BaseUnit enemyUnit = ObjectPoolManager
                .SpawnObject(enemy, new Vector2(2.03f, Random.Range(-0.1f, 0.1f)), quaternion.identity,
                    ObjectPoolManager.PoolType.GameObject).GetComponent<BaseUnit>();
            
            enemyUnit.isEnemy = true;
            int unitIndex = Random.Range(0, unitList.Count);
            enemyUnit.unit = unitList[unitIndex];
            CharSelection.onUnitInitialize?.Invoke();

            yield return new WaitForSeconds(spawnRate);
        }
        spawnRate -= 0.1f;
        enemyCount += 3;

        yield return new WaitForSeconds(timeBetweenWaves);

        waveIsDone = true;
    }
}
