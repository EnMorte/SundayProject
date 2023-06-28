using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] private List<ButtonExtension> inputButtons;
        [SerializeField] private AndroidEventsEmitter androidEventsEmitter;
    
        private void Start()
        {
            EnableAutoOrientation();
            AddListeners();
            InitAndroid();
        }

        private void OnDisable()
        {
            DisableAutoOrientation();
        }
        
        private void InitAndroid()
        {
            androidEventsEmitter.InitEvents();
            androidEventsEmitter.androidEscape.AddListener(HandleAndroidEscape);
        }

        private void HandleAndroidEscape()
        {
            LoadingController.LoadScene(EScene.MainMenu);
        }

        private void AddListeners()
        {
            foreach (ButtonExtension button in inputButtons)
            {
                button.Init();
                button.buttonUp.AddListener(ButtonUp);
                button.buttonDown.AddListener(ButtonDown);
            }
        }

        private void ButtonUp(EInput input)
        {
            switch (input)
            {
                case EInput.Throttle:
                    car.inputForward = false;
                    break;
                case EInput.ReverseNBrake:
                    car.inputBackward = false;
                    break;
                case EInput.Left:
                    car.inputLeft = false;
                    break;
                case EInput.Right:
                    car.inputRight = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(input), input, null);
            }
        }
    
        private void ButtonDown(EInput input)
        {
            switch (input)
            {
                case EInput.Throttle:
                    car.inputForward = true;
                    break;
                case EInput.ReverseNBrake:
                    car.inputBackward = true;
                    break;
                case EInput.Left:
                    car.inputLeft = true;
                    break;
                case EInput.Right:
                    car.inputRight = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(input), input, null);
            }
        }

        private void EnableAutoOrientation()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    
        private void DisableAutoOrientation()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
