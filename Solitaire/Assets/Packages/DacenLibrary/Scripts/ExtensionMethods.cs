using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using Dacen.Utility;

namespace Dacen.ExtensionMethods.Generic
{
    public static class ExtensionMethods_Generic
    {
        /// <summary>
        /// Returns a random element from this list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Capacity - 1)];
        }

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
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T_component, T_list>(this List<T_list> list) where T_list : Component
        {
            foreach (T_list element in list)
                if (element.GetComponent<T_component>() != null)
                    return true;
            return false;
        }

        /// <summary>
        /// Checks if the list contains a gameObject that has a component of a type attached to it.
        /// </summary>
        /// <typeparam name="T">The component-type to search for</typeparam>
        /// <param name="list"></param>
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T>(this List<GameObject> list)
        {
            foreach (GameObject element in list)
                if (element.GetComponent<T>() != null)
                    return true;
            return false;
        }

        /// <summary>
        /// Checks if the list contains a gameObject that has a component of a type attached to it.
        /// </summary>
        /// <typeparam name="T_component">The component-type to search for</typeparam>
        /// <typeparam name="T_list">The type of the list</typeparam>
        /// <param name="list"></param>
        /// <param name="elementsWithComponent">List of components that are attached to gameObjects that have the desired component attached to it</param>
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T_list>(this List<T_list> list, Type componentType, out List<T_list> elementsWithComponent) where T_list : Component
        {
            elementsWithComponent = new List<T_list>();
            bool containsComponent = false;
            foreach (T_list element in list)
            {
                if (element.GetComponent(componentType) != null)
                {
                    elementsWithComponent.Add(element);
                    containsComponent = true;
                }
            }
            return containsComponent;
        }

        /// <summary>
        /// Checks if the list contains a gameObject that has a component of a type attached to it.
        /// </summary>
        /// <typeparam name="T_component">The component-type to search for</typeparam>
        /// <typeparam name="T_list">The type of the list</typeparam>
        /// <param name="list"></param>
        /// <param name="components">List of the desired components</param>
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T_component, T_list>(this List<T_list> list, out List<T_component> components) where T_list : Component
        {
            components = new List<T_component>();
            bool containsComponent = false;
            foreach (T_list element in list)
            {
                if (element.TryGetComponent(out T_component component))
                {
                    components.Add(component);
                    containsComponent = true;
                }
            }
            return containsComponent;
        }

        /// <summary>
        /// Checks if the list contains a gameObject that has a component of a type attached to it.
        /// </summary>
        /// <typeparam name="T">The component-type to search for</typeparam>
        /// <param name="list"></param>
        /// <param name="gameObjectsWithComponent">List of elements that have the desired component attached to it</param>
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T>(this List<GameObject> list, out List<GameObject> gameObjectsWithComponent)
        {
            gameObjectsWithComponent = new List<GameObject>();
            bool containsComponent = false;
            foreach (GameObject go in list)
            {
                if (go.GetComponent<T>() != null)
                {
                    gameObjectsWithComponent.Add(go);
                    containsComponent = true;
                }
            }
            return containsComponent;
        }

        /// <summary>
        /// Checks if the list contains a gameObject that has a component of a type attached to it.
        /// </summary>
        /// <typeparam name="T">The component-type to search for</typeparam>
        /// <param name="list"></param>
        /// <param name="gameObjectsWithComponent">List of the desired components</param>
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T>(this List<GameObject> list, out List<T> gameObjectsWithComponent)
        {
            gameObjectsWithComponent = new List<T>();
            bool containsComponent = false;
            foreach (GameObject go in list)
            {
                if (go.TryGetComponent(out T component))
                {
                    gameObjectsWithComponent.Add(component);
                    containsComponent = true;
                }
            }
            return containsComponent;
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
        /// Checks if the list contains a gameObject that has a component of a type attached to it.
        /// </summary>
        /// <typeparam name="T_component">The component-type to search for</typeparam>
        /// <param name="list"></param>
        /// <param name="componentDictionary">Dictionary of the gameObjects (key) that have the desired component attached to it (value)</param>
        /// <returns>Does a gameObject in this list have the desired component attached to it?</returns>
        public static bool ContainsComponent<T_component>(this List<GameObject> list, out Dictionary<GameObject, T_component> componentDictionary)
        {
            componentDictionary = new Dictionary<GameObject, T_component>();
            bool containsComponent = false;
            foreach (GameObject go in list)
            {
                if (go.TryGetComponent(out T_component component))
                {
                    componentDictionary.Add(go, component);
                    containsComponent = true;
                }
            }
            return containsComponent;
        }

        /// <summary>
        /// Calculates which is the closest gameObject to a position in 3D-space and returns the component of this gameObject out of the list.
        /// </summary>
        /// <typeparam name="T">Type of the list has to be a component</typeparam>
        /// <param name="vector3">The position you want the closest component to</param>
        /// <param name="components">List of components to get the closest out of</param>
        /// <returns>The component of the closest gameObject</returns>
        public static T GetClosest<T>(this List<T> components, Vector3 vector3) where T : Component
        {
            float shortestSqrDistance = float.PositiveInfinity;
            T closestComponent = null;

            foreach (T component in components)
            {
                float sqrDistance = Mathf.Abs(Vector3.SqrMagnitude(vector3 - component.transform.position));
                if (sqrDistance < shortestSqrDistance)
                {
                    shortestSqrDistance = sqrDistance;
                    closestComponent = component;
                }
            }

            return closestComponent;
        }

        /// <summary>
        /// Calculates which is the closest gameObject to this position in 2D-space and returns the component of this gameObject out of the list.
        /// </summary>
        /// <typeparam name="T">Type of the list has to be a component</typeparam>
        /// <param name="vector2">The position you want the closest component to</param>
        /// <param name="components">List of components to get the closest out of</param>
        /// <returns>The component of the closest gameObject</returns>
        public static T GetClosest<T>(this List<T> components, Vector2 vector2) where T : Component
        {
            float shortestSqrDistance = float.PositiveInfinity;
            T closestComponent = null;

            foreach (T component in components)
            {
                float sqrDistance = Mathf.Abs(Vector2.SqrMagnitude(vector2 - (Vector2)component.transform.position));
                if (sqrDistance < shortestSqrDistance)
                {
                    shortestSqrDistance = sqrDistance;
                    closestComponent = component;
                }
            }

            return closestComponent;
        }

        /// <summary>
        /// Calculates which is the closest gameObject to this position in 3D-space and returns the component of this gameObject out of the dictionary.
        /// </summary>
        /// <typeparam name="T_key">Type if the keys in the dictionary</typeparam>
        /// <typeparam name="T_value">Type of the values in the dictionary</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="vector3">The position you want the closest component to</param>
        /// <returns>The keyValuePair with the closest key</returns>
        public static KeyValuePair<T_key, T_value> GetClosest<T_key, T_value>(this Dictionary<T_key, T_value> dictionary, Vector3 vector3) where T_key : Component
        {
            float shortestSqrDistance = float.PositiveInfinity;
            KeyValuePair<T_key, T_value> closest;

            foreach (KeyValuePair<T_key, T_value> keyValuePair in dictionary)
            {
                float sqrDistance = Mathf.Abs(Vector3.SqrMagnitude(vector3 - keyValuePair.Key.transform.position));
                if (sqrDistance < shortestSqrDistance)
                {
                    shortestSqrDistance = sqrDistance;
                    closest = keyValuePair;
                }
            }

            return closest;
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

namespace Dacen.ExtensionMethods.Vectors
{
    public static class ExtensionMethods_Vectors
    {
        /// <summary>
        /// Check if a position is inside bounds (3-dimensional).
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="bounds">The bounds the position is possibly in.</param>
        /// <returns></returns>
        public static bool IsInBounds(this Vector3 vector3, Bounds bounds)
        {
            if (vector3.x >= bounds.min.x && vector3.x <= bounds.max.x &&
                vector3.y >= bounds.min.y && vector3.y <= bounds.max.y &&
                vector3.z >= bounds.min.z && vector3.z <= bounds.max.z)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if a position is inside bounds (2-dimensional).
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="bounds">The bounds the position is possibly in.</param>
        /// <returns></returns>
        public static bool IsInBounds(this Vector2 vector2, Bounds bounds)
        {
            if (vector2.x >= bounds.min.x && vector2.x <= bounds.max.x &&
                vector2.y >= bounds.min.y && vector2.y <= bounds.max.y)
            {
                return true;
            }
            return false;
        }
    }
}

namespace Dacen.ExtensionMethods.General
{
    public static class ExtensionMethods_General
    {
        /// <summary>
        /// Gets all the desired components of the children of this transform, but not of this transform itself.
        /// </summary>
        /// <typeparam name="T">The type of the desired components</typeparam>
        /// <param name="transform">The transform to check for the components in children</param>
        /// <returns>Array of the desired components</returns>
        public static T[] GetComponentsInChildrenExcludingSelf<T>(this Transform transform) where T : Component
        {
            List<T> componentsList = new List<T>();
            transform.GetComponentsInChildren(componentsList);
            if (transform.TryGetComponent(out T component))
                componentsList.Remove(component);

            if (componentsList.Count > 0)
                return componentsList.ToArray();
            else
                return null;
        }

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

        public static string GetHoursAndMinutesInDigitalFormat(this TimeSpan timeSpan)
        {
            string hours   = DacenUtility.ConvertToTwoDigits(timeSpan.Hours);
            string minutes = DacenUtility.ConvertToTwoDigits(timeSpan.Minutes);
            return hours + ":" + minutes;
        }
    }
}