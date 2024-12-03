# InventoryItem / ItemStats

### Declaration
public Dictionary<string, int> ItemStats();

### Returns
```Dictionary<string, int>``` where string is the name of a stat and int is the stat.

### Description
Grabs the stat info off of an item.

The below example takes in an ```InvetoryItem``` and loops through its stats adding it to ```itemStats```. It then displays that on a ```TextMeshProUGUI``` to the user.
```cs
public string itemStats;
[SerializeField] private TextMeshProUGUI stats;

private void GetStats(InventoryItem item)
{
  foreach (KeyValuePair<string, int> kvp in item.ItemStats()) itemStats += $"{kvp.Key}: {kvp.Value}\n";
  stats.text = itemStats;
}
```
