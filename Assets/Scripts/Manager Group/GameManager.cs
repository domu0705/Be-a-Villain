using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public PlayerMovement playerMovement;
    public TalkManager talkManager;
    public QuestManager questManager;

    public void Action(ObjectData obj)
    {
        playerMovement.Stop();
        talkManager.Talk(questManager.curQuestNum, obj);

    }

}
 