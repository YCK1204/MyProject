using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        CreatePortal();
    }


    private void CreatePortal()
    {


        if (player != null)
        {
            // 플레이어의 위치에서 포탈 생성
            player.transform.position = transform.position + transform.up * 2; // 플레이어 앞쪽으로 약간 위에 생성
            gameObject.SetActive(true); // 포탈 활성화


        }
    }
}


