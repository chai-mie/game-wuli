using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecollect : MonoBehaviour
{
   
    public float rotationSpeed = 15f; // ÿ֡��ת�ĽǶ�
    private bool isRotatingClockwise = false; // �Ƿ�˳ʱ����ת
    private bool isRotatingCounterClockwise = false; // �Ƿ���ʱ����ת

    void Update()
    {
        // ��ⰴť1�Ƿ񱻰�ס
        if (Input.GetKey(KeyCode.Y)) // ���谴ť1��������
        {
            isRotatingClockwise = true;
        }
        else
        {
            isRotatingClockwise = false;
        }

        // ��ⰴť2�Ƿ񱻰�ס
        if (Input.GetKey(KeyCode.U)) // ���谴ť2������Ҽ�
        {
            isRotatingCounterClockwise = true;
        }
        else
        {
            isRotatingCounterClockwise = false;
        }

        // ���ݰ�ס�İ�ťִ����Ӧ����ת
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
