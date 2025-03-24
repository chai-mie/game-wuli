using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecollect : MonoBehaviour
{
   
    public float rotationSpeed = 15f; // 每帧旋转的角度
    private bool isRotatingClockwise = false; // 是否顺时针旋转
    private bool isRotatingCounterClockwise = false; // 是否逆时针旋转

    void Update()
    {
        // 检测按钮1是否被按住
        if (Input.GetKey(KeyCode.Y)) // 假设按钮1是鼠标左键
        {
            isRotatingClockwise = true;
        }
        else
        {
            isRotatingClockwise = false;
        }

        // 检测按钮2是否被按住
        if (Input.GetKey(KeyCode.U)) // 假设按钮2是鼠标右键
        {
            isRotatingCounterClockwise = true;
        }
        else
        {
            isRotatingCounterClockwise = false;
        }

        // 根据按住的按钮执行相应的旋转
        if (isRotatingClockwise)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if (isRotatingCounterClockwise)
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
    }

}
