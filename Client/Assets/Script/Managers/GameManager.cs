using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    void Init();   // 초기화 메서드
    void Clear();  // 리소스 정리 메서드
}

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern
    public static GameManager Instance;
    #endregion

    #region Dependencies
    public OnPlayerController playerController; // 플레이어 컨트롤러
    public PoolManager pool;                    // 풀링 매니저
    private NetworkManager _network = new NetworkManager(); // 네트워크 매니저
    private PacketManager _packet = new PacketManager();    // 패킷 매니저
    private SceneManagerEx _scene = new SceneManagerEx();
    public static NetworkManager Network { get { return Instance._network; } }
    public static PacketManager Packet { get { return Instance._packet; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    #endregion

    private void Awake()
    {
        // 싱글턴 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // GameManager가 씬 전환 시에도 유지되도록 설정
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        pool = GetComponent<PoolManager>(); // PoolManager 컴포넌트 할당

        // 게임 초기화 메서드 호출
        InitializeGame();

        // 네트워크 매니저 초기화
        Network.Init();
    }

    private void InitializeGame()
    {
        // 초기화 단계 예시
        if (playerController != null)
        {
            // playerController.Initialize(); // 필요 시 플레이어 초기화 메서드 호출
        }
        // 추가적인 게임 초기화 로직 구현 가능
    }

    private void Update()
    {
        Network.Update();
    }
}
