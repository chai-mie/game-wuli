using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
        // �ָ���Ϸ������ͣ��
        // Time.timeScale = 1; 
    }
}
