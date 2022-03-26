using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// Properties -----------------------------------------------------------------------------------
	public Player Player => player;

	// Outer Functions ------------------------------------------------------------------------------
	/*	public void Action(ObjectData obj)
		{
			player.Movement.Stop();
			TalkManager.Instance.Talk(questManager.curQuestNum, obj);
		}
	*/


	// Fields : caching -----------------------------------------------------------------------------
	private Player player;

	// Fields ---------------------------------------------------------------------------------------

	// Event Handlers -------------------------------------------------------------------------------
	private void Player_OnInteraction(Player player, ObjectData obj)
	{
		player.Movement.Stop();
		TalkManager.Instance.Talk(questManager.curQuestNum, obj);
	}

	private void Player_Attack(Player player, Item equipWeapon)
	{
		Debug.Log("GameManager.Player_Attack()");
		//���̸� �Ѿ� ������Ʈ Ǯ������ ��������
		//Į�̸� �ݶ����� �ѱ�

		equipWeapon.Use();
		/*		else if (equipWeapon.type == Item.Type.Gun)
		{

		}*/
	}

	private void Player_CamChange(int camNum)
    {
		cameraMovement.ChangeCamTo(camNum);
    }

	// Unity Inspectors -----------------------------------------------------------------------------
	public QuestManager questManager;
	public CameraMovement cameraMovement;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		player = FindObjectOfType<Player>();
	}

	

	private void OnEnable()//������Ʈ�� Ȱ��ȭ�� ��� �ڵ����� ȣ��
	{
		player.OnInteraction += Player_OnInteraction;//+= �� �̺�Ʈ �ڵ鷯�� �̺�Ʈ�� �߰�.  (Player_OnInteraction() �� ����Ǵ� ���� �ƴ�)
		player.OnAttack += Player_Attack;
		player.OnCamChange += Player_CamChange;
	}

	private void OnDisable()//������Ʈ�� ��Ȱ��ȭ�� ��� �ڵ����� ȣ��
	{
		player.OnInteraction -= Player_OnInteraction;//-= �� �̺�Ʈ �ڵ鷯�� �̺�Ʈ�� �߰�
		player.OnAttack -= Player_Attack;
		player.OnCamChange -= Player_CamChange;
	}

	
}
