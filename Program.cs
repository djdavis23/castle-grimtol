using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      Console.CursorVisible = true;
      Console.CursorSize = 100;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("\nGreetings Noble Knight!  What is your name?  ");
      string name = Console.ReadLine();
      Console.WriteLine($"\nWelcome Sir {name}!  You have arrived just in time.  Our forces are failing and the enemy grows more powerful everyday.  I fear that if we don't act now, our people will be driven from their homes by the Dark Lord Grimtol.  Our fellowship of warriors grows smaller with each cowardly attack by Lord Grimtol's legions of orcs.  These dark times have left us with one final course of action: we must cut off the head of the snake!  We must assassinate the Dark Lord Grimtol.");
      Console.WriteLine("\nOur sources have identifed a small tunnel that leads into the rear of the castle.  We need a brave and strong knight such as yourself to enter the castle and slay the Dark Lord.");
      Console.Write($"\n\nWill you help us Sir {name}?  ");
      string response = Console.ReadLine().ToLower();
      if (response == "y" || response == "yes")
      {
        Game game = new Game(name);
        game.StartGame();
      }
      else
      {
        Console.WriteLine("\nThen be on your way coward!");
      }
      Console.ResetColor();
    }
  }
}
