using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    // 新增颜色变量声明（放在类顶部变量区）
    [Header("停止效果")]
    public Color stoppedColor = Color.black; // 必须与下方代码中使用的名称完全一致

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

            // 强制设置黑色（无论是否选中）
            GetComponent<Renderer>().material.color = stoppedColor;

            // 清除选中状态
            if (IsSelected())
            {
                selectedObject = null;
            }
        }
    }
}