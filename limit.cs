using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RopeLengthController : MonoBehaviour
{
    public Transform ropeStart; // 绳子的起点
    public Transform ropeEnd;   // 绳子的终点
    public float maxLength = 10.0f; // 绳子的最大长度

    private void Update()
    {
        // 检测绳子两端点之间的距离
        float currentLength = Vector3.Distance(ropeStart.position, ropeEnd.position);

        // 如果当前长度超过最大长度，则进行调整
        if (currentLength > maxLength)
        {
            // 计算调整后的终点位置，使其与起点的距离为最大长度
            Vector3 direction = (ropeEnd.position - ropeStart.position).normalized;
            ropeEnd.position = ropeStart.position + direction * maxLength;
        }
    }
}