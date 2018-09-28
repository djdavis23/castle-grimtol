using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }


    public void PrintInventory()
    {
      if (Inventory.Count == 0)
      {
        System.Console.WriteLine("\nYour inventory is empty!");
      }
      else
      {
        System.Console.WriteLine("\nYOUR CURRENT INVENTORY:");
        Inventory.ForEach(item =>
        {
          System.Console.WriteLine($"\t{item.Name}");
        });
      }
    }
    public Player(string name)
    {
      PlayerName = name;
      Inventory = new List<Item>();
    }
  }
}