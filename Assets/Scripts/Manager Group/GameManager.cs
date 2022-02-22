using BAV.Common;

public class GameManager : Singleton<GameManager>
{
	// Methods --------------------------------------------------------------------------------------
	public void Action(ObjectData obj)
	{
		player.Movement.Stop();
		talkManager.Talk(questManager.curQuestNum, obj);
	}



	// Fields : caching -----------------------------------------------------------------------------
	private Player player;



	// Unity Inspectors -----------------------------------------------------------------------------
	public TalkManager talkManager;
	public QuestManager questManager;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		// player가 1개이고, GameManager 생성전에 이미 존재할 경우만 가능
		// 다른 대안으로 PlayerManager를 따로 둘 수도 있고, GameManager가 생성시킬 수도 있음
		// 2022.02.22 by veramocor
		player = FindObjectOfType<Player>();
	}
}
