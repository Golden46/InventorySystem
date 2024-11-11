# Inventory System Tutorial
> This tutorial will cover how to create this inventory system for yourself and it will fully explain each section in detail.

## Prerequisites
Before being able to follow this tutorial you will need to have the correct version of Unity installed and a development IDE. 
This project was created using Unity 2022.3.46f1 and Rider, although you can use any code editor you like.

If you need help installing this version of unity please go [here](INSTALLUNITY.md) and follow the steps as needed.

Additionally, you will need basic understanding of a few intermediate programming concepts:
- Scriptable Objects
- Dictionaries
- Abstract Classes
- Inheritance

## Objectives

## Setting up the scene
First, you will need to create a Unity 3D project. This was created using the Universal 3D template with the Universal Rednder
pipeline but it can be created the same using the Built-In Render Pipeline too.

## [Inventory Item](InventoryItem.md)
To start off, we will create the base class for every item in our game. This class is going to contain all of the important item information.

```cs
using UnityEngine;
using System.Collections.Generic;

public abstract class InventoryItem : ScriptableObject
{
    [Header("Item Info")]
    public int id; 
    public string itemName; 
    public Sprite itemIcon;

    [Header("Stack Information")]
    public bool isStackable; 
    public int maxStackSize;

    public GameObject prefab;

    public abstract Dictionary<string, int> ItemStats();
}
```
f
