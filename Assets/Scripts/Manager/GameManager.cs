using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// Properties -----------------------------------------------------------------------------------
	public Player Player => player;
	public CameraMovement CameraMovement=> cameraMovement;
	public HudUI HudUI => hudUI;
	public InventoryUI InventoryUI => inventoryUI;

	// Outer Functions ------------------------------------------------------------------------------
	/*	public void Action(ObjectData obj)
		{
			player.Movement.Stop();
			TalkManager.Instance.Talk(questManager.curQuestNum, obj);
		}
	*/


	// Properties : caching -------------------------------------------------------------------------
	private Player player;
	[SerializeField] private CameraMovement cameraMovement;
	[SerializeField] private HudUI hudUI;
	[SerializeField] private InventoryUI inventoryUI;
	[SerializeField] private QuestManager questManager;

	// Inner Properties -----------------------------------------------------------------------------
	[SerializeField] private TalkManager talkManager;

	// Inner Functions ------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	private void Player_OnInteraction(Player player, ObjectData obj)
	{
		player.Movement.Stop();
		talkManager.Talk(questManager.curQuestNum, obj);
	}

	private void Player_Attack(Player player, Item equipWeapon)
	{
		//Debug.Log("GameManager.Player_Attack()");
		//���̸� �Ѿ� ������Ʈ Ǯ������ ��������
		//Į�̸� �ݶ����� �ѱ�

		equipWeapon.Use();
	}

	private void Player_CamChange(int camNum)
    {
		cameraMovement.ChangeCamTo(camNum);
    }

	// Unity Inspectors -----------------------------------------------------------------------------

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		//Debug.Log("���� �޴��� awake()");
		player = FindObjectOfType<Player>();
		Debug.Log("���� �޴����� player="+ player.gameObject.scene.name);
		cameraMovement = FindObjectOfType<CameraMovement>();
		hudUI = FindObjectOfType<HudUI>();
		inventoryUI = FindObjectOfType<InventoryUI>();
		questManager = FindObjectOfType<QuestManager>();
		talkManager = TalkManager.Instance;
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
