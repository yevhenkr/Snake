using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RZ
{
    public class MouseWheelForSlider : MonoBehaviour
    // , IPointerEnterHandler
    {
        // static Slider currentSlider;
        Slider slider;
        static EventSystem currentEventSystem;

        void Start()
        {
            slider = gameObject.GetComponent<Slider>();
            currentEventSystem = EventSystem.current;
        }

        void Update()
        {
            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

            // if (mouseWheel != 0
            // && currentSlider != null
            // && currentSlider == sliderDeliveruTime
            // && currentSlider.enabled == true)
            // {
            //     if (currentSlider.gameObject != currentEventSystem.currentSelectedGameObject)
            //     {
            //         currentEventSystem.SetSelectedGameObject(currentSlider.gameObject);
            //     }

            //     sliderDeliveruTime.value += sliderDeliveruTime.wholeNumbers ? (Mathf.Sign(mouseWheel)) : (mouseWheel);
            // }

            if (mouseWheel != 0
            && slider != null
            && slider.enabled == true
            && slider.gameObject == currentEventSystem.currentSelectedGameObject)
            {
                slider.value += slider.wholeNumbers ? (Mathf.Sign(mouseWheel)) : (mouseWheel);
            }
        }

        // public void OnPointerEnter(PointerEventData eventData)
        // {
        //     currentSlider = sliderDeliveruTime;
        // }
    }
}