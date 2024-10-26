using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ
    private bool isPortalActive = false; // ��Ż Ȱ��ȭ ����

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
            // ��Ż�� Ȱ��ȭ�� �� �÷��̾��� ��ġ�� ��Ż ����
            player.transform.position = transform.position + transform.up * 2; // �÷��̾� �������� �ణ ���� ����
            gameObject.SetActive(true); // ��Ż Ȱ��ȭ
            isPortalActive = true; // ��Ż Ȱ��ȭ ����
        }
    }

    private void HandlePortalActivation()
    {
        // ���콺 Ŭ�� �� �̵�
        if (Input.GetMouseButtonDown(0))
        {
            MovePlayerToMouse();
        }
    }

    private void MovePlayerToMouse()
    {
        // ���콺 ��ġ�� ȭ�鿡�� ���� ��ǥ�� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // �̵��� ��ġ ���
            Vector3 targetPosition = hit.point;

            // �÷��̾�� ��ǥ ��ġ ���� �Ÿ� ���
            float distance = Vector3.Distance(player.transform.position, targetPosition);

            // �̵� �Ÿ��� 3���� ����
            if (distance > 3f)
            {
                // ��ǥ ��ġ�� �÷��̾�� 3�� �Ÿ��� ����
                targetPosition = player.transform.position + (targetPosition - player.transform.position).normalized * 3f;
            }

            // �÷��̾� �̵�
            player.transform.position = targetPosition;

            // ��Ż ��Ȱ��ȭ
            isPortalActive = false;
            gameObject.SetActive(false);
        }
    }
}

