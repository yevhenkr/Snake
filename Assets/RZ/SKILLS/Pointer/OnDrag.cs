
namespace RZ
{

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;


    /// <summary>
    /// Invoke Unity-Events: IBeginDragHandler, IDragHandler, IEndDragHandler
    /// Use it with UI.Image or UI.Text.
    /// </summary>
    public class OnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rt;

        /// <summary>
        /// Invoke if begin drag raycast target.
        /// </summary>
        [Tooltip("Invoke ifbegin drag raycast target.")]
        public UnityEvent onBeginDrag;

        /// <summary>
        /// Invoke if drag raycast target at now.
        /// </summary>
        [Tooltip("Invoke if drag raycast target at now.")]
        public UnityEvent onDrag;

        /// <summary>
        /// Invoke if end drag raycast target.
        /// </summary>
        [Tooltip("Invoke if end drag raycast target.")]
        public UnityEvent onEndDrag;


        private bool _isDraging;
        /// <summary>
        /// Return if drag just at now.
        /// </summary>
        public bool IsDragging() { return _isDraging; }


        private PointerEventData _pointerEventData;
        /// <summary>
        /// Return last pointer event data.
        /// </summary>
        public PointerEventData GetPointerEventData() { return _pointerEventData; }


        public void Reset()
        {
            _CheckTarget();
        }


        private void Start()
        {
            _rt = GetComponent<RectTransform>();
            _CheckTarget();
        }


        private bool _CheckTarget()
        {
            if (GetComponentInChildren<MaskableGraphic>() != null)
                return true;

            RZDebug.LogWarningNoTrace(
                "Target Error: You need a raycast target " +
                "(MaskableGraphic like UI.Text or UI.Image or etc.)" +
                " on gameObject or on it's child." + "\n" +
                this.RZGetHierarchy());

#if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.PingObject(gameObject);
#endif
            return false;
        }


        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            _isDraging = true;
            onBeginDrag.Invoke();
        }


        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            onDrag.Invoke();
        }


        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            onEndDrag.Invoke();
            _isDraging = false;
        }

    }

}