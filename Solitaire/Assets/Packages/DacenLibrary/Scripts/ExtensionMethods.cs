using UnityEngine;
using System.Collections.Generic;

namespace Dacen.ExtensionMethods.Generic
{
    public static class ExtensionMethods_Generic
    {
        /// <summary>
        /// Shuffles a list via the Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this List<T> list)
        {
            int count = list.Count;
            int last = count - 1;
            for (int i = 0; i < last; ++i)
            {
                int r = UnityEngine.Random.Range(i, count);
                T tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
        }

        /// <summary>
        /// Takes elements from a list and removes them from this list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index">The index of the first element to grab</param>
        /// <param name="count">The total amount of elements to grab</param>
        /// <returns></returns>
        public static List<T> Grab<T>(this List<T> list, int index, int count)
        {
            List<T> grabbedElements = list.GetRange(index, count);
            list.RemoveRange(index, count);
            return grabbedElements;
        }

        /// <summary>
        /// Takes one element from a list and removes it from the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index">The index of the element to grab</param>
        /// <returns></returns>
        public static T Grab<T>(this List<T> list, int index)
        {
            T grabbedElement = list[index];
            list.RemoveAt(index);
            return grabbedElement;
        }

        /// <summary>
        /// Checks if the list contains a gameObject that has a component of a type attached to it.
        /// </summary>
        /// <typeparam name="T_component">The component-type to search for</typeparam>
        /// <typeparam name="T_list">The type of the list</typeparam>
        /// <param name="list"></param>
        /// <param name="componentDictionary">Dictionary of the elements in the list (key) that have the desired component attached to it (value)</param>
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T_component, T_list>(this List<T_list> list, out Dictionary<T_list, T_component> componentDictionary) where T_list : Component
        {
            componentDictionary = new Dictionary<T_list, T_component>();
            bool containsComponent = false;
            foreach (T_list element in list)
            {
                if (element.TryGetComponent(out T_component component))
                {
                    componentDictionary.Add(element, component);
                    containsComponent = true;
                }
            }
            return containsComponent;
        }

        /// <summary>
        /// Calculates which is the closest gameObject to this position in 2D-space and returns the component of this gameObject out of the dictionary.
        /// </summary>
        /// <typeparam name="T_key">Type if the keys in the dictionary</typeparam>
        /// <typeparam name="T_value">Type of the values in the dictionary</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="vector2">The position you want the closest component to</param>
        /// <returns>The keyValuePair with the closest key</returns>
        public static KeyValuePair<T_key, T_value> GetClosest<T_key, T_value>(this Dictionary<T_key, T_value> dictionary, Vector2 vector2) where T_key : Component
        {
            float shortestSqrDistance = float.PositiveInfinity;
            KeyValuePair<T_key, T_value> closest;

            foreach (KeyValuePair<T_key, T_value> keyValuePair in dictionary)
            {
                float sqrDistance = Mathf.Abs(Vector2.SqrMagnitude(vector2 - (Vector2) keyValuePair.Key.transform.position));
                if (sqrDistance < shortestSqrDistance)
                {
                    shortestSqrDistance = sqrDistance;
                    closest = keyValuePair;
                }
            }

            return closest;
        }
    }
}

namespace Dacen.ExtensionMethods.General
{
    public static class ExtensionMethods_General
    {
        /// <summary>
        /// Checks if this transform has children with the desired component attached to it.
        /// </summary>
        /// <typeparam name="T">The type of the desired components</typeparam>
        /// <param name="transform"></param>
        /// <param name="componentsInChildren">The components in the children of this transform, but not in this transform itself</param>
        /// <returns>Does this transform have children with the component attached to it?</returns>
        public static bool TryGetComponentsInChildrenExcludingSelf<T>(this Transform transform, out T[] componentsInChildren) where T : Component
        {
            List<T> componentsList = new List<T>();
            transform.GetComponentsInChildren(componentsList);
            if (transform.TryGetComponent(out T component))
                componentsList.Remove(component);

            if (componentsList.Count > 0)
            {
                componentsInChildren = componentsList.ToArray();
                return true;
            }
            else
            {
                componentsInChildren = null;
                return false;
            }
        }
    }
}