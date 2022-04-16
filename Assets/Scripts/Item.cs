using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
    public enum Type { Gun, Sword, Shield };
    public Type type;
    public int price;
    public int damage;//���ݷ�,����
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

        //�Ѿ� 
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
            transform.localRotation = Quaternion.Slerp(destRot, startRot, (elapsedTime / delay));//dest�� �����̵��ϰ� õõ�� ����ġ�� ���ƿ���
            yield return new WaitForEndOfFrame();
        }

        transform.localRotation = startRot; ; //�ð����� ��ǥ���⿡ �������� ���ϸ� �ٷ� �ٲ��ֱ�
    }

    // Unity Inspectors -----------------------------------------------------------------------------
    public TrailRenderer moveEffect;//���� �ֵθ� �� ȿ��
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
