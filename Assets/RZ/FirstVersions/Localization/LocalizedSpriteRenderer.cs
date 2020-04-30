using UnityEngine;

namespace RZ.Localizations
{
    // This component will update a SpriteRenderer component with a localized sprite, or use a fallback if none is found
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class LocalizedSpriteRenderer : LocalizedBehaviour
    {
        // [Tooltip("If PhraseName couldn't be found, this sprite will be used")]
        // public Sprite FallbackSprite;

        // This gets called every time the translation needs updating
        public override void UpdateTranslation(Translation translation)
        {
            // Get the SpriteRenderer component attached to this GameObject
            var spriteRenderer = GetComponent<SpriteRenderer>();

            // Use translation?
            if (translation != null)
            {
                spriteRenderer.sprite = translation.Object as Sprite;
            }
            else
            {
                spriteRenderer.sprite = null;
            }
            // // Use fallback?
            // else
            // {
            // 	spriteRenderer.sprite = FallbackSprite;
            // }
        }

        // protected virtual void Awake()
        // {
        // 	// Should we set FallbackSprite?
        // 	if (FallbackSprite == null)
        // 	{
        // 		// Get the SpriteRenderer component attached to this GameObject
        // 		var spriteRenderer = GetComponent<SpriteRenderer>();

        // 		// Copy current sprite to fallback
        // 		FallbackSprite = spriteRenderer.sprite;
        // 	}
        // }
    }
}