using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public OnPlayerController player;
    public static GameManager Instance;
    public PoolManager pool;

    private void Awake()
    {
        Instance = this;
        pool = GetComponent<PoolManager>();

        InitializeGame();
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
}

