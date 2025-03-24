using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RopeLengthController : MonoBehaviour
{
    public Transform ropeStart; // ���ӵ����
    public Transform ropeEnd;   // ���ӵ��յ�
    public float maxLength = 10.0f; // ���ӵ���󳤶�

    private void Update()
    {
        // ����������˵�֮��ľ���
        float currentLength = Vector3.Distance(ropeStart.position, ropeEnd.position);

        // �����ǰ���ȳ�����󳤶ȣ�����е���
        if (currentLength > maxLength)
        {
            // �����������յ�λ�ã�ʹ�������ľ���Ϊ��󳤶�
            Vector3 direction = (ropeEnd.position - ropeStart.position).normalized;
            ropeEnd.position = ropeStart.position + direction * maxLength;
        }
    }
}