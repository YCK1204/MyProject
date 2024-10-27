using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject portal = GameManager.Instance.pool.Get(1); // 포탈 인덱스에 맞게 변경

            if (portal != null)
            {
                // 포탈 위치 설정
                portal.transform.position = transform.position + transform.up * 2; // 캐릭터 앞에 생성
            
            }
            else
            {
                Debug.LogError("Failed to get portal object from pool.");
            }
        }
    }

}