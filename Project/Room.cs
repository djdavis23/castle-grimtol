using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }

    public Room LeaveRoom(string direction)
    {
      if (this.Exits.ContainsKey(direction))
      {
        return (Room)this.Exits[direction];
      }
      return null;
    }

    public Item TakeItem(string name)
    {
      Item myitem = Items.Find(item => item.Name == name);
      if (myitem == null || !myitem.CanTake) return null;
      Items.Remove(myitem);
      return myitem;
    }

    public void PrintItems()
    {
      if (Items.Count == 0)
      {
        System.Console.WriteLine("You do not see any items of interest in this room");
      }
      else
      {
        System.Console.WriteLine("You note the following items of interest in the room:");
        Items.ForEach(item =>
        {
          System.Console.WriteLine($"\t{item.Name} -- {item.Description}");
        });
      }
    }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
  }
}