using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// Properties -----------------------------------------------------------------------------------
	public Player Player => player;

	// Methods --------------------------------------------------------------------------------------
	public void Action(ObjectData obj)
	{
		player.Movement.Stop();
		TalkUI.Instance.Talk(questManager.curQuestNum, obj);
	}



	// Fields : caching -----------------------------------------------------------------------------
	private Player player;

	// Event Handlers -------------------------------------------------------------------------------
	private void Player_OnInteraction(Player player, ObjectData obj)
	{
		Debug.Log("Player_OnInteraction");
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

	private void OnEnable()//������Ʈ�� Ȱ��ȭ�� ��� �ڵ����� ȣ��
	{
		player.OnInteraction += Player_OnInteraction;//+= �� �̺�Ʈ �ڵ鷯�� �̺�Ʈ�� �߰�.  (Player_OnInteraction() �� ����Ǵ� ���� �ƴ�)
	}

	private void OnDisable()//������Ʈ�� ��Ȱ��ȭ�� ��� �ڵ����� ȣ��
	{
		player.OnInteraction -= Player_OnInteraction;//-= �� �̺�Ʈ �ڵ鷯�� �̺�Ʈ�� �߰�
	}

	
}
