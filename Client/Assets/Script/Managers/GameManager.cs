using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    void Init();   // �ʱ�ȭ �޼���
    void Clear();  // ���ҽ� ���� �޼���
}

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern
    public static GameManager Instance;
    #endregion

    #region Dependencies
    public OnPlayerController playerController; // �÷��̾� ��Ʈ�ѷ�
    public PoolManager pool;                    // Ǯ�� �Ŵ���
    private NetworkManager _network = new NetworkManager(); // ��Ʈ��ũ �Ŵ���
    private PacketManager _packet = new PacketManager();    // ��Ŷ �Ŵ���
    private SceneManagerEx _scene = new SceneManagerEx();
    public static NetworkManager Network { get { return Instance._network; } }
    public static PacketManager Packet { get { return Instance._packet; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    #endregion

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // GameManager�� �� ��ȯ �ÿ��� �����ǵ��� ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        pool = GetComponent<PoolManager>(); // PoolManager ������Ʈ �Ҵ�

        // ���� �ʱ�ȭ �޼��� ȣ��
        InitializeGame();

        // ��Ʈ��ũ �Ŵ��� �ʱ�ȭ
        Network.Init();
    }

    private void InitializeGame()
    {
        // �ʱ�ȭ �ܰ� ����
        if (playerController != null)
        {
            // playerController.Initialize(); // �ʿ� �� �÷��̾� �ʱ�ȭ �޼��� ȣ��
        }
        // �߰����� ���� �ʱ�ȭ ���� ���� ����
    }

    private void Update()
    {
        Network.Update();
    }
}
