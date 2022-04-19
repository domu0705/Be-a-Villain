using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class TalkManager : Singleton<TalkManager>
{
	// ----------------------------------------------------------------------------------------------

	// FSM ------------------------------------------------------------------------------------------

	// Definitions ----------------------------------------------------------------------------------
	// Outer Properties -----------------------------------------------------------------------------

	// Outer Functions ------------------------------------------------------------------------------
	public void Talk(int currentQuestNum, ObjectData obj)
	{
		Player Player = GameManager.Instance.Player;
		TalkUI TalkUI = TalkUI.Instance;

		Debug.Log("Talk Manager | Talk()");
		//playerMove에서 isAction이 false면 안움직임. 그래서 계속 이야기 할 수 있는 것임.
		//string dialogue = obj.GetTalkData(currentQuestNum, talkIndex);
		string dialogue = TalkData.Instance.GetTalkData(currentQuestNum, obj.npcNum, talkIndex);

		if (dialogue == null)//얘기가 더이상 없을 때 (대화가 끝났을 때, 물건 조사가 끝났을 때)
		{
			Debug.Log("dialogue == null");
			TalkUI.HidePanel();

			talkIndex = 0;
			Player.Movement.canMove = true;
			return;
		}

		talkIndex++;

		TalkUI.SetTalkText(dialogue);
		TalkUI.ShowPanel();
	}
	// Events ---------------------------------------------------------------------------------------



	// Fields : caching -----------------------------------------------------------------------------

	// Fields ---------------------------------------------------------------------------------------
	// Functions ------------------------------------------------------------------------------------


	// Unity Inspectors -----------------------------------------------------------------------------
	[Header("Talk Info")]
	[SerializeField] private int talkIndex = 0;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		//Debug.Log("talk manager Awake()");
	}

	private void Start()
	{
		
	}

	// Unity Coroutine ------------------------------------------------------------------------------
}
