// RZ.TabWalker 2.5
//
// Позволяет перемещение между всеми визуальными объектами по "TAB"
//
// Привязать только один экземпляр к каждой сцене.
// "enterAsTab" - для однострочных полей ввода реагировать на нажатие "ENTER",
//                так-же как на нажатие "TAB".

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RZ
{
    public class TabWalker : RZ.SingletonMonoBehaviour<RZ.TabWalker>
    {
        public bool EnterAsTab = true;
        public GameObject FirstSelected = null;

        public static void SetFirstSelected(GameObject UiObject)
        {
            EventSystem.current.firstSelectedGameObject = UiObject;
            GetInstance().FirstSelected = UiObject;
        }

        public static GameObject GetFirstSelected()
        {
            SynchronizeFirstSelected();
            return GetInstance().FirstSelected;
        }

        public static void Select(GameObject UiObject)
        {
            UiObject.GetComponent<Selectable>().Select();
            if (GetInstance().FirstSelected == null)
            {
                SynchronizeFirstSelected();
            }
        }

        public static GameObject Selected()
        {
            return EventSystem.current.currentSelectedGameObject;
        }

        private static void SynchronizeFirstSelected()
        {
            if (GetInstance().FirstSelected == null)
            {
                if (EventSystem.current.firstSelectedGameObject != null)
                {
                    GetInstance().FirstSelected = EventSystem.current.firstSelectedGameObject;
                }
                else if (Selectable.allSelectablesArray.Length > 0)
                {
                    if (EventSystem.current.currentSelectedGameObject == null)
                    {
                        SetFirstSelected(Selectable.allSelectablesArray[0].gameObject);
                    }
                    else
                    {
                        SetFirstSelected(EventSystem.current.currentSelectedGameObject);
                    }

                }
            }
            else
            {
                EventSystem.current.firstSelectedGameObject = GetInstance().FirstSelected;
            }
        }

        void Update()
        {
            SynchronizeFirstSelected();

            if (Selected() == null)
            {
                Select(GetFirstSelected());
            }

            if (Input.GetKeyDown(KeyCode.Tab))
                SelectAnother(true, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            else if (GetInstance().EnterAsTab && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
                SelectAnother(false, false);
        }

        public static void SelectNext()
        {
            SelectAnother(true, false);
        }

        public static void SelectPrevious()
        {
            SelectAnother(true, true);
        }

        //Выделить следующий элемент:
        private static void SelectAnother(bool tabPressed, bool shiftPressed)
        {
            if (EventSystem.current.currentSelectedGameObject == null) return;
            Selectable current = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
            if (current == null) return;

            int i = Selectable.allSelectablesArray.Length;
            if (i == 0) return;

            Selectable next = shiftPressed ? current.FindSelectableOnUp() : current.FindSelectableOnDown();
            if (next == null) next = GetInstance().FirstSelected.GetComponent<Selectable>();
            // if (next == null) next = GetFirstSelected().GetComponent<Selectable>();
            // if (next == null) next = Selectable.allSelectables[0].gameObject.GetComponent<Selectable>();


            // //    Зацикливание переходов:
            // if (next == null)
            // {
            //     next = current;
            //     Selectable postNext;
            //     if (shiftPressed)
            //     {
            //         while ((postNext = next.FindSelectableOnDown()) != null && i-- > 0)
            //         {
            //             next = postNext;
            //         }
            //     }
            //     else
            //     {
            //         while ((postNext = next.FindSelectableOnUp()) != null && i-- > 0)
            //         {
            //             next = postNext;
            //         }
            //     }
            //     if (i <= 0) next = Selectable.allSelectables[0];
            // }


            // Симулируем нажатие мышью на поле ввода:
            // InputField nextInputField = next.GetComponent<InputField>();
            // if (nextInputField != null)
            // {
            //     Debug.Log("Click on: "+ nextInputField.name);
            //     nextInputField.Select();
            //     nextInputField.OnPointerClick(new PointerEventData(currentEventSystem));
            // }

            // Выбираем следующий компонент:
            InputField currentInputField = current.GetComponent<InputField>();
            if (tabPressed || (currentInputField != null && GetInstance().EnterAsTab
                && (!currentInputField.multiLine || currentInputField.contentType == InputField.ContentType.Password)))
            {
                next.Select();
                //currentEventSystem.SetSelectedGameObject(next.gameObject, new BaseEventData(currentEventSystem));
                //Debug.Log("Selected on: " + currentEventSystem.currentSelectedGameObject.name);
            }
        }

    }
}