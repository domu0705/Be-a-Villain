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



    public void Talk(int currentQuestNum, ObjectData obj)//playerMove���� isAction�� false�� �ȿ�����. �׷��� ��� �̾߱� �� �� �ִ� ����.
    {
        int talkIndex = 0;
        string dialogue = obj.GetTalkData(currentQuestNum, talkIndex);
        Debug.Log("���: "+dialogue);
        if (dialogue == null)//��Ⱑ ���̻� ���� �� (��ȭ�� ������ ��, ���� ���簡 ������ ��)
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
