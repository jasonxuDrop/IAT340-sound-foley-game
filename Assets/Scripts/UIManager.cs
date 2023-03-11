using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	// variables
	[SerializeField] PlayerController playerController;
	[SerializeField] Animator recorderUIAnimator;

	bool isRecorderActive = false;

	// functions
	private void Awake()
	{
		SetRecorderUI(false);
	}


	public void ToggleRecorderUI()
	{
		Debug.Log("HIIIII");
		isRecorderActive = !isRecorderActive;
		SetRecorderUI(isRecorderActive);
	}

	private void SetRecorderUI(bool state)
	{
		isRecorderActive = state;
		if (state)
		{
			// show mouse cursor
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			// hide mouse cursor
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		playerController.canMove = !isRecorderActive;
		recorderUIAnimator.SetBool("isActive", isRecorderActive);
	}
}
