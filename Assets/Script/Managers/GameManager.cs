using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    void Init();
    void Clear();
}

public class GameManager : MonoBehaviour
{
    #region Singleton
    public OnPlayerController player;
    public static GameManager Instance;
    public PoolManager pool;
    NetworkManager _network = new NetworkManager();
    PacketManager _packet = new PacketManager();
    public static NetworkManager Network { get { return Instance._network; } }
    public static PacketManager Packet { get { return Instance._packet; } }
    #endregion
    private void Awake()
    {
        Instance = this;
        pool = GetComponent<PoolManager>();

        InitializeGame();
        Network.Init();
    }


    private void InitializeGame()
    {
        // 초기 설정 예: 플레이어 설정, 게임 상태 초기화 등
        if (player != null)
        {
            //player.Initialize(); // 플레이어 초기화 메서드 호출 (필요하다면)
        }
        // 추가적인 초기화 로직
    }
    void Update()
    {
        Network.Update();
    }
}

