using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class ExtensionMethods 
{
    //public static TComponent CopyComponent<TComponent>(this GameObject destination, TComponent originalComponent) where TComponent : Component
    //{
    //    Type componentType = originalComponent.GetType();

    //    Component copy;
    //    if(destination.TryGetComponent<TComponent>(out var component))
    //    {
    //        //copy = componentType; //if component already exist then use it instead
    //    }
    //    else
    //    {
    //        copy = destination.AddComponent(componentType); //add component type if it doesn't exist 
    //    }

    //    FieldInfo[] fields = componentType.GetFields();
    //    foreach(FieldInfo field in fields)
    //    {
    //        //field.SetValue(copy, field.GetValue(originalComponent));
    //    }

    //    return copy as TComponent;
    // }
}
