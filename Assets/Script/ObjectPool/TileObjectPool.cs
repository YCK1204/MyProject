using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectPool : MonoBehaviour
{
    public GameObject tilePrefab; // Ÿ�� ������
    public int poolSize = 30; // �ʱ� Ǯ ũ��
    private List<GameObject> pool; // Ÿ�� Ǯ

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

        // �ʿ��� ��� ���ο� Ÿ���� ����
        GameObject newTile = Instantiate(tilePrefab);
        pool.Add(newTile);
        return newTile;
    }

    public void ReturnTile(GameObject tile)
    {
        tile.SetActive(false);
    }
}

