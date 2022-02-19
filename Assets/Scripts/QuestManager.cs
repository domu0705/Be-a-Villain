using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    public int curQuestNum;
    public int maxQuestNum = 10;
    public int semiQuestIndex = 0; //한 퀘스트 내부에서 이벤트가 어디까지 진행됐는지를 마크함.
    public int questCushion = 10000;//퀘스트와 세미 퀘스트 값이 만나는 것을 막기 위해 퀘스트에 덧붙이는 임의의 쿠션 값

    public void GetCurQuestIndex()
    {

    }
}
