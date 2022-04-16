using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
    public enum Type { Gun, Sword, Shield };
    public Type type;
    public int price;
    public int damage;//공격력,방어력
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
            StartCoroutine("Shot");
        }
    }

    // Properties : caching -------------------------------------------------------------------------
    private BoxCollider collider;

    // Inner Properties -----------------------------------------------------------------------------
    [SerializeField] Quaternion startRot;
    [SerializeField] Quaternion destRot;
    Quaternion gunGradient;

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
        StartCoroutine("GunAnim");

        //총알 
        Bullet bullet = ObjectManager.Instance.GetBullet();
        bullet.ShootBullet();
        yield return null;
    }

    IEnumerator GunAnim()
    {
        float elapsedTime = 0.0f;
        destRot = transform.localRotation * gunGradient;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime; // <- move elapsedTime increment here
            // Rotations
            transform.localRotation = Quaternion.Slerp(destRot, startRot, (elapsedTime / delay));//dest로 순간이동하고 천천히 원위치로 돌아오기
            yield return new WaitForEndOfFrame();
        }

        transform.localRotation = startRot; ; //시간내에 목표기울기에 도달하지 못하면 바로 바꿔주기
    }

    // Unity Inspectors -----------------------------------------------------------------------------
    public TrailRenderer moveEffect;//무기 휘두를 때 효과
    public Transform bulletPos;

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

        gunGradient = Quaternion.Euler(new Vector3(-25, 0, -10));
        startRot = transform.localRotation;
    }

	private void Start()
	{

	}

    private void Update()
    {
        if (type == Type.Gun)
        {
            Debug.Log("transform.localRotation=" + transform.localRotation);
        }
    }
}
