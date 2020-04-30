using UnityEngine;
using UnityEngine.UI;

namespace RZ.Localizations
{
    // This component will update an Image component with a localized sprite, or use a fallback if none is found
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public class LocalizedImage : LocalizedBehaviour
    {
        // [Tooltip("If PhraseName couldn't be found, this sprite will be used")]
        // public Sprite FallbackSprite;

        // This gets called every time the translation needs updating
        public override void UpdateTranslation(Translation translation)
        {
            // Get the Image component attached to this GameObject
            var image = GetComponent<Image>();
            // Use translation?
            if (translation != null)
            {
                image.sprite = translation.Object as Sprite;
            }
            else
            {
                image.sprite = null;
            }
            // // Use fallback?
            // else
            // {
            // 	image.sprite = FallbackSprite;
            // }
        }

        // protected virtual void Awake()
        // {
        // 	// Should we set FallbackSprite?
        // 	if (FallbackSprite == null)
        // 	{
        // 		// Get the SpriteRenderer component attached to this GameObject
        // 		var spriteRenderer = GetComponent<Image>();
        // 		// Copy current sprite to fallback
        // 		FallbackSprite = spriteRenderer.sprite;
        // 	}
        // }
    }
}