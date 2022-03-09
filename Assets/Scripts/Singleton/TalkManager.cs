using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class TalkManager : Singleton<TalkManager>
{
	// ----------------------------------------------------------------------------------------------

	// FSM ------------------------------------------------------------------------------------------

	// Definitions ----------------------------------------------------------------------------------
	// Properties -----------------------------------------------------------------------------------
	// Methods --------------------------------------------------------------------------------------
	public void Talk(int currentQuestNum, ObjectData obj)
	{
		//playerMove에서 isAction이 false면 안움직임. 그래서 계속 이야기 할 수 있는 것임.
		string dialogue = obj.GetTalkData(currentQuestNum, talkIndex);

		if (dialogue == null)//얘기가 더이상 없을 때 (대화가 끝났을 때, 물건 조사가 끝났을 때)
		{
			TalkUI.HidePanel();

			talkIndex = 0;
			player.Movement.canMove = true;
			return;
		}

		talkIndex++;

		TalkUI.SetTalkText(dialogue);
		TalkUI.ShowPanel();
	}
	// Events ---------------------------------------------------------------------------------------



	// Fields : caching -----------------------------------------------------------------------------
	Player player;
	TalkUI TalkUI;

	// Fields ---------------------------------------------------------------------------------------
	// Functions ------------------------------------------------------------------------------------


	// Unity Inspectors -----------------------------------------------------------------------------
	[Header("Talk Info")]
	[SerializeField] private int talkIndex = 0;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		player = GameManager.Instance.Player;
		TalkUI = TalkUI.Instance;
	}

	private void Start()
	{
		
	}

	// Unity Coroutine ------------------------------------------------------------------------------
}
