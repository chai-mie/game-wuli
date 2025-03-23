using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private static GameObject selectedObject;
    private Color originalColor;
    public float moveSpeed = 5f;
    public Color selectedColor = Color.yellow;

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    void OnMouseDown()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material.color =
                selectedObject.GetComponent<ObjectMovement>().originalColor;
        }
        selectedObject = gameObject;
        GetComponent<Renderer>().material.color = selectedColor;
    }

    void Update()
    {
        if (IsSelected())
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
}