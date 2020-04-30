// namespace RZ
namespace UnityEngine
{
    /// <summary>
    /// Extensions for 'UnityEngine.RectTransform'
    /// </summary>
    public static class RectTransformExtensions
    {

        /// <summary>
        /// Check parent for anchors/corners operations.
        /// Return parent RectTransform or null.
        /// </summary>
        // public
        private static RectTransform RZGetParentRectTransform(this RectTransform rectTransform)
        {
            if (rectTransform == null) return null;
            if (rectTransform.transform.parent == null) return null;
            return rectTransform.parent.gameObject.GetComponent<RectTransform>();
        }


        /// <summary>
        /// Move anchors to object's corners.
        /// </summary>
        public static void RZAnchorsToCorners(this RectTransform rectTransform)
        {
            var parentRT = RZGetParentRectTransform(rectTransform);
            if (parentRT == null) return;

            Vector2 parent_scale = new Vector2(
                parentRT.rect.width, parentRT.rect.height);

            rectTransform.anchorMin = new Vector2(
                rectTransform.anchorMin.x + (rectTransform.offsetMin.x / parent_scale.x),
                rectTransform.anchorMin.y + (rectTransform.offsetMin.y / parent_scale.y));

            rectTransform.anchorMax = new Vector2(
                rectTransform.anchorMax.x + (rectTransform.offsetMax.x / parent_scale.x),
                rectTransform.anchorMax.y + (rectTransform.offsetMax.y / parent_scale.y));

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }


        /// <summary>
        /// Move anchors to object's corners.
        /// </summary>
        public static void RZAnchorsToCorners(GameObject gameObject)
        {
            if (gameObject == null) return;
            RZAnchorsToCorners(gameObject.GetComponent<RectTransform>());
        }




        /// <summary>
        /// Move anchors to object's corners, and do it with object's children,
        /// children of children..."
        /// </summary>
        public static void RZAnchorsToCornersRecursive(this RectTransform rectTransform)
        {
            if (rectTransform == null) return;
            RZAnchorsToCornersRecursive(rectTransform.gameObject);
        }


        /// <summary>
        /// Move anchors to object's corners, and do it with object's children,
        /// children of children..."
        /// </summary>
        public static void RZAnchorsToCornersRecursive(GameObject gameObject)
        {
            if (gameObject == null) return;

            RZAnchorsToCorners(gameObject);

            for (int i = 0; i < gameObject.transform.childCount; i++)
            { RZAnchorsToCornersRecursive(gameObject.transform.GetChild(i).gameObject); }
        }




        /// <summary>
        /// Move corners to object's anchors.
        /// </summary>
        public static void RZCornersToAnchors(this RectTransform rectTransform)
        {
            var parentRT = RZGetParentRectTransform(rectTransform);
            if (parentRT == null) return;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }


        /// <summary>
        /// Move corners to object's anchors.
        /// </summary>
        public static void RZCornersToAnchors(GameObject gameObject)
        {
            if (gameObject == null) return;
            RZCornersToAnchors(gameObject.GetComponent<RectTransform>());
        }




        /// <summary>
        /// Move corners to object's anchors, and do it with object's children,
        /// children of children..."
        /// </summary>
        public static void RZCornersToAnchorsRecursive(this RectTransform rectTransform)
        {
            if (rectTransform == null) return;
            RZCornersToAnchorsRecursive(rectTransform.gameObject);
        }


        /// <summary>
        /// Move corners to object's anchors, and do it with object's children,
        /// children of children..."
        /// </summary>
        public static void RZCornersToAnchorsRecursive(GameObject gameObject)
        {
            if (gameObject == null) return;

            RZCornersToAnchors(gameObject);

            for (int i = 0; i < gameObject.transform.childCount; i++)
            { RZCornersToAnchorsRecursive(gameObject.transform.GetChild(i).gameObject); }
        }

    }

}
