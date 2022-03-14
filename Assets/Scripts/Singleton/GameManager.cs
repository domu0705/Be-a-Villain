using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// Properties -----------------------------------------------------------------------------------
	public Player Player => player;
	public HudUI HUDUI => hudUI;
	// Methods --------------------------------------------------------------------------------------
/*	public void Action(ObjectData obj)
	{
		player.Movement.Stop();
		TalkManager.Instance.Talk(questManager.curQuestNum, obj);
	}
*/


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

	private void Player_Attack(Player player, Item equipWeapon)
	{
		Debug.Log("GameManager.Player_Attack()");
		//총이면 총알 오브젝트 풀링으로 가져오기
		//칼이면 콜라이터 켜기
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
		player.OnAttack += Player_Attack;
	}

	private void OnDisable()//오브젝트가 비활성화될 경우 자동으로 호출
	{
		player.OnInteraction -= Player_OnInteraction;//-= 러 이벤트 핸들러를 이벤트에 추가
		player.OnAttack -= Player_Attack;
	}

	
}
