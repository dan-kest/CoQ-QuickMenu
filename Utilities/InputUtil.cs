using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;
using CavesOfQuickMenu.Concepts;

namespace CavesOfQuickMenu.Utilities
{
    public static class InputUtil
    {
        public static readonly BiDictionary<InputDevice, ControlManager.InputDeviceType> InputToInput
                = new BiDictionary<InputDevice, ControlManager.InputDeviceType>()
        {
            Data = new Dictionary<InputDevice, ControlManager.InputDeviceType>()
            {
                { InputDevice.Gamepad, ControlManager.InputDeviceType.Gamepad },
                { InputDevice.Keyboard, ControlManager.InputDeviceType.Keyboard },
                { InputDevice.Mouse, ControlManager.InputDeviceType.Mouse },
            }
        };

        public static Direction AxisToDirection(float x, float y)
        {
            const double inputSectorAngle = 360.0f / 8;
            const double inputSectorAngleOffset = inputSectorAngle / 2.0f;

            double angleRadian = Math.Atan2(x, y);
            if (angleRadian < 0.0f)
            {
                angleRadian += Math.PI * 2.0f;
            }
            double angleDegree = 180.0f * angleRadian / Math.PI;
            double inputDegree = angleDegree + inputSectorAngleOffset;
            return (Direction) Math.Floor(inputDegree / inputSectorAngle);
        }

        public static bool IsStickInDeadzone(float axisX, float axisY)
        {
            float threshold = QudOption.DeadzoneThreshold;
            bool hasAxisX = axisX <= threshold && axisX >= -threshold;
            bool hasAxisY = axisY <= threshold && axisY >= -threshold;
            return hasAxisX && hasAxisY;
        }

        public static (float, float) GetMousePosition()
        {
            Vector3 vector = Input.mousePosition;
            return (vector.x, vector.y);
        }

        public static (float, float) GetStickPosition(string stickType)
        {
            Vector2 vector = CommandBindingManager.CommandBindings[stickType].ReadValue<Vector2>();
            return (vector.x, vector.y);
        }
    }
}
