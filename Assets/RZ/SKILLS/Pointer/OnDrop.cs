
namespace RZ
{

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;


    /// <summary>
    /// Invoke Unity-Events: IDropHandler
    /// Use it with UI.Image or UI.Text.
    /// </summary>
    public class OnDrop : MonoBehaviour, IDropHandler
    {
        private RectTransform _rt;

        /// <summary>
        /// Invoke if object drop on raycast target.
        /// </summary>
        [Tooltip("Invoke if object drop on raycast target.")]
        public UnityEvent onDrop;


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


        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            onDrop.Invoke();
        }
    }

}