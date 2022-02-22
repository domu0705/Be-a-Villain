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
		// player�� 1���̰�, GameManager �������� �̹� ������ ��츸 ����
		// �ٸ� ������� PlayerManager�� ���� �� ���� �ְ�, GameManager�� ������ų ���� ����
		// 2022.02.22 by veramocor
		player = FindObjectOfType<Player>();
	}
}
