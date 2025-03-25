using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectMovementManager : MonoBehaviour
{
    // ����ģʽ����ҳ2����ҳ3�¼�����˼·��
    public static ObjectMovementManager Instance;

    [Header("��������")]
    public int totalObjects = 8; // ���볡����ʵ������һ��
    private int stoppedCount;

    [Header("��������")]
    public Transform targetPlane;
    public Vector3 planeTargetPosition;
    public float planeMoveSpeed = 3f;
    public GameObject uiCanvas; // ���ڱ༭���а�Canvas����

    [Space]
    public UnityEvent OnAllStopped; // �¼���������ҳ4�¼�ϵͳ��

    void Awake() => Instance = this;

    // ��ObjectMovement�ű�����
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
        // ʹ�ò�ֵƽ���ƶ�����ҳ1�����ĵ��Ż���
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
        // ��ͣ��Ϸ����ѡ��
        // Time.timeScale = 0;
    }
}
