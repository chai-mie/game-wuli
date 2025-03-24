using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCauldronInteraction : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerHand;          // 玩家手部锚点
    public float waterLevel = 0.0f;       // 水面高度
    public Vector3 hookedOffset = new Vector3(0, -0.5f, 0); // 钩住时的位置偏移

    [Header("States")]
    [SerializeField] private Rigidbody hookedCauldron; // 当前钩住的大锅
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
        // 获取并验证组件
        hookedCauldron = Cauldron.GetComponent<Rigidbody>();
        if (hookedCauldron == null) return;

        // 建立父子关系
        Cauldron.transform.SetParent(transform);
        Cauldron.transform.localPosition = hookedOffset;
        Cauldron.transform.localRotation = Quaternion.identity;

        // 设置物理状态
        hookedCauldron.isKinematic = true;
        hookedCauldron.interpolation = RigidbodyInterpolation.None;

        isHooked = true;
    }

    void DetachCauldron()
    {
        if (hookedCauldron == null) return;

        // 解除父子关系
        hookedCauldron.transform.SetParent(null);

        // 设置物理属性
        hookedCauldron.isKinematic = false;
        hookedCauldron.interpolation = RigidbodyInterpolation.Interpolate;

        // 传送到玩家手上
        MoveToPlayerHand();

        // 重置状态
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

        // 立即传送到手部位置
        hookedCauldron.transform.position = playerHand.position;
        hookedCauldron.transform.rotation = playerHand.rotation;

        // 设为玩家手的子对象
        hookedCauldron.transform.SetParent(playerHand);

        // 防止物理干扰
        hookedCauldron.isKinematic = true;
    }

    // 调试可视化
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, waterLevel, 0), new Vector3(10, 0.1f, 10));
    }
}