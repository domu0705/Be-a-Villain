using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Properties -----------------------------------------------------------------------------------
    public enum Type { Gun, Sword, Shield };
    public Type type;
    public int price;
    public int value;//공격력,방어력
    public float delay;//무기 재사용까지 걸리는 시간

    [TextArea(3, 5)]
    public string itemName = "";
    [TextArea(3, 5)]
    public string info = "";

    // Methods --------------------------------------------------------------------------------------
    public void Use()
    {

    }

}
