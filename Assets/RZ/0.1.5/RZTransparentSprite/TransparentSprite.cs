using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public static class TransparentSprite
    {
        static Sprite _sprite;

        /// <summary>
        /// Return the transparent sprite.
        /// </summary>
        public static Sprite sprite
        {
            get
            {
                if (_sprite == null)
                {
                    // Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    // texture.SetPixel(0, 0, Color.clear);
                    // texture.Apply();
                    // transparentSprite = Sprite.Create(texture, new Rect(0f, 0f, 1, 1), new Vector2(0.5f, 0.5f), 1);
                    // transparentSprite.name = "transparentSprite";

                    _sprite = Resources.Load<Sprite>("TransparentSprite");
                }
                return _sprite;
            }
        }
    }
}