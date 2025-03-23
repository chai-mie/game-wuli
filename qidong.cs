using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.F;
    public KeyCode returnKey = KeyCode.E;  // �������ذ���

    [Header("References")]
    public GameObject qidongObject;
    public GameObject promptUI;
    public Camera mainCamera;       // ��ӦCamera1�������ӽǣ�
    public Camera targetCamera;     // ��ӦCamera2���̶��ӽǣ�
    public ThirdPersonController playerController;

    private bool isInteracting = false;

    void Update()
    {
        if (qidongObject == null) return;

        float distance = Vector3.Distance(transform.position, qidongObject.transform.position);

        // ����״̬�߼�
        if (!isInteracting)
        {
            // �ӽ��ɽ�������
            if (distance <= interactDistance)
            {
                promptUI.SetActive(true);
                if (Input.GetKeyDown(interactKey))
                {
                    StartInteraction();
                }
            }
            else
            {
                promptUI.SetActive(false);
            }
        }
        else  // ���ڽ���״̬ʱ
        {
            // ��ⷵ�ذ���
            if (Input.GetKeyDown(returnKey))
            {
                ReturnToPlayer();
            }
        }
    }

    // �޸ĺ�Ĵ���Ƭ��
    void StartInteraction()
    {
        isInteracting = true;

        // �л������
        mainCamera.gameObject.SetActive(false);
        targetCamera.gameObject.SetActive(true);

        // ȡ����ѡ��ҿ����� (��ӦInspector����еĸ�ѡ��)
        playerController.enabled = false;
        Debug.Log("��ȡ����ѡ��ҿ�����"); // ������־

        promptUI.SetActive(false);
    }

    void ReturnToPlayer()
    {
        isInteracting = false;

        targetCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        // ���¹�ѡ��ҿ����� (��ӦInspector��ѡ���)
        playerController.enabled = true;
        Debug.Log("�����¹�ѡ��ҿ�����"); // ������־
    }
}