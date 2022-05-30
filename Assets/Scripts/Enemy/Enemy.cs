using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Definitions ----------------------------------------------------------------------------------
    // Outer Properties -----------------------------------------------------------------------------
    public int damage;

    // Outer Functions ------------------------------------------------------------------------------
    // Events ---------------------------------------------------------------------------------------



    // Properties : caching -------------------------------------------------------------------------
    protected Animator anim;

    // Inner Properties -----------------------------------------------------------------------------
    [SerializeField] private int health;
    [SerializeField] protected float attackDelay;
    private NavMeshAgent nav;
    private Rigidbody rigid;
    protected bool isChasing;
    protected bool isAttacking;
    private bool isDead;

    [SerializeField] private float targetRadius;
    [SerializeField] private float targetRange;

    // Inner Functions ------------------------------------------------------------------------------
    private void getDamage(int damage)
    {

        if (damage >= health)
        {
            StartCoroutine("die");
            die();
        }
        else
        {
            anim.SetTrigger("doAttacked");
            health -= damage;
        }
    }

    private void chase()
    {
        if (isDead || isAttacking) return;

        Debug.Log("chase");
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, transform.forward * targetRange, Color.green);//이작위치,쏘는방향*ray길이, ray색
        if (rayHits.Length > 0)
        {
            StartCoroutine(attack());
        }
    }
    private void deleteExternalForce()//외부의 물리적 힘에 의해 AI의 이동이 망가지지 않도록 외부 힘 제거.
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void startChase()
    {
        isChasing = true;
        anim.SetBool("isWalking", true);
    }

    // Coroutine ------------------------------------------------------------------------------------
    IEnumerator die()
    {
        isDead = true;
        anim.SetTrigger("doDie");
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }

    // Event Handlers -------------------------------------------------------------------------------
    // Overrides ------------------------------------------------------------------------------------
    protected virtual IEnumerator attack()
    {
        yield return new WaitForEndOfFrame();
    }


    // Unity Inspectors -----------------------------------------------------------------------------
    [SerializeField] protected GameObject attackArea;

    // Unity Messages -------------------------------------------------------------------------------
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        startChase();
    }
    private void Update()
    {
        nav.SetDestination(GameManager.Instance.Player.transform.position);
        nav.isStopped = !isChasing;
    }
    private void FixedUpdate()
    {
        deleteExternalForce();
        chase();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            getDamage(bullet.damage);
        }
        else if (other.tag == "Weapon")
        {
            Item weapon = other.GetComponent<Item>();
            getDamage(weapon.damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
