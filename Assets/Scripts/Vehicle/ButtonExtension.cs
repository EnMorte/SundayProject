using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Vehicle
{
    public enum EInput
    {
        Throttle = 1,
        ReverseNBrake = 2,
        Left = 3,
        Right = 4
    }

    public class ButtonExtension : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [HideInInspector] public UnityEvent<EInput> buttonDown, buttonUp;
        [SerializeField] private EInput input;

        public void Init()
        {
            buttonDown ??= new UnityEvent<EInput>();
            buttonUp ??= new UnityEvent<EInput>();
        }
    
        public void OnPointerDown(PointerEventData eventData)
        {
            buttonDown.Invoke(input);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonUp.Invoke(input);
        }
    }
}