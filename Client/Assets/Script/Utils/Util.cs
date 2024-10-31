using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T FindChild<T>(GameObject parent, string name, bool recursive = false) where T : Component
    {
        if (recursive)
        {
            T[] children = parent.GetComponentsInChildren<T>();
            foreach (T child in children)
                if (child.transform.name == name)
                    return child;
        }
        else
        {
            int childCount = parent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parent.transform.GetChild(i);
                T component = childTransform.GetComponent<T>();
                if (component != null && component.name == name)
                    return component;
            }
        }
        return null;
    }
}
