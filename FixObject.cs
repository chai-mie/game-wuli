using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FixObject : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // ���ø���Ϊ��ֹ״̬
            rb.isKinematic = true;
        }
    }
}