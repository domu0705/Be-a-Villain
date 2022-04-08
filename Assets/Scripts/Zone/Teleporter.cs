using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
	// Definitions ----------------------------------------------------------------------------------
	// Outer Properties -----------------------------------------------------------------------------
	// Outer Functions ------------------------------------------------------------------------------
	public void Teleport()
    {
		SceneManager.LoadScene(destScene);
    }

	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	// Inner Properties -----------------------------------------------------------------------------

	// Inner Functions ------------------------------------------------------------------------------
	// Coroutine ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------



	// Unity Inspectors -----------------------------------------------------------------------------
	[SerializeField] private string destScene;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		
	}
	private void Start()
	{
		
	}
}