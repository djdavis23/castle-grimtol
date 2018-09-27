using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Item : IItem
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanTake { get; set; }

    public Item(string name, string description, bool canTake = true)
    {
      Name = name;
      Description = description;
      CanTake = canTake;
    }
  }
}