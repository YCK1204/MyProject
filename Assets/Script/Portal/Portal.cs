using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트
    private bool isPortalActive = false; // 포탈 활성화 여부

    void Update()
    {
        if (isPortalActive)
        {
            HandlePortalActivation();
        }
    }

    private void CreatePortal()
    {
        if (player != null)
        {
            // 포탈이 활성화될 때 플레이어의 위치에 포탈 생성
            player.transform.position = transform.position + transform.up * 2; // 플레이어 앞쪽으로 약간 위에 생성
            gameObject.SetActive(true); // 포탈 활성화
            isPortalActive = true; // 포탈 활성화 상태
        }
    }

    private void HandlePortalActivation()
    {
        // 마우스 클릭 시 이동
        if (Input.GetMouseButtonDown(0))
        {
            MovePlayerToMouse();
        }
    }

    private void MovePlayerToMouse()
    {
        // 마우스 위치를 화면에서 월드 좌표로 변환
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 이동할 위치 계산
            Vector3 targetPosition = hit.point;

            // 플레이어와 목표 위치 간의 거리 계산
            float distance = Vector3.Distance(player.transform.position, targetPosition);

            // 이동 거리를 3으로 제한
            if (distance > 3f)
            {
                // 목표 위치를 플레이어와 3의 거리로 설정
                targetPosition = player.transform.position + (targetPosition - player.transform.position).normalized * 3f;
            }

            // 플레이어 이동
            player.transform.position = targetPosition;

            // 포탈 비활성화
            isPortalActive = false;
            gameObject.SetActive(false);
        }
    }
}

