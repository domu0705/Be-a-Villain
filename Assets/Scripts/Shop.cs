using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform shopUI;

    public void Enter()
    {
        shopUI.anchoredPosition = Vector3.zero; //UI�� ȭ�� ���߾ӿ� ���� ��.

    }

    public void Exit()
    {

        shopUI.anchoredPosition = Vector3.down*1000;
    }
}
