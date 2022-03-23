using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : Singleton<InventoryUI>
{
	// Definitions ----------------------------------------------------------------------------------
	// Outer Properties -----------------------------------------------------------------------------

	// Outer Functions ------------------------------------------------------------------------------
	public void CheckSwordUI()
    {
		swordShadeImg.SetActive(false);
		gunShadeImg.SetActive(true);
	}

	public void CheckGunUI()
	{
		swordShadeImg.SetActive(true);
		gunShadeImg.SetActive(false);
	}

	public void TurnOnSwordImg()
	{
		swordImg.SetActive(true);
	}

	public void TurnOnGunImg()
    {
		gunImg.SetActive(true);
	}

	// Events ---------------------------------------------------------------------------------------



	// Fields : caching -----------------------------------------------------------------------------
	// Fields ---------------------------------------------------------------------------------------
	// Functions ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------



	// Unity Inspectors -----------------------------------------------------------------------------
	[Header("Inventory UI")]
	public GameObject swordImg;
	public GameObject gunImg;
	public GameObject swordShadeImg;
	public GameObject gunShadeImg;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		swordImg.SetActive(false);
		gunImg.SetActive(false);
		swordShadeImg.SetActive(true);
		gunShadeImg.SetActive(true);
	}

	private void Start()
	{
		
	}

	// Unity Coroutine ------------------------------------------------------------------------------
}
