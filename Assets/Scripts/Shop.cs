using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform shopUI;

    public void Enter()
    {
        shopUI.anchoredPosition = Vector3.zero; //UI가 화면 정중앙에 오게 함.

    }

    public void Exit()
    {

        shopUI.anchoredPosition = Vector3.down*1000;
    }
}
