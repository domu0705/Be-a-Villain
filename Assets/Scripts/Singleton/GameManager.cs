using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// Properties -----------------------------------------------------------------------------------
	public Player Player => player;
	public HudUI HUDUI => hudUI;
	// Methods --------------------------------------------------------------------------------------
	public void Action(ObjectData obj)
	{
		player.Movement.Stop();
		TalkManager.Instance.Talk(questManager.curQuestNum, obj);
	}



	// Fields : caching -----------------------------------------------------------------------------
	private Player player;
	private HudUI hudUI;
	// Fields ---------------------------------------------------------------------------------------

	// Event Handlers -------------------------------------------------------------------------------
	private void Player_OnInteraction(Player player, ObjectData obj)
	{
		player.Movement.Stop();
		TalkManager.Instance.Talk(questManager.curQuestNum, obj);
	}


	// Unity Inspectors -----------------------------------------------------------------------------
	public QuestManager questManager;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		player = FindObjectOfType<Player>();
	}

	

	private void OnEnable()//오브젝트가 활성화될 경우 자동으로 호출
	{
		player.OnInteraction += Player_OnInteraction;//+= 로 이벤트 핸들러를 이벤트에 추가.  (Player_OnInteraction() 이 실행되는 것은 아님)
	}

	private void OnDisable()//오브젝트가 비활성화될 경우 자동으로 호출
	{
		player.OnInteraction -= Player_OnInteraction;//-= 러 이벤트 핸들러를 이벤트에 추가
	}

	
}
