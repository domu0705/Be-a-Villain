using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Zone : MonoBehaviour
{
	// ----------------------------------------------------------------------------------------------

	// FSM ------------------------------------------------------------------------------------------

	// Definitions ----------------------------------------------------------------------------------
	// Properties -----------------------------------------------------------------------------------
	// Outer Functions ------------------------------------------------------------------------------
	public void Enter(Player curPlayer)
	{
		player = curPlayer;
		player.Movement.Stop();
		zoneUI.anchoredPosition = Vector3.zero; //UI가 화면 정중앙에 오게 함.
		npcText.text = startText;

		Prepare();
	}

	public void Exit()
	{
		player.Movement.canMove = true;
		zoneUI.anchoredPosition = Vector3.down * 1000;
		player.Movement.Resume();
		player = null;
	}

	// Events ---------------------------------------------------------------------------------------



	// Fields : caching -----------------------------------------------------------------------------
	// Fields ---------------------------------------------------------------------------------------
	protected Player player;
	// Functions ------------------------------------------------------------------------------------

	protected virtual void Prepare()//ENter함수 호출 시 호출됨.
	{

	}

	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------



	// Unity Inspectors -----------------------------------------------------------------------------
	[Header("Zone UI")]
	public RectTransform zoneUI;
	public Text npcText;

	[Header("Zone NPC Dialogue")]
	[TextArea]
	public string startText;

	// Unity Messages -------------------------------------------------------------------------------


	// Unity Coroutine ------------------------------------------------------------------------------
}
