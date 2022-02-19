using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public bool isNpc;
    public float lookSpeed = 3;
    [SerializeField]
    Dictionary<int, string[]> talkData; //int:quest num, string:dialogue
    
    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();

        GenerateTalkData();
    }

    void GenerateTalkData()
    {
        talkData.Add(0, new string[] {"��~ ���� ���� ���ҷ� �� ĳ�����Ѵٸ� �����ٵ�...", "������ ��Ÿ�� �͸� ���� ���̾�~"});
        talkData.Add(1, new string[] { "�̰� ����Ʈ 1�� ��굥..", "ơ��Ʈ 1�� ��� �ι�°��.." });
    }

    public string GetTalkData(int questNum,int talkIndex)
    {
        return talkData[questNum][talkIndex];
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
