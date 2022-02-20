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
        GeneratePortraitData();
    }

    void GenerateTalkData()
    {
        talkData.Add(0, new string[] {"green npc 퀘스트 0번 대사야~~", "퀘스트 0번의 두번째 대사야~" });
        talkData.Add(1, new string[] { "이거 퀘스트 1번 대산데..", "퉤스트 1번 대사 두번째야!" });
    }

    void GeneratePortraitData()
    {

    }

    public string GetTalkData(int questNum,int talkIndex)
    {
        Debug.Log("GetTalkData() | questNum=" + questNum + " talkIndex=" + talkIndex);

        if (talkIndex < talkData[questNum].Length)
            return talkData[questNum][talkIndex];
        else
            return null;
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
