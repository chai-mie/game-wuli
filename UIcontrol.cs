using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
        // ª÷∏¥”Œœ∑£®»Ù‘›Õ££©
        // Time.timeScale = 1; 
    }
}
