using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;

public class SpeedController : MonoBehaviour
{
	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	float minSpeed = 0f;
	[SerializeField]
	float maxSpeed = 100f;
	private XRSlider slider;

	float speed;

	private void Start()
	{
		slider = GetComponent<XRSlider>();
	}

	void Update()
	{
		speed = slider.value;
		if (playerController == null) return;
		float speedTransformed = speed * 2f - 1f;

		// Asigna el input de velocidad actualizado al avión
		playerController.plane.SetThrottleInput(speedTransformed);
	}
}
