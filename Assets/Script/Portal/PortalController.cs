using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject portalPrefab; // ��Ż ������
    public float portalDuration = 5f; // ��Ż ���� �ð�

    private GameObject currentPortal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentPortal == null)
            {
                // ��Ż ����
                CreatePortal();
            }
            else
            {
                // ��Ż�� �̵�
                Teleport();
            }
        }
    }

    private void CreatePortal()
    {
        Vector3 portalPosition = transform.position + transform.up * 2; // ĳ���� �տ� ����
        currentPortal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
        StartCoroutine(ClosePortalAfterDelay(portalDuration));
    }

    private void Teleport()
    {
        // ��Ż �������� �̵�
        Vector3 teleportDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        teleportDirection.z = 0; // Z���� 0���� ����
        teleportDirection = (teleportDirection - transform.position).normalized * 5; // �Ÿ� ����
        transform.position += teleportDirection;

        Destroy(currentPortal); // ��Ż ����
        currentPortal = null; // ��Ż ���� �ʱ�ȭ
    }

    private IEnumerator ClosePortalAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentPortal != null)
        {
            Destroy(currentPortal);
            currentPortal = null; // ��Ż ���� �ʱ�ȭ
        }
    }
}
