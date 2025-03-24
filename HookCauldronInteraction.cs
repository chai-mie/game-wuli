using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCauldronInteraction : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerHand;          // ����ֲ�ê��
    public float waterLevel = 0.0f;       // ˮ��߶�
    public Vector3 hookedOffset = new Vector3(0, -0.5f, 0); // ��סʱ��λ��ƫ��

    [Header("States")]
    [SerializeField] private Rigidbody hookedCauldron; // ��ǰ��ס�Ĵ��
    [SerializeField] private bool isHooked;

    void Update()
    {
        if (isHooked)
        {
            CheckWaterLevel();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isHooked && collision.gameObject.CompareTag("Cauldron"))
        {
            AttachCauldron(collision.gameObject);
        }
    }

    void AttachCauldron(GameObject Cauldron)
    {
        // ��ȡ����֤���
        hookedCauldron = Cauldron.GetComponent<Rigidbody>();
        if (hookedCauldron == null) return;

        // �������ӹ�ϵ
        Cauldron.transform.SetParent(transform);
        Cauldron.transform.localPosition = hookedOffset;
        Cauldron.transform.localRotation = Quaternion.identity;

        // ��������״̬
        hookedCauldron.isKinematic = true;
        hookedCauldron.interpolation = RigidbodyInterpolation.None;

        isHooked = true;
    }

    void DetachCauldron()
    {
        if (hookedCauldron == null) return;

        // ������ӹ�ϵ
        hookedCauldron.transform.SetParent(null);

        // ������������
        hookedCauldron.isKinematic = false;
        hookedCauldron.interpolation = RigidbodyInterpolation.Interpolate;

        // ���͵��������
        MoveToPlayerHand();

        // ����״̬
        hookedCauldron = null;
        isHooked = false;
    }

    void CheckWaterLevel()
    {
        if (hookedCauldron.transform.position.y > waterLevel)
        {
            DetachCauldron();
        }
    }

    void MoveToPlayerHand()
    {
        if (playerHand == null) return;

        // �������͵��ֲ�λ��
        hookedCauldron.transform.position = playerHand.position;
        hookedCauldron.transform.rotation = playerHand.rotation;

        // ��Ϊ����ֵ��Ӷ���
        hookedCauldron.transform.SetParent(playerHand);

        // ��ֹ�������
        hookedCauldron.isKinematic = true;
    }

    // ���Կ��ӻ�
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, waterLevel, 0), new Vector3(10, 0.1f, 10));
    }
}