using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject portal = GameManager.Instance.pool.Get(1); // ��Ż �ε����� �°� ����

            if (portal != null)
            {
                // ��Ż ��ġ ����
                portal.transform.position = transform.position + transform.up * 2; // ĳ���� �տ� ����
            
            }
            else
            {
                Debug.LogError("Failed to get portal object from pool.");
            }
        }
    }

}