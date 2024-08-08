using UnityEngine;

namespace iCare {
    public static class TexturesExtensions {
        public static Texture2D Get(this Textures2D texture2D) {
            return Texture2DReferenceManager.GetByName(texture2D.ToString()).Value;
        }
    }
}