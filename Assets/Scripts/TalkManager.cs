using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{

    public int talkIndex;

    // Start is called before the first frame update
    void Start()
    {

    }



    public void Talk(int currentQuestNum, ObjectData obj)//playerMove에서 isAction이 false면 안움직임. 그래서 계속 이야기 할 수 있는 것임.
    {
        int talkIndex = 0;
        string dialogue = obj.GetTalkData(currentQuestNum, talkIndex);
        Debug.Log("대사: "+dialogue);
        if (dialogue == null)//얘기가 더이상 없을 때 (대화가 끝났을 때, 물건 조사가 끝났을 때)
        {
            talkIndex = 0;
            return;
        }
        else
        {
            talkIndex++;

        }
        
    }
}
