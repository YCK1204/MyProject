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
            // �÷��̾��� ��ġ���� ��Ż ����
            player.transform.position = transform.position + transform.up * 2; // �÷��̾� �������� �ణ ���� ����
            gameObject.SetActive(true); // ��Ż Ȱ��ȭ


        }
    }
}


