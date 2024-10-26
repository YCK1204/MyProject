using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject portalPrefab; // 포탈 프리팹
    public float portalDuration = 5f; // 포탈 지속 시간

    private GameObject currentPortal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentPortal == null)
            {
                // 포탈 생성
                CreatePortal();
            }
            else
            {
                // 포탈로 이동
                Teleport();
            }
        }
    }

    private void CreatePortal()
    {
        Vector3 portalPosition = transform.position + transform.up * 2; // 캐릭터 앞에 생성
        currentPortal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
        StartCoroutine(ClosePortalAfterDelay(portalDuration));
    }

    private void Teleport()
    {
        // 포탈 방향으로 이동
        Vector3 teleportDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        teleportDirection.z = 0; // Z축을 0으로 설정
        teleportDirection = (teleportDirection - transform.position).normalized * 5; // 거리 조절
        transform.position += teleportDirection;

        Destroy(currentPortal); // 포탈 삭제
        currentPortal = null; // 포탈 상태 초기화
    }

    private IEnumerator ClosePortalAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentPortal != null)
        {
            Destroy(currentPortal);
            currentPortal = null; // 포탈 상태 초기화
        }
    }
}
