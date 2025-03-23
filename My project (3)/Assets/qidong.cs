using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.F;
    public KeyCode returnKey = KeyCode.E;  // 新增返回按键

    [Header("References")]
    public GameObject qidongObject;
    public GameObject promptUI;
    public Camera mainCamera;       // 对应Camera1（跟随视角）
    public Camera targetCamera;     // 对应Camera2（固定视角）
    public ThirdPersonController playerController;

    private bool isInteracting = false;

    void Update()
    {
        if (qidongObject == null) return;

        float distance = Vector3.Distance(transform.position, qidongObject.transform.position);

        // 交互状态逻辑
        if (!isInteracting)
        {
            // 接近可交互物体
            if (distance <= interactDistance)
            {
                promptUI.SetActive(true);
                if (Input.GetKeyDown(interactKey))
                {
                    StartInteraction();
                }
            }
            else
            {
                promptUI.SetActive(false);
            }
        }
        else  // 处于交互状态时
        {
            // 检测返回按键
            if (Input.GetKeyDown(returnKey))
            {
                ReturnToPlayer();
            }
        }
    }

    // 修改后的代码片段
    void StartInteraction()
    {
        isInteracting = true;

        // 切换摄像机
        mainCamera.gameObject.SetActive(false);
        targetCamera.gameObject.SetActive(true);

        // 取消勾选玩家控制器 (对应Inspector面板中的复选框)
        playerController.enabled = false;
        Debug.Log("已取消勾选玩家控制器"); // 调试日志

        promptUI.SetActive(false);
    }

    void ReturnToPlayer()
    {
        isInteracting = false;

        targetCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        // 重新勾选玩家控制器 (对应Inspector复选框打钩)
        playerController.enabled = true;
        Debug.Log("已重新勾选玩家控制器"); // 调试日志
    }
}