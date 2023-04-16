using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class SpeedController : MonoBehaviour
{
	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	float minSpeed = 0f;
	[SerializeField]
	float maxSpeed = 100f;
	private XRSlider slider;
	private bool sliderActive = false;

	float speed;

	private void Start()
	{
		slider = GetComponent<XRSlider>();
	}


	public void Selected(bool b)
	{
		sliderActive = b;
	}

	void Update()
	{
		speed = slider.value;
		if (playerController == null) return;
		float speedTransformed = speed * 2f - 1f;

		// Asigna el input de velocidad actualizado al avión
		playerController.plane.SetThrottleInput(speedTransformed);

		if (sliderActive)
		{
			UnityEngine.XR.InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
			float intensity = speed;
			device.SendHapticImpulse(0, intensity);
		}

	}
}
