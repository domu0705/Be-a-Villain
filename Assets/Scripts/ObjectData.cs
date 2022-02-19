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
        talkData.Add(0, new string[] {"아~ 누가 공주 역할로 날 캐스팅한다면 좋을텐데...", "용사님이 나타날 것만 같은 날이야~"});
        talkData.Add(1, new string[] { "이거 퀘스트 1번 대산데..", "퉤스트 1번 대사 두번째야.." });
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
