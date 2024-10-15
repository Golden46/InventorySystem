using UnityEngine;

/*
This script / class is essentially a blueprint for all
interactable scripts created. It is abstract because
each interactable object has its own script which
overrides these functions because they all need
different functionality.
*/
public abstract class Interactable : MonoBehaviour
{
    public virtual void Awake()
    {
        gameObject.layer = 6;
    }
    
    /// <summary>
    /// Called when the user presses the interact button while focusing
    /// on an item with the interactable layer.
    /// </summary>
    public abstract void OnInteract();

    /// <summary>
    /// Called when the user hovers their mouse
    /// on an item with the interactable layer.
    /// </summary>
    public abstract void OnFocus();

    /// <summary>
    /// Called when the user stops hovering their
    /// mouse over the item they were previously
    /// foccused on.
    /// </summary>
    public abstract void OnLoseFocus();
}
