using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Properties -----------------------------------------------------------------------------------
    public enum Type { Gun, Sword, Shield };
    public Type type;
    public int price;
    public int value;//���ݷ�,����
    public float delay;//���� ������� �ɸ��� �ð�

    [TextArea(3, 5)]
    public string itemName = "";
    [TextArea(3, 5)]
    public string info = "";

    // Methods --------------------------------------------------------------------------------------
    public void Use()
    {

    }

}
