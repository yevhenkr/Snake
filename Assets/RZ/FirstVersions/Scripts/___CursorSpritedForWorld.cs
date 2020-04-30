using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ___CursorSpritedForWorld : MonoBehaviour
    {
        public static ___CursorSpritedForWorld cursorControl;

        // Sprites/colors IDs : 0, 1, ...
        public Sprite mainCursor, clickCursor;
        public Color mainColor, clickColor;

        public bool getClicks = true;
        public bool cameraMoving;

        private Transform t;
        private SpriteRenderer sr;
        private Vector3 mousePosToWorld;
        private float zPos;
        private Sprite spriteTemp;
        private Color colorTemp;

        void Awake()
        {
            if (cursorControl == null)
            {
                DontDestroyOnLoad(gameObject);
                cursorControl = this;
            }
            else if (cursorControl != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            t = transform;
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = mainCursor;
            sr.color = mainColor;
            spriteTemp = sr.sprite;
            colorTemp = sr.color;

            // Set the sortingOrder value so that the cursor is displayed on top of everything
            sr.sortingOrder = 10;

            zPos = Camera.main.nearClipPlane + 1f;
            Cursor.visible = false;
        }

        void Update()
        {
            if (!cameraMoving)
            {
                Move();
            }

            // Change color or sprite or both on click
            if (getClicks)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //ChangeColor(1);
                    ChangeSprite(1);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    //ChangeColor(0);
                    ChangeSprite(0);
                }
            }
        }

        // If the cursor trembles when the camera moves:
        // Set bool cameraMoving to true and call this function just after the Camera movement
        public void Move()
        {
            mousePosToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPos));
            // mousePosToWorld = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPos);
            t.position = mousePosToWorld;
        }

        void ChangeColor(int colorID)
        {
            sr.color = GetColor(colorID);
        }

        Color GetColor(int colorID)
        {
            if (colorID == 0)
            {
                return mainColor;
            }
            else if (colorID == 1)
            {
                return clickColor;
            }
            else
            {
                return Color.white;
            }
        }

        void ChangeSprite(int spriteID)
        {
            sr.sprite = GetSprite(spriteID);
        }

        Sprite GetSprite(int spriteID)
        {
            if (spriteID == 0)
            {
                return mainCursor;
            }
            else if (spriteID == 1)
            {
                return clickCursor;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Hide cursor or redisplay it as it was before hiding it.
        /// </summary>
        /// <param name="visible"></param>
        public void SetCursor(bool visible)
        {
            if (!visible)
            {
                spriteTemp = sr.sprite;
                colorTemp = sr.color;
                sr.sprite = null;
            }
            else
            {
                sr.sprite = spriteTemp;
                sr.color = colorTemp;
            }
        }
        /// <summary>
        /// Set cursor visible with new sprite and color.
        /// If false stocks new sprite and color before hiding cursor or only stocks if already not visible.
        /// </summary>
        /// <param name="visible"></param>
        /// <param name="spriteID">0 = mainCursor, 1 = clickCursor, ...</param>
        /// <param name="colorID">0 = mainColor, 1 = clickColor, ..., else return white.</param>
        public void SetCursor(bool visible, int spriteID, int colorID)
        {
            if (visible)
            {
                ChangeSprite(spriteID);
                ChangeColor(colorID);
            }
            else
            {
                spriteTemp = GetSprite(spriteID);
                colorTemp = GetColor(colorID);
                sr.sprite = null;
            }
        }
        /// <summary>
        /// Set cursor visible with new sprite or color.
        /// If false stocks new sprite or color before hiding cursor or only stocks if already not visible.
        /// </summary>
        /// <param name="visible"></param>
        /// <param name="type">Put "Sprite" or "Color".</param>
        /// <param name="typeID">0 = mainType, 1 = clickType, ...</param>
        public void SetCursor(bool visible, string type, int typeID)
        {
            if (type.Equals("Sprite"))
            {
                if (visible)
                {
                    ChangeSprite(typeID);
                }
                else
                {
                    spriteTemp = GetSprite(typeID);
                    sr.sprite = null;
                }
            }
            else if (type.Equals("Color"))
            {
                if (visible)
                {
                    ChangeColor(typeID);
                }
                else
                {
                    colorTemp = GetColor(typeID);
                    sr.sprite = null;
                }
            }
        }
        /// <summary>
        /// Set cursor scale.
        /// </summary>
        /// <param name="scaleAmount">xAmount will be multiplied by current scale</param>
        /// <param name="reset">Reset scale to 1 no matter what scaleAmount</param>
        public void SetCursor(float scaleAmount, bool reset)
        {
            t.localScale = reset ? t.localScale = Vector3.one : t.localScale * scaleAmount;
            t.localScale = new Vector3(
                Mathf.Clamp(t.localScale.x, 0.05f, 10f),
                Mathf.Clamp(t.localScale.y, 0.05f, 10f),
                1f);
        }

        public void PauseInputs(bool pause)
        {
            getClicks = !pause;
        }

        /*public void OnSceneTransition()
        {
            // TODO : Play an Animation or change cursor;
            // getClicks = false;
        }*/



        void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                Cursor.visible = false;
            }
        }
    }
}