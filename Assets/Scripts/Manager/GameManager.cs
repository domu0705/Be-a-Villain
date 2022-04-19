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
		//총이면 총알 오브젝트 풀링으로 가져오기
		//칼이면 콜라이터 켜기

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
		//Debug.Log("게임 메니저 awake()");
		player = FindObjectOfType<Player>();
		Debug.Log("게임 메니저의 player="+ player.gameObject.scene.name);
		cameraMovement = FindObjectOfType<CameraMovement>();
		hudUI = FindObjectOfType<HudUI>();
		inventoryUI = FindObjectOfType<InventoryUI>();
		questManager = FindObjectOfType<QuestManager>();
		talkManager = TalkManager.Instance;
	}

	private void OnEnable()//오브젝트가 활성화될 경우 자동으로 호출
	{
		player.OnInteraction += Player_OnInteraction;//+= 로 이벤트 핸들러를 이벤트에 추가.  (Player_OnInteraction() 이 실행되는 것은 아님)
		player.OnAttack += Player_Attack;
		player.OnCamChange += Player_CamChange;
	}

	private void OnDisable()//오브젝트가 비활성화될 경우 자동으로 호출
	{
		player.OnInteraction -= Player_OnInteraction;//-= 러 이벤트 핸들러를 이벤트에 추가
		player.OnAttack -= Player_Attack;
		player.OnCamChange -= Player_CamChange;
	}

	
}
