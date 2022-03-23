using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
    public enum Type { Gun, Sword, Shield };
    public Type type;
    public int price;
    public int value;//���ݷ�,����
    public float delay;//���� ������� �ɸ��� �ð�

    [TextArea(3, 5)]
    public string itemName = "";
    [TextArea(3, 5)]
    public string info = "";

    // Outer Functions ------------------------------------------------------------------------------
    public void Use()
    {
        if (type == Type.Sword)
        {
            StopCoroutine("Swing"); // �ڷ�ƾ�� ������ �Լ�. ������ �������� �ڷ�ƾ�� ������ �ʰ��ϱ� ����
            StartCoroutine("Swing");
        }
        if (type == Type.Gun)
        {
            //StopCoroutine("Shot");
            //StartCoroutine("Shot");
        }
    }

    // Properties : caching -------------------------------------------------------------------------
    private BoxCollider collider;
    // Inner Properties -----------------------------------------------------------------------------


    // Inner Functions ------------------------------------------------------------------------------

    // Coroutine ------------------------------------------------------------------------------------
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        moveEffect.enabled = true;
        yield return new WaitForSeconds(0.3f);
        collider.enabled = true;
        yield return new WaitForSeconds(0.3f); 
        collider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        moveEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.1f);
    }


    // Unity Inspectors -----------------------------------------------------------------------------
    public TrailRenderer moveEffect;//���� �ֵθ� �� ȿ��

    // Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
        if (type == Type.Sword)
        {
            collider = GetComponent<BoxCollider>();



            /*initial setting*/
            collider.enabled = false;
            moveEffect.enabled = false;
        }
    }

	private void Start()
	{

	}
}
