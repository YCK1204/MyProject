using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectPool : MonoBehaviour
{

    public GameObject tilePrefab;
    public int poolSize = 30;
    private List<GameObject> pool;
    // Start is called before the first frame update
    void Start()
    {

        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(tilePrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }    // Update is called once per frame
        }
    public GameObject GetTile()
    {
        foreach (var tile  in pool)
        {
            if(!tile.activeInHierarchy)
            {
                tile.SetActive(true);
                return tile;
            }
        }
        GameObject newTile = Instantiate(tilePrefab);
        pool.Add(newTile);
        return newTile;
    }
        
    public void ReturnTile(GameObject tile)
    {
        tile.SetActive(false);
    }


    }
