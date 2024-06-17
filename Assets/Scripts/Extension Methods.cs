using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class ExtensionMethods 
{
    public static TComponent CopyComponent<TComponent>(this GameObject destination, TComponent originalComponent) where TComponent : Component
    {
        Type componentType = originalComponent.GetType();

        Component copy;
        if (destination.TryGetComponent<TComponent>(out var component))
        {
            UnityEngine.Object.DestroyImmediate(component, true); //remove component of the same type if it already exist 
        }
        copy = destination.AddComponent(componentType);

        FieldInfo[] fields = componentType.GetFields();
        foreach (FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(originalComponent));
        }

        return copy as TComponent;
    }
}
