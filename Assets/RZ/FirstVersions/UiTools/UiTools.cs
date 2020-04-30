using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RZ
{
    public class UiTools
    {
        // static Sprite _transparentSprite;
        // /// <summary>
        // /// Return the transparent sprite.
        // /// </summary>
        // public static Sprite transparentSprite
        // {
        //     get
        //     {
        //         if (_transparentSprite == null)
        //         {
        //             // Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        //             // texture.SetPixel(0, 0, Color.clear);
        //             // texture.Apply();
        //             // transparentSprite = Sprite.Create(texture, new Rect(0f, 0f, 1, 1), new Vector2(0.5f, 0.5f), 1);
        //             // transparentSprite.name = "transparentSprite";

        //             _transparentSprite = Resources.Load<Sprite>("TransparentSprite");
        //         }
        //         return _transparentSprite;
        //     }
        // }

        ////////// НАЖАТЬ НА ОБЪЕКТ: //////////
        public static void PressOn(GameObject UIObject
        //  ,bool forceForUIButton = true
        )
        {
            var ped = new PointerEventData(EventSystem.current);

            Selectable selectable = UIObject.GetComponent<Selectable>();
            if (selectable != null)
            {
                // EventSystem.current.SetSelectedGameObject(UIObject, null);
                EventSystem.current.SetSelectedGameObject(UIObject, ped);
            }

            IPointerClickHandler clickHandler = UIObject.GetComponent<IPointerClickHandler>();
            if (clickHandler != null)
            {
                clickHandler.OnPointerClick(ped);
            }


            // if (forceForUIButton)
            // {
            //     Button button = UIObject.GetComponent<Button>();
            //     if (button != null)
            //     {
            //         button.onClick.Invoke();
            //     }
            // }



            // Debug.Log(UIObject.GetComponents<IPointerClickHandler>().Length);


            // IPointerClickHandler clickHandler = UIObject.GetComponent<IPointerClickHandler>();
            // if (clickHandler != null)
            // {
            //     // clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
            //     clickHandler.OnPointerClick(ped);

            // }
        }

        ////////// ИЕРАРХИЯ ОТ ОБЬЕКТА "ВВЕРХ": //////////
        public static string GetFullHieracly(GameObject go)
        {
            StringBuilder sb = new StringBuilder(go.name);
            while (go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
                sb.Insert(0, go.name + "/");
            }
            sb.Insert(0, go.scene.name + "/");
            return sb.ToString();
        }

        static void _scrollTo(ScrollRect scroll, float horizontalPosition, float verticalPosition)
        {
            Canvas.ForceUpdateCanvases();
            scroll.horizontalNormalizedPosition = horizontalPosition;
            scroll.verticalNormalizedPosition = verticalPosition;
            Canvas.ForceUpdateCanvases();
        }

        public static void ScrollTo(ScrollRect scroll, float horizontalPosition, float verticalPosition)
        {
            // scroll.StartCoroutine(scrollTo(scroll, horizontalPosition, verticalPosition));
            _scrollTo(scroll, horizontalPosition, verticalPosition);
        }

        public static void ScrollToTop(ScrollRect scroll)
        {
            // scroll.StartCoroutine(scrollTo(scroll, scroll.horizontalNormalizedPosition, 1f));
            _scrollTo(scroll, scroll.horizontalNormalizedPosition, 1f);
        }

        public static void ScrollToBottom(ScrollRect scroll)
        {
            // scroll.StartCoroutine(scrollTo(scroll, scroll.horizontalNormalizedPosition, 0f));
            _scrollTo(scroll, scroll.horizontalNormalizedPosition, 0f);
        }

        public static void ScrollToLeft(ScrollRect scroll)
        {
            // scroll.StartCoroutine(scrollTo(scroll, 0f, scroll.verticalNormalizedPosition));
            _scrollTo(scroll, 0f, scroll.verticalNormalizedPosition);
        }

        public static void ScrollToRight(ScrollRect scroll)
        {
            // scroll.StartCoroutine(scrollTo(scroll, 1f, scroll.verticalNormalizedPosition));
            _scrollTo(scroll, 1f, scroll.verticalNormalizedPosition);
        }

        public static void ScrollToStart(ScrollRect scroll)
        {
            // scroll.StartCoroutine(scrollTo(scroll, 0f, 1f));
            _scrollTo(scroll, 0f, 1f);
        }

        public static void ScrollToEnt(ScrollRect scroll)
        {
            // scroll.StartCoroutine(scrollTo(scroll, 1f, 0f));
            _scrollTo(scroll, 1f, 0f);
        }


        // public static void ShowHideObjects(GameObject[] all, params string[] show)
        // {
        //     for (int i = 0; i < all.Length; i++)
        //     {
        //         GameObject o = all[i];
        //         o.SetActive(show.Contains(o.name));
        //     }
        // }

        public static void ShowHideObjects(GameObject[] all, params GameObject[] show)
        {
            for (int i = 0; i < all.Length; i++)
            {
                GameObject o = all[i];
                o.SetActive(show.Contains(o));
            }
        }
    }
}