using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Health, Sword, Shield };
    public Type type;

    public int price;
    public int value;
    [TextArea(3, 5)]
    public string itemName = "";
    [TextArea(3,5)]
    public string info = "";
}
