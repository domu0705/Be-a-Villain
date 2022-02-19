using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TalkManager talkManager;
    public QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Action(ObjectData obj)
    {
        talkManager.Talk(questManager.curQuestNum, obj);

    }
}
