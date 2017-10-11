using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public enum AxisOption
        {
            // Options for which axes to use
            Both, // Use both 0
            OnlyHorizontal, // Only horizontal suihei 1
            OnlyVertical // Only vertical suityokju 2
        }

        public int MovementRange = 5;
        public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
        public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
        public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

        Vector2 m_StartPos;
        Camera camera;
        bool m_UseX; // Toggle for using the x axis
        bool m_UseY; // Toggle for using the Y axis
        CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
        CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input


        //		void OnEnable()
        //		{
        //			CreateVirtualAxes();
        //		}

        //
        void Start()
        {
            CreateVirtualAxes();
            camera = Camera.main;

            m_StartPos = new Vector2(75, 75);
        }

        void UpdateVirtualAxes(Vector2 delta)
        {
            delta /= MovementRange;
            if (m_UseX)
            {
                m_HorizontalVirtualAxis.Update(delta.x);
            }

            if (m_UseY)
            {
                m_VerticalVirtualAxis.Update(delta.y);
            }
        }

        void CreateVirtualAxes()
        {

            // set axes to use
            m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
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
            Vector2 deltaXY = Vector2.zero;
            if (m_UseX)
            {
                float deltaX = data.position.x - m_StartPos.x;
                deltaX = Mathf.Clamp(deltaX, -MovementRange, MovementRange);
                deltaXY.x = deltaX;
            }

            if (m_UseY)
            {
                float deltaY = data.position.y - m_StartPos.y;
                deltaY = Mathf.Clamp(deltaY, -MovementRange, MovementRange);
                deltaXY.y = deltaY;
            }

            Vector3 tmp = camera.ScreenToWorldPoint(new Vector3(50, 50, 0) + (Vector3)deltaXY);
            tmp.z = 0;
            transform.position = tmp;
            UpdateVirtualAxes(deltaXY);
        }


        public void OnPointerUp(PointerEventData data)
        {
            Vector3 tmp = camera.ScreenToWorldPoint(new Vector3(50, 50, 0));
            tmp.z = 0;
            transform.position = tmp;
            UpdateVirtualAxes(Vector2.zero);
        }


        public void OnPointerDown(PointerEventData data)
        {
        }

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