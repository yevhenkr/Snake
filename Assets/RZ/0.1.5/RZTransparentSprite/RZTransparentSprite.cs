namespace RZ
{
    using UnityEngine;

    /// <summary>
    /// Allow to use small empty static sprite 4x4 with color alpha == 0.
    /// </summary>
    public static class RZTransparentSprite
    {

        private static Sprite _spriteFromRes = null;
        /// <summary>
        /// Return the transparent sprite from RZ resources.
        /// </summary>
        public static Sprite fromResources
        {
            get
            {
                if (_spriteFromRes == null)
                    _spriteFromRes = Resources.Load<Sprite>("RZTransparentSprite");
                _spriteFromRes.name = "RZTransparentSpriteFromRes";

                return _spriteFromRes;
            }
        }


        private static Sprite _spriteByCode = null;
        /// <summary>
        /// Return the transparent sprite created by RZ code.
        /// </summary>
        public static Sprite byCode
        {
            get
            {
                if (_spriteByCode == null)
                {
                    Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    texture.SetPixel(0, 0, Color.clear);
                    texture.Apply();
                    _spriteByCode = Sprite.Create(
                        texture, new Rect(0f, 0f, 4, 4), new Vector2(0.5f, 0.5f), 10);
                    _spriteByCode.name = "RZTransparentSpriteByCode";
                }
                return _spriteByCode;
            }
        }

    }

}