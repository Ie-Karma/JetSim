using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

public class JoystickController : MonoBehaviour
{
	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	float sensitivity = 1f;
	//add xr reference
	[SerializeField]
	InputActionProperty aimAction;
	[SerializeField]
	GameObject pointer, dotPointer;
	private Vector2 aimInput;

	private XRJoystick joystick;
	private bool joystickActive = false;

	Vector2 joystickInput;

	void Start()
	{
		joystick = GetComponent<XRJoystick>();

	}

	public void Selected(bool b)
	{
		joystickActive = b;
		if (!b) {
			pointer.transform.localPosition = Vector3.zero;

			dotPointer.transform.localPosition =  Vector3.zero;
		}
	}
	void Update()
	{
		aimInput = aimAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

		if (joystick != null)
		{
			joystickInput = joystick.value;
		}

		if (playerController == null) return;

		// Multiplica el input del joystick por la sensibilidad y asigna los valores a pitch y roll
		float pitch = joystickInput.x * sensitivity;
		float roll = joystickInput.y * sensitivity;

		// Crea un vector3 para el input de control y asigna los valores de pitch y roll
		Vector3 controlInput = new Vector3(pitch, playerController.controlInput.y, roll);

		// Asigna el input de control actualizado al jugador
		playerController.controlInput = controlInput;

		if (joystickActive)
		{
			UpdatePointer();
            UnityEngine.XR.InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
			float intensity = joystickInput.magnitude;
			device.SendHapticImpulse(0, intensity);
		}

	}

	public void UpdatePointer()
	{

		joystickInput = Vector2.ClampMagnitude(joystickInput, 1f);
		aimInput = Vector2.ClampMagnitude(aimInput, 1f);

		float posX = Mathf.Lerp(0.1f, -0.1f, (aimInput.x + 1) / 2);
		float posY = Mathf.Lerp(0.07f, -0.07f, (aimInput.y + 1) / 2);

		pointer.transform.localPosition = new Vector3(posX, posY, pointer.transform.localPosition.z);

		// Calculate pointer rotation
		float zRotation = Mathf.Lerp(-90f, 90f, (joystickInput.y + 1f) / 2f);
		pointer.transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);


		posX = Mathf.Lerp(0.007f, -0.007f, (aimInput.x + 1) / 2);
		posY = Mathf.Lerp(0.007f, -0.007f, (aimInput.y + 1) / 2);

		dotPointer.transform.localPosition = new Vector3(posX, posY, 0f);


	}


	public void Shoot(int type)
	{

		switch (type)
		{
			case 1:
				// Llama al método OnFireCannon del PlayerController
				playerController.OnFireMissile();
				//playerController.OnFireCannon(true);
				break;
			case 2:
				playerController.OnFireCannon(false);
				break;
			case 3:
				// Llama al método OnFireMissile del PlayerController
				//playerController.OnFireMissile(true);
				break;
		}

	}

}
