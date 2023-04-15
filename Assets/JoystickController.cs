using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;

public class JoystickController : MonoBehaviour
{
	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	float sensitivity = 1f;

	private XRJoystick joystick;

	Vector2 joystickInput;

	void Start()
	{
		joystick = GetComponent<XRJoystick>();
	}

	void Update()
	{

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
	}

	public void Shoot(int type)
	{

		switch (type)
		{
			case 1:
				// Llama al método OnFireCannon del PlayerController
				playerController.OnFireCannon(true);
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
