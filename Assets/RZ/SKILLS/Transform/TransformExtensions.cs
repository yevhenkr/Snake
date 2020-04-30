// namespace RZ
namespace UnityEngine
{
    /// <summary>
    /// Extensions for 'UnityEngine.Transform'
    /// </summary>
    public static class TransformExtensions
    {

        /// <summary>
        /// Returns the string-hierarchy of transform.
        /// </summary>
        public static string RZGetHierarchy(this Transform t)
        {
            if (t == null)
                return null;
            else
                return _RZGetHierarchy(t);
        }


        private static string _RZGetHierarchy(Transform t)
        {
            if (t.parent == null) return t.gameObject.scene.name + "/" + t.name;
            return _RZGetHierarchy(t.parent) + "/" + t.name;
        }

    }

}
