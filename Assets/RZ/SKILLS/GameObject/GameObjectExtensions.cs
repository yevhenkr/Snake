using System;

// namespace RZ
namespace UnityEngine
{
    /// <summary>
    /// Extensions for 'UnityEngine.GameObject'
    /// </summary>
    public static class GameObjectExtensions
    {
        // /// <summary>
        // /// Returns the component of Type type if the game object has one attached, null
        // /// </summary>
        // public static Component RZGetComponent(this GameObject go, string type)
        // { return go.GetComponent(type); }


        // /// <summary>
        // /// Returns the component of Type type if the game object has one attached, null
        // /// </summary>
        // public static Component RZGetComponent(this GameObject go, Type type)
        // { return go.GetComponent(type); }


        // /// <summary>
        // /// Returns the component of Type type if the game object has one attached, null
        // /// </summary>
        // public static T RZGetComponent<T>(this GameObject go)
        // { return go.GetComponent<T>(); }




        /// <summary>
        /// Returns the component of Type type if the game object has one attached, null
        /// </summary>
        public static Component RZGetComponent(this GameObject go, Type t, bool includeInactive)
        {
            if (includeInactive)
            {
                var components = go.GetComponentsInChildren(t, includeInactive);
                if (components.Length > 0 &&
                    components[0].gameObject == go)
                    return components[0];
                else return null;
            }
            else
            {
                return go.GetComponent(t);
            }
        }



        /// <summary>
        /// Returns the component of Type type if the game object has one attached, null
        /// </summary>
        [System.Security.SecuritySafeCritical]
        public static T RZGetComponent<T>(
            this GameObject go, bool includeInactive) where T : Component
        {
            if (includeInactive)
            {
                var components = go.GetComponentsInChildren<T>(includeInactive);
                if (components.Length > 0 &&
                    ((Component)components[0]).gameObject == go) return components[0];
                else return default(T);
            }
            else
            {
                return go.GetComponent<T>();
            }
        }




        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its parents.
        /// </summary>
        public static Component RZGetComponentInParent(
            this GameObject go, Type t, bool includeInactive)
        {
            if (includeInactive)
            {
                var components = go.GetComponentsInParent(t, includeInactive);
                if (components.Length > 0) return components[0];
                else return null;
            }
            else
            {
                return go.GetComponentInParent(t);
            }
        }


        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its parents.
        /// </summary>
        public static T RZGetComponentInParent<T>(
            this GameObject go, bool includeInactive)
        {
            if (includeInactive)
            {
                var components = go.GetComponentsInParent<T>(includeInactive);
                if (components.Length > 0) return components[0];
                else return default(T);
            }
            else
            {
                return go.GetComponentInParent<T>();
            }
        }



        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its childrens.
        /// </summary>
        public static Component RZGetComponentInChildren(
            this GameObject go, Type t, bool includeInactive)
        {
            return go.GetComponentInChildren(t, includeInactive);
        }


        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its childrens.
        /// </summary>
        public static T RZGetComponentInChildren<T>(
            this GameObject go, bool includeInactive)
        {
            return go.GetComponentInChildren<T>(includeInactive);
        }




        /// <summary>
        /// Returns the string-hierarchy of gameObject.
        /// </summary>
        public static string RZGetHierarchy(this GameObject go)
        {
            if (go == null)
                return null;
            else
                return go.transform.RZGetHierarchy();
        }


    }

}
