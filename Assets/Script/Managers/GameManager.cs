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
        // �ʱ� ���� ��: �÷��̾� ����, ���� ���� �ʱ�ȭ ��
        if (player != null)
        {
            //player.Initialize(); // �÷��̾� �ʱ�ȭ �޼��� ȣ�� (�ʿ��ϴٸ�)
        }
        // �߰����� �ʱ�ȭ ����
    }
    void Update()
    {
        Network.Update();
    }
}

