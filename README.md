# Inventory System Learning Journal

This log is a record of my progress with the inventory system and any issues I encounterd

## 2024-10-10

- Created the inventory item class with no errors.
- Created the modular inventory base class with no errors
- Created the player inventory class with no errors.

## 2024-10-11

- Added the UI elements into the scene.
  - I wanted to make the panel more round so I created my own background sprite. I had a few issues with making it look decent but resolved it in the end by trial and error.
  - I also encountered some issues thereafter when using the grid layout group as I had to mess around with the cell size and spacing values to make it not overflow off the edge of the panel due to the roundness.
- Following on, I made a prefab for each inventory slot as a button so it can be interacted with.
- Created an Inventory UI and slot script
  - I started creating the UI script before the slot script. When a slot gets added the UI script calls a function in the slot in order to update it with the correct values. I had a few NullReferenceExeption errors at first when setting this up but they got quicky resolved when I made the slot script.
  - When testing the scripts, I forgot to add a reference to the slot prefab in the Inventory UI script so I kept getting errors until I did this.

## 2024-10-15

- Created the github repository so I can work on the tutorials at home and at university - no issues to report.
- ADD PART ABOUT SETTING UP INTERACTIONS
