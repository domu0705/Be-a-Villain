using System.Collections;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
    public int npcNum;
    
    // Outer Functions ------------------------------------------------------------------------------
    // Events ---------------------------------------------------------------------------------------



    // Properties : caching -------------------------------------------------------------------------
    // Inner Properties -----------------------------------------------------------------------------
    [SerializeField] private string objName;
    [SerializeField] private bool isNpc;
    [SerializeField] private float lookSpeed = 3;
    // Inner Functions ------------------------------------------------------------------------------
    // Coroutine ------------------------------------------------------------------------------------
    // Event Handlers -------------------------------------------------------------------------------
    // Overrides ------------------------------------------------------------------------------------



    // Unity Inspectors -----------------------------------------------------------------------------


    // Unity Messages -------------------------------------------------------------------------------

    private void Awake()
    {

    }


    void GeneratePortraitData()
    {

    }

    void lookAt(GameObject obj)
    {
            Vector3 lookVec = obj.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(lookVec), Time.deltaTime * lookSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && isNpc)
        {
            lookAt(other.gameObject);
        }

    }
}
