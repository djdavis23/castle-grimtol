using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public bool Visited { get; set; }

    public bool DoorLocked { get; set; }

    public Room LeaveRoom(string direction)
    {
      if (!this.Exits.ContainsKey(direction))
      {
        System.Console.WriteLine("\n OUCH - you walked into a wall clumsy knight!");
        return this;
      }
      Room newRoom = (Room)this.Exits[direction];
      if (newRoom.DoorLocked)
      {
        System.Console.WriteLine("\n The door is locked!");
        return this;
      }
      return newRoom;
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
        System.Console.WriteLine("\n You do not see any other items of interest in this room");
      }
      else
      {
        System.Console.WriteLine("\n You note the following items of interest in the room:");
        Items.ForEach(item =>
        {
          System.Console.WriteLine($"\t{item.Name} -- {item.Description}");
        });
      }
    }

    public Room(string name, string description, bool doorlocked = false)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      Visited = false;
      DoorLocked = doorlocked;
    }
  }
}