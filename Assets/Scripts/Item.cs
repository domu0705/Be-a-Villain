using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
    public enum Type { Gun, Sword, Shield };
    public Type type;
    public int price;
    public int value;//공격력,방어력
    public float delay;//무기 재사용까지 걸리는 시간

    [TextArea(3, 5)]
    public string itemName = "";
    [TextArea(3, 5)]
    public string info = "";

    // Outer Functions ------------------------------------------------------------------------------
    public void Use()
    {
        if (type == Type.Sword)
        {
            StopCoroutine("Swing"); // 코루틴을 끝내는 함수. 이전에 진행중인 코루틴과 섞이지 않게하기 위함
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
    public TrailRenderer moveEffect;//무기 휘두를 때 효과

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
