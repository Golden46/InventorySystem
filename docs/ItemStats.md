# ItemStats

### Declaration
public Dictionary<string, int> ItemStats();

### Returns
Dictionary<string, int> where string is the name of a stat and int is the stat.

### Description
Grabs the stat info off of an item.

The below example is loops through each stat from an item and displays it on a text component.
```cs
itemStats = "";
foreach (KeyValuePair<string, int> kvp in item.ItemStats()) itemStats += $"{kvp.Key}: {kvp.Value}\n";
stats.text = itemStats;
```
