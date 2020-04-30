#define RZ_SAFE_AREA

namespace RZ
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// Resizes a RectTransform to fit a safe area of screen.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    // [ExecuteAlways]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Layout/RZ/RZ Safe Area", RZMenuPriority)]
    public class RZSafeArea : UIBehaviour, ILayoutSelfController
    // ,ILayoutController
    , ILayoutIgnorer
    {
        public const int RZMenuPriority = -300;
        // public const int MinSafeSize = 100;

        [SerializeField] private RectTransform rootCanvasRT;
        [SerializeField] private RectTransform parentRT;
        [SerializeField] private RectTransform myRT;
        private DrivenRectTransformTracker drtt;

#if UNITY_EDITOR
        [SerializeField] private GameObject _myGO;

        override protected void Reset()
        {
            Init();
            _myGO = gameObject;
        }

        override protected void OnValidate()
        {
            if (_myGO != gameObject) Init();
            _myGO = gameObject;
        }
#endif

        // private bool dirty;
        public void SetDirty() { transform.hasChanged = _readyToWork; ; }

        private ScreenOrientation _orientation;

        [SerializeField] private bool _readyToWork = false;
        [SerializeField] private bool _safeAxisX = true;
        [SerializeField] private bool _expandLeft = false;
        [SerializeField] private bool _expandRight = false;
        [SerializeField] private bool _safeAxisY = true;
        [SerializeField] private bool _expandTop = false;
        [SerializeField] private bool _expandBottom = false;

        public bool readyToWork { get { return _readyToWork; } }

        public bool safeAxisX
        { get { return _safeAxisX; } set { SetVar(ref _safeAxisX, value); } }

        public bool expandLeft
        { get { return _expandLeft; } set { SetVar(ref _expandLeft, value); } }

        public bool expandRight
        { get { return _expandRight; } set { SetVar(ref _expandRight, value); } }

        public bool safeAxisY
        { get { return _safeAxisY; } set { SetVar(ref _safeAxisY, value); } }

        public bool expandTop
        { get { return _expandTop; } set { SetVar(ref _expandTop, value); } }

        public bool expandBottom
        { get { return _expandBottom; } set { SetVar(ref _expandBottom, value); } }

        private void SetVar(ref bool variable, bool value)
        {
            if (variable == value) return;
            variable = value;
            SetDirty();
        }

        public static Rect GetSafeArea()
        { return Screen.safeArea; }

        /// <summary>
        /// Method called by the layout system. Has no effect
        /// </summary>
        public virtual void SetLayoutHorizontal() { }

        /// <summary>
        /// Method called by the layout system. Has no effect
        /// </summary>
        public virtual void SetLayoutVertical() { }

        // override protected void Awake() { Init(); }

        // Work on disabled:
        override protected void OnTransformParentChanged() { StartCoroutine(DelayedInit()); }

        private System.Collections.IEnumerator DelayedInit()
        {
            transform.hasChanged = false;
            yield return new WaitForEndOfFrame();
            Init();
            if (myRT != null) myRT.localScale = Vector3.one;
            yield return null;
        }

        // Work on disabled:
        override protected void OnRectTransformDimensionsChange()
        {
            SetDirty();
            myRT.hasChanged = true;
        }

        public bool ignoreLayout { get { return true; } }

        public void Init()
        {
            drtt.Clear();

            Canvas[] canvases = GetComponentsInParent<Canvas>();
            rootCanvasRT = canvases.Length > 0 ?
                canvases[canvases.Length - 1].GetComponent<RectTransform>() : null;

            parentRT = transform.parent == null ?
                null : transform.parent.GetComponent<RectTransform>();

            myRT = GetComponent<RectTransform>();

            string errors = GetHierarchyErrors();
            _readyToWork = string.IsNullOrEmpty(errors);

            SetDirty();

            if (!_readyToWork)
            {
                Debug.LogError(errors);
#if UNITY_EDITOR
                UnityEditor.EditorGUIUtility.PingObject(this.gameObject);
#endif
            }
        }

        override protected void OnEnable() { SetDirty(); }

        override protected void OnDisable() { this.drtt.Clear(); }


        private void Update()
        { if (transform.hasChanged || _orientation != Screen.orientation) SetLayout(); }


        public string GetHierarchyErrors()
        {
            string errors = "";

            if (GetComponentInParent<Canvas>() == null)
                errors += "\n - Cannot work without Canvas in parent hierarchy.";

            Canvas myCanvas = GetComponent<Canvas>();
            if (myCanvas != null && myCanvas.rootCanvas)
                errors += "\n - Cannot work on root Canvas.";

            return string.IsNullOrEmpty(errors) ?
                null : nameof(RZSafeArea) + ":" + errors.ToString();
        }


        public void SetLayout()
        {
            drtt.Clear();

            if (!_readyToWork) return;

            if (myRT.localScale == Vector3.zero) myRT.localScale = Vector3.one;

            Rect safeArea = GetSafeArea();

            drtt.Add(this, myRT, DrivenTransformProperties.Rotation);
            myRT.rotation = Quaternion.identity;

            drtt.Add(this, myRT, DrivenTransformProperties.Scale);
            myRT.localScale = Vector3Int.one;

            Vector2 pivot = myRT.pivot;
            Vector2 aMin = myRT.anchorMin;
            Vector2 aMax = myRT.anchorMax;
            Vector3 position = myRT.position;
            Vector2 sizeDelta = myRT.sizeDelta;

            if (_expandLeft)
            {
                safeArea.width += safeArea.x;
                safeArea.x -= safeArea.x;
            }

            if (_expandRight)
            {
                safeArea.width += Screen.width - safeArea.width - safeArea.position.x;
            }

            if (_expandTop)
            {
                safeArea.height += Screen.height - safeArea.height - safeArea.position.y;
            }

            if (_expandBottom)
            {
                safeArea.height += safeArea.y;
                safeArea.y -= safeArea.y;
            }


            if (safeAxisX)
            {
                drtt.Add(this, myRT, DrivenTransformProperties.PivotX);
                drtt.Add(this, myRT, DrivenTransformProperties.AnchorMinX);
                drtt.Add(this, myRT, DrivenTransformProperties.AnchorMaxX);
                drtt.Add(this, myRT, DrivenTransformProperties.AnchoredPositionX);
                drtt.Add(this, myRT, DrivenTransformProperties.SizeDeltaX);

                pivot.x = 0.5f;
                aMin.x = 0.5f;
                aMax.x = 0.5f;
                position.x = safeArea.position.x + (safeArea.size.x * pivot.x);
                sizeDelta.x = safeArea.size.x / myRT.lossyScale.x;
                // myRT.anchorMin = new Vector2(0.5f, myRT.anchorMin.y);
                // myRT.anchorMax = new Vector2(0.5f, myRT.anchorMax.y);
                // myRT.position = new Vector2(safeArea.position.x + (safeArea.size.x * pivot.x), myRT.position.y);
                // myRT.sizeDelta = new Vector2(safeArea.size.x / myRT.lossyScale.x, myRT.sizeDelta.y);

                // myRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, safeArea.size.x / myRT.lossyScale.x);
            }

            if (_safeAxisY)
            {
                drtt.Add(this, myRT, DrivenTransformProperties.PivotY);
                drtt.Add(this, myRT, DrivenTransformProperties.AnchorMinY);
                drtt.Add(this, myRT, DrivenTransformProperties.AnchorMaxY);
                drtt.Add(this, myRT, DrivenTransformProperties.AnchoredPositionY);
                drtt.Add(this, myRT, DrivenTransformProperties.SizeDeltaY);

                pivot.y = 0.5f;
                aMin.y = 0.5f;
                aMax.y = 0.5f;
                position.y = safeArea.position.y + (safeArea.size.y * pivot.y);
                sizeDelta.y = safeArea.size.y / myRT.lossyScale.y;
                // myRT.anchorMin = new Vector2(myRT.anchorMin.x, 0.5f);
                // myRT.anchorMax = new Vector2(myRT.anchorMax.x, 0.5f);
                // myRT.position = new Vector2(myRT.position.x, safeArea.position.y + (safeArea.size.y * pivot.y));
                // myRT.sizeDelta = new Vector2(myRT.sizeDelta.x, safeArea.size.y / myRT.lossyScale.y);

                // myRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, safeArea.size.y / myRT.lossyScale.y);
            }


            myRT.pivot = pivot;
            myRT.anchorMin = aMin;
            myRT.anchorMax = aMax;
            myRT.position = position;
            myRT.sizeDelta = sizeDelta;

            transform.hasChanged = false;
            _orientation = Screen.orientation;
        }

    }
}
