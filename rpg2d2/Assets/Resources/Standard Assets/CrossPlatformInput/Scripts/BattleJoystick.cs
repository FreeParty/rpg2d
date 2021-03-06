﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class BattleJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both 0
			OnlyHorizontal, // Only horizontal suihei 1
			OnlyVertical // Only vertical suityokju 2
		}

		public int MovementRange = 100;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

		EncountController m_encount;
		GameObject refObj;
		CommandsController m_battle;


		void Start()
		{
			CreateVirtualAxes();
			m_StartPos = transform.position;
			m_encount = GetComponent<EncountController>();
			refObj = GameObject.Find ("Fielad1/parent/commands/Panel");
			if (SceneManager.GetActiveScene ().name == "battle") {
				m_battle = refObj.GetComponent<CommandsController> ();
				//				this.enabled = true;
			}
		}

		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
			}

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes()
		{
			// set axes to use
//			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseX = false;
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);
			// create new axes based on axes to use
			if (m_UseX)
			{
				if (CrossPlatformInputManager.AxisExists(horizontalAxisName))
				{
					CrossPlatformInputManager.UnRegisterVirtualAxis(horizontalAxisName);
				}
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				if (CrossPlatformInputManager.AxisExists(verticalAxisName))
				{
					CrossPlatformInputManager.UnRegisterVirtualAxis(verticalAxisName);
				}
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}
		}


		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;

			if (SceneManager.GetActiveScene ().name == "main") {
				m_encount.RandomEncount ();
				if (SceneManager2d.isEncount)
					this.enabled = false;
			}

			if (m_UseX)
			{
				int delta = (int)(data.position.x - m_StartPos.x);
				delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (m_UseY)
			{
				int delta = (int)(data.position.y - m_StartPos.y);
				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}
			transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
			UpdateVirtualAxes(transform.position);
		}


		public void OnPointerUp(PointerEventData data)
		{
			float x = CrossPlatformInputManager.GetAxis(horizontalAxisName); // X
			float y = CrossPlatformInputManager.GetAxis (verticalAxisName); // Y

			transform.position = m_StartPos;
			UpdateVirtualAxes(m_StartPos);

			if (SceneManager.GetActiveScene ().name == "battle") {
				m_battle.MoveCommandCursor (x, y);
			}

		}


		public void OnPointerDown(PointerEventData data) { }

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis.Remove();
			}
		}
	}
}