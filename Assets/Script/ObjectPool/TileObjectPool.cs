using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectPool : MonoBehaviour
{
    public GameObject tilePrefab; // 타일 프리팹
    public int poolSize = 30; // 초기 풀 크기
    private List<GameObject> pool; // 타일 풀

    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(tilePrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetTile()
    {
        foreach (var tile in pool)
        {
            if (!tile.activeInHierarchy)
            {
                tile.SetActive(true);
                return tile;
            }
        }

        // 필요한 경우 새로운 타일을 생성
        GameObject newTile = Instantiate(tilePrefab);
        pool.Add(newTile);
        return newTile;
    }

    public void ReturnTile(GameObject tile)
    {
        tile.SetActive(false);
    }
}

