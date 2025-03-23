using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private static GameObject selectedObject;
    private Color originalColor;
    public float moveSpeed = 5f;
    public Color selectedColor = Color.yellow;

    [Header("ֹͣ����")]
    public Vector3 targetPosition; // ��Inspector�����õ�����
    public float stopDistance = 1f; // ֹͣ������ֵ

    private bool isMovementEnabled = true; // �ƶ�����
    private bool isPermanentlyStopped = false; // ����ֹͣ��־

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    void OnMouseDown()
    {
        if (!isPermanentlyStopped && isMovementEnabled)
        {
            // ȡ��֮ǰѡ�е�����
            if (selectedObject != null)
            {
                selectedObject.GetComponent<Renderer>().material.color =
                    selectedObject.GetComponent<ObjectMovement>().originalColor;
            }

            // ������ѡ������
            selectedObject = gameObject;
            GetComponent<Renderer>().material.color = selectedColor;
        }
    }

    void Update()
    {
        if (isPermanentlyStopped) return;

        // �����Ŀ��λ�õľ���
        CheckProximity();

        if (IsSelected() && isMovementEnabled)
        {
            HandleMovement();
            HandleDeselect();
        }
    }

    bool IsSelected()
    {
        return selectedObject == gameObject;
    }

    void HandleMovement()
    {
        // ���� A/S �����ܣ���ת Horizontal ������
        float xInput = Input.GetAxis("Vertical");      // W/S �� ����X��
        float zInput = -Input.GetAxis("Horizontal");   // A/D �� ��ת����Z��

        Vector3 input = new Vector3(xInput, 0, zInput).normalized;
        Vector3 movement = input * moveSpeed * Time.deltaTime;

        // ȷ��Y��̶�
        Vector3 newPosition = transform.position + movement;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    void HandleDeselect()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Renderer>().material.color = originalColor;
            selectedObject = null;
        }
    }

    void CheckProximity()
    {
        if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
        {
            // ���ý����ƶ�����
            isMovementEnabled = false;
            isPermanentlyStopped = true;

            // ȡ��ѡ��״̬
            if (IsSelected())
            {
                GetComponent<Renderer>().material.color = originalColor;
                selectedObject = null;
            }
        }
    }
}