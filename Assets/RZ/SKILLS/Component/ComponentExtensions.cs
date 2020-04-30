using System;

// namespace RZ
namespace UnityEngine
{
    /// <summary>
    /// Extensions for 'UnityEngine.Component'
    /// </summary>
    public static class ComponentExtensions
    {
        // /// <summary>
        // /// Returns the component of Type type if the game object has one attached, null
        // /// </summary>
        // public static Component RZGetComponent(this Component c, string type)
        // { return c.GetComponent(type); }


        // /// <summary>
        // /// Returns the component of Type type if the game object has one attached, null
        // /// </summary>
        // public static Component RZGetComponent(this Component c, Type type)
        // { return c.GetComponent(type); }


        // /// <summary>
        // /// Returns the component of Type type if the game object has one attached, null
        // /// </summary>
        // public static T RZGetComponent<T>(this Component c)
        // { return c.GetComponent<T>(); }




        /// <summary>
        /// Returns the component of Type type if the game object has one attached, null
        /// </summary>
        public static Component RZGetComponent(this Component c, Type t, bool includeInactive)
        {
            if (includeInactive)
            {
                var components = c.GetComponentsInChildren(t, includeInactive);
                if (components.Length > 0 &&
                    components[0].gameObject == c.gameObject)
                    return components[0];
                else return null;
            }
            else
            {
                return c.GetComponent(t);
            }
        }


        /// <summary>
        /// Returns the component of Type type if the game object has one attached, null
        /// </summary>
        public static T RZGetComponent<T>(
            this Component c, bool includeInactive) where T : Component
        {
            if (includeInactive)
            {
                var components = c.GetComponentsInChildren<T>(includeInactive);
                if (components.Length > 0 &&
                    ((Component)components[0]).gameObject == c.gameObject) return components[0];
                else return default(T);
            }
            else
            {
                return c.GetComponent<T>();
            }
        }




        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its parents.
        /// </summary>
        public static Component RZGetComponentInParent(
            this Component c, Type t, bool includeInactive)
        {
            if (includeInactive)
            {
                var components = c.GetComponentsInParent(t, includeInactive);
                if (components.Length > 0) return components[0];
                else return null;
            }
            else
            {
                return c.GetComponentInParent(t);
            }
        }


        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its parents.
        /// </summary>
        public static T RZGetComponentInParent<T>(
            this Component c, bool includeInactive)
        {
            if (includeInactive)
            {
                var components = c.GetComponentsInParent<T>(includeInactive);
                if (components.Length > 0) return components[0];
                else return default(T);
            }
            else
            {
                return c.GetComponentInParent<T>();
            }
        }




        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its childrens.
        /// </summary>
        public static Component RZGetComponentInChildren(
            this Component c, Type t, bool includeInactive)
        {
            return c.GetComponentInChildren(t, includeInactive);
        }


        /// <summary>
        /// Returns the component of Type type in the GameObject or any of its childrens.
        /// </summary>
        public static T RZGetComponentInChildren<T>(
            this Component c, bool includeInactive)
        {
            return c.GetComponentInChildren<T>(includeInactive);
        }




        /// <summary>
        /// Returns the string-hierarchy of component.
        /// </summary>
        public static string RZGetHierarchy(this Component c)
        {
            if (c == null)
                return null;
            else
                return c.transform.RZGetHierarchy() + "/" + c.GetType().ToString();
        }

    }

}
