using BAV.Common;

public class GameManager : Singleton<GameManager>
{
	// Methods --------------------------------------------------------------------------------------
	public void Action(ObjectData obj)
	{
		player.Movement.Stop();
		//talkManager.Talk(questManager.curQuestNum, obj);
		TalkUI.Instance.Talk(questManager.curQuestNum, obj);
	}



	// Fields : caching -----------------------------------------------------------------------------
	private Player player;

	// Event Handlers -------------------------------------------------------------------------------
	private void Player_OnInteraction(Player player, ObjectData obj)
	{
		player.Movement.Stop();
		TalkUI.Instance.Talk(questManager.curQuestNum, obj);
	}


	// Unity Inspectors -----------------------------------------------------------------------------
	public QuestManager questManager;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		// player�� 1���̰�, GameManager �������� �̹� ������ ��츸 ����
		// �ٸ� ������� PlayerManager�� ���� �� ���� �ְ�, GameManager�� ������ų ���� ����
		// 2022.02.22 by veramocor
		player = FindObjectOfType<Player>();
	}
	private void OnEnable()
	{
		player.OnInteraction += Player_OnInteraction;
	}
	private void OnDisable()
	{
		player.OnInteraction -= Player_OnInteraction;
	}

	
}
