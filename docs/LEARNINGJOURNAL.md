# Inventory System Learning Journal
This log is a record of my progress with the inventory system and any issues I encounterd


## 2024-10-10
- Created the inventory item class with no errors.
- Created the modular inventory base class with no errors
- Created the player inventory class with no errors.

## 2024-10-11
### Added the UI elements into the scene.
- I wanted to make the panel more round so I created my own background sprite. I had a few issues with making it look decent but resolved it in the end by trial and error.
- I also encountered some issues thereafter when using the grid layout group as I had to mess around with the cell size and spacing values to make it not overflow off the edge of the panel due to the roundness.
  
### I made a prefab for each inventory slot as a button so it can be interacted with.
  - I forgot to install TextMeshPro so I did that to make the button.
  
### Created an Inventory UI and slot script.
  - I started creating the UI script before the slot script. When a slot gets added the UI script calls a function in the slot in order to update it with the correct values. I had a few NullReferenceExeption errors at first when setting this up but they got quicky resolved when I made the slot script.
  - When testing the scripts, I forgot to add a reference to the slot prefab in the Inventory UI script so I kept getting errors until I did this.


## 2024-10-15
- Created the github repository so I can work on the tutorials at home and at university - no issues to report.
  
### Set up the mouse looking from my movement script.
The camera movement from my script was orginally using cinemachine so the player movement script had no logic for doing this.
- Added in logic to move the camera up, down, left, and right but then I realised that if someone is looking left and right then the body should move and not the head (camera).
    - I setup the new input system with this so that I could read the players mouse movement as a Vector2 which worked perfectly. It read the value but for some reason the camera never moved and it seemed like it was fighting to stay in place. This was because when I rotated the cameras transform, I had the Y value of the Quaternion.Euler set to 0. The cameras Y rotation in the unity scene is set to 90 to be on the same axis as the player so I changed this and the rotation then worked fine.
    - Now, the WASD keys for movement (which were also setup without issue using unitys new input system) were moving my character the wrong way. Upon investigation, I found that I was moving the character using the WASD keys relative to the cameras position. Now that I am rotating the player and not just the camera I changed it to rotated the player locally and it works without issue.

### Set up interactions.
I already had interactions from a script I already created so I had no issues with that.
- I did have to make an ItemInteract script which is placed on each item you can pick up. This was very easy to do and had no issues. 
- Upon setting up the interactions I did forget to create an Interactable layer for the objects to have so at first the interactions didn't work.
- I had a lot of trouble setting up the new input system for the interact key to subscribe to the performed event action. I was subscribing to a function in the first person controller script and I am using a seperate script for the input manager. I was referencing the first person controller script wrong so it was coming back as null. I changed this to use FindObjectOfType because there will be only one FPS and now it works perfectly.


## 2024-10-17
- Using the new input system, set up a way to toggle the inventory UI with no issues
  - Had to add a way to disable camera movement and interaction when in the inventory otherwise you could pick up items and look around while trying to navigate the inventory which I do not want.


## 2024-10-21

### Stat panel.
- I wanted to create a way to view the stats of each item in the players inventory.
  - My first issue was reading the stats from the item. They way I have the system setup meant I had to change a few things. The inventory is a list of InventoryItems but each item type e.g. "Sword", "Axe" etc... is its own class which derives from the InventoryItem class. The way I got around this was by making the InventoryItem class abstract and making a function I called ItemStats which returns a Dictionary<string, int>, where string is the name of the stat and int is the stat value. Then in each class for the items I override this function and return a dictionary with all the stat names and values. In the inventoryslot script I can call this function on each item and display the stats modularly now.
  - I also had an issue with the Z-index of the UI Panel that the stats appear on. In unity you can't change the Z-index of UI componenets. The way I managed to resolve this was by changing the parent of the statPanel from the slot to the slots parent and setting it as the last sibling meaning it will always appear on top. Then when the mouse stops hovering over the slot it becomes the child of the slot again and hides.

 
## 2024-10-29

### Swapping items in the inventory
- First, I wanted to just get dragging working and figure out the swapping after. To drag the icon I wanted to make it so you the slot stays in place but the icon gets dragged. I thought of two ways to do this. The first way is to hide the icon on the slot and duplicate it to follow the mouse. The way I did it was to make the icon a child of the slot and use that to follow the mouse.
  - I coded it so this would work perfectly, however, I forgot to actuall extract the icon from the button and make it a child on the prefab so I kept getting really weird bugs when dragging. For some reaason it took me ages to figure out why it wasn't working but when I did and I made the icon a child it worked perfectly.
- Then, I did the coding for swapping the icons, this was very easy as I already had the code in place from when I initialise the slot. I just had to call it again on the from and to slot but just with the copied data. I created this functuion in the inventoryUI script and it looked as though it was working flawlessly.
  - This came with a few bugs when I was tesing: If I switched slots around and then picked up an item - the order reset, and if I dropped an item and picked another one up it would add the item picked up and the one I dropped back to the inventory. This is due to the way I update the inventory; Every time the inventory item gets picked up it adds to a list and then updates the UI by adding it on to the end. This means that every time I do something which changes the inventory I need to update the list. In the Inventory script I made a remove item and swap item function. The remove item function just removed the item from the list, which was easy. For the swap item functon I had to get the index of the from and to item and then swap them. This was a little harder to get my head around but still relativaly simple.
