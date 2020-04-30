namespace RZ
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Pointer tools for touch/mouse input.
    /// </summary>
    public class PointerTools
    {

        public static Vector2 PointerNormalizedPositionIn(
            RectTransform rt, PointerEventData ped, Canvas parentCanvas = null)
        {
            Vector2 point;
            if (parentCanvas == null) parentCanvas = rt.GetComponentInParent<Canvas>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rt, ped.position, parentCanvas.worldCamera, out point);
            point = Rect.PointToNormalized(rt.rect, point);
            return point;
        }


        public static Vector2 MouseNormalizedPositionIn(
            RectTransform rt, Canvas parentCanvas = null)
        {
            Vector2 point;
            if (parentCanvas == null) parentCanvas = rt.GetComponentInParent<Canvas>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rt, Input.mousePosition, parentCanvas.worldCamera, out point);
            point = Rect.PointToNormalized(rt.rect, point);
            return point;
        }


        public static bool IsMouseOver(GameObject gameObject)
        {
            return IsMouseOver(gameObject.GetComponent<RectTransform>());
        }


        public static bool IsMouseOver(RectTransform rectTransform)
        {
            Vector2 mousePoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                Input.mousePosition,
                rectTransform.GetComponentInParent<Canvas>().worldCamera,
                out mousePoint);
            return rectTransform.rect.Contains(mousePoint);
        }


        public static bool IsClickOrTouchAnywhere()
        { return IsClickAnywhere() || IsTouchAnywhere(); }


        public static bool IsClickAnywhere()
        {
            return
                Input.GetMouseButtonDown(0) ||
                Input.GetMouseButtonDown(1) ||
                Input.GetMouseButtonDown(2);
        }


        public static bool IsTouchAnywhere()
        {
            return
                Input.touchCount >= 1 &&
                Input.touches[0].phase == TouchPhase.Began;
        }
    }
}