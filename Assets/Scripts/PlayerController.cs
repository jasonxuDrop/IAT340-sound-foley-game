using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	// variables
	[SerializeField] float moveSpeed;
	[SerializeField] float lookSpeed;
	[SerializeField] float lookCameraLimit;
	[SerializeField] Transform cam;

	private CharacterController characterController;
	private Vector2 m_Rotation; // x: camera, y: player
	private Vector2 m_Look; // input
	private Vector2 m_Move;

	// functions
	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		// update look before move so the player doesn't move in the wrong direction
		// before the rotation is updated
		Look();
		Move();
	}

	private void Look()
	{
		if (m_Look.sqrMagnitude < 0.01f)
			return;
		var scaledLook = m_Look * lookSpeed;
		m_Rotation.y = Mathf.Clamp(m_Rotation.y + scaledLook.y, -lookCameraLimit, lookCameraLimit);
		m_Rotation.x += scaledLook.x;

		cam.localRotation = Quaternion.Euler(-m_Rotation.y, 0, 0);
		transform.localRotation = Quaternion.Euler(0, m_Rotation.x, 0);
	}

	private void Move()
	{
		if (m_Move.sqrMagnitude < 0.01f)
			return;
		// For simplicity's sake, we just keep movement in a single plane here. Rotate
		// direction according to world Y rotation of player.
		var orientedMove = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(m_Move.x, 0, m_Move.y);
		characterController.Move(orientedMove * moveSpeed * Time.deltaTime);
	}


	// input (called by PlayerInput component on the player)
	public void OnMove(InputAction.CallbackContext context)
	{
		m_Move = context.ReadValue<Vector2>();
	}

	public void OnLook(InputAction.CallbackContext context)
	{
		m_Look = context.ReadValue<Vector2>();
	}
}
