using UnityEngine;

public static class DebugUtility
{
    public static void HandleErrorIfNullGetComponent<TO, TS>(Component component, Component source, GameObject objectSource)
    {
        if (!component) Debug.LogError("Error: Component of type " + typeof(TS) + " on GameObject " + source.gameObject.name +
                 " expected to find a component of type " + typeof(TO) + " on GameObject " + objectSource.name + ", but none were found.");
    }

    public static void HandleErrorIfNullFindObject<TO, TS>(UnityEngine.Object obj, Component source)
    {
        if(!obj) Debug.LogError("Error: Component of type " + typeof(TS) + " on GameObject " + source.gameObject.name +
                " expected to find an object of type " + typeof(TO) + " in the scene, but none were found.");
    }
    
}
