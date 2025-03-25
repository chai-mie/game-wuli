using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectMovementManager : MonoBehaviour
{
    // 单例模式（网页2、网页3事件管理思路）
    public static ObjectMovementManager Instance;

    [Header("触发条件")]
    public int totalObjects = 8; // 需与场景中实际数量一致
    private int stoppedCount;

    [Header("后续动作")]
    public Transform targetPlane;
    public Vector3 planeTargetPosition;
    public float planeMoveSpeed = 3f;
    public GameObject uiCanvas; // 需在编辑器中绑定Canvas对象

    [Space]
    public UnityEvent OnAllStopped; // 事件驱动（网页4事件系统）

    void Awake() => Instance = this;

    // 由ObjectMovement脚本调用
    public void ReportStopped()
    {
        stoppedCount++;
        if (stoppedCount >= totalObjects)
        {
            StartPlaneMovement();
            OnAllStopped?.Invoke();
        }
    }

    void StartPlaneMovement()
    {
        StartCoroutine(MovePlane());
    }

    IEnumerator MovePlane()
    {
        // 使用插值平滑移动（网页1方法四的优化）
        while (Vector3.Distance(targetPlane.position, planeTargetPosition) > 0.1f)
        {
            targetPlane.position = Vector3.Lerp(
                targetPlane.position,
                planeTargetPosition,
                planeMoveSpeed * Time.deltaTime
            );
            yield return null;
        }
        ShowUI();
    }

    void ShowUI()
    {
        uiCanvas.SetActive(true);
        // 暂停游戏（可选）
        // Time.timeScale = 0;
    }
}
