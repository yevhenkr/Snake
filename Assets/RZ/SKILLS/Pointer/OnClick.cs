
namespace RZ
{

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;


    /// <summary>
    /// Invoke Unity-Events: IPointerDownHandler, IPointerUpHandler and OnPointerClick
    /// Use it with UI.Image or UI.Text.
    /// </summary>
    public class OnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private RectTransform _rt;

        /// <summary>
        /// Invoke if press on raycast target.
        /// </summary>
        [Tooltip("Invoke if press on raycast target.")]
        public UnityEvent onPointerDown;

        /// <summary>
        /// Invoke if press and than unpress inside rect of raycast target.
        /// </summary>
        [Tooltip("Invoke if press and than unpress inside rect of raycast target.")]
        public UnityEvent onPointerClick;

        /// <summary>
        /// Invoke if uppress after press on raycast target.
        /// </summary>
        [Tooltip("Invoke if uppress after press on raycast target.")]
        public UnityEvent onPointerUp;


        private bool _isPressing;
        /// <summary>
        /// Return if press just at now.
        /// </summary>
        public bool IsPressing() { return _isPressing; }


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
            onPointerClick.AddListener(delegate { Debug.Log(1111111); });
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


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            _isPressing = true;
            onPointerDown.Invoke();
        }


        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            if (_isPressing)
            {
                if (_rt.rect.Contains(_rt.InverseTransformPoint(eventData.position)))
                    onPointerClick.Invoke(); ;
            }
            onPointerUp.Invoke();

            _isPressing = false;
        }

    }

}