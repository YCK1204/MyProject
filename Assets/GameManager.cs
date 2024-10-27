using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OnPlayerController player;
    public static GameManager Instance;
    public PoolManager pool;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;    
    }

}
