using System;
using DG.Tweening;
using ScriptableObjectArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectionItem : MonoBehaviour
{
    [SerializeField] private GameEvent updateCoinEvent;
    [SerializeField] private GameObject coinObject;
    
    private GameObject _coin;
    
    private int _minValue = 5;
    private int _maxValue = 10;
    
    private void ReturnPoolAfterTime(float time)
    {
        DOVirtual.DelayedCall(time, () =>
        {
            ObjectPoolManager.ReturnObjectToPool(_coin);
            UIGamePlayManager.Instance.collectedCoin += (int)Random.Range(_minValue,_maxValue);
            updateCoinEvent.Raise();
        });

    }

    public void DropCoin(Transform spawnPos, bool isBase)
    {

        Vector3 endPos = new Vector3(0,0,0);
        
        if (!isBase)
        {
            endPos = new Vector3(Random.Range(spawnPos.position.x - 0.5f, spawnPos.position.x + 0.5f),
                Random.Range(spawnPos.position.y - 0.5f, spawnPos.position.y + 0.5f), spawnPos.position.z);
            _coin = ObjectPoolManager.SpawnObject(coinObject, spawnPos.position, Quaternion.identity,
                ObjectPoolManager.PoolType.Coin);
        }
        else if(isBase && gameObject.layer == 10)
        {
            return;
        }else if (isBase && gameObject.layer == 11)
        {
            endPos = new Vector3(spawnPos.position.x - 0.5f,
                Random.Range(spawnPos.position.y - 0.5f, spawnPos.position.y + 0.5f), spawnPos.position.z);
            _coin = ObjectPoolManager.SpawnObject(coinObject, spawnPos.position, Quaternion.identity,
                ObjectPoolManager.PoolType.Coin);
        }
        
        _coin.transform.DOJump(endPos, 0.5f, 2, 0.5f);

        DOVirtual.DelayedCall(1.5f, () =>
        {
            _coin.transform.DOMove(new Vector3(-1.8f, 3.5f), 0.5f);
        });
        
        ReturnPoolAfterTime(2.5f);

    }
}
