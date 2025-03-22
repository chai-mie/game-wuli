using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private static GameObject selectedObject;
    private Color originalColor;
    public float moveSpeed = 5f;
    public Color selectedColor = Color.yellow;

    [Header("停止条件")]
    public Vector3 targetPosition; // 在Inspector中设置的坐标
    public float stopDistance = 1f; // 停止距离阈值

    private bool isMovementEnabled = true; // 移动开关
    private bool isPermanentlyStopped = false; // 永久停止标志

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    void OnMouseDown()
    {
        if (!isPermanentlyStopped && isMovementEnabled)
        {
            // 取消之前选中的物体
            if (selectedObject != null)
            {
                selectedObject.GetComponent<Renderer>().material.color =
                    selectedObject.GetComponent<ObjectMovement>().originalColor;
            }

            // 设置新选中物体
            selectedObject = gameObject;
            GetComponent<Renderer>().material.color = selectedColor;
        }
    }

    void Update()
    {
        if (isPermanentlyStopped) return;

        // 检测与目标位置的距离
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
        // 交换 A/S 键功能：反转 Horizontal 轴输入
        float xInput = Input.GetAxis("Vertical");      // W/S → 控制X轴
        float zInput = -Input.GetAxis("Horizontal");   // A/D → 反转控制Z轴

        Vector3 input = new Vector3(xInput, 0, zInput).normalized;
        Vector3 movement = input * moveSpeed * Time.deltaTime;

        // 确保Y轴固定
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
            // 永久禁用移动功能
            isMovementEnabled = false;
            isPermanentlyStopped = true;

            // 取消选中状态
            if (IsSelected())
            {
                GetComponent<Renderer>().material.color = originalColor;
                selectedObject = null;
            }
        }
    }
}