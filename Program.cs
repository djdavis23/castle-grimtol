using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      System.Console.WriteLine("Screen width: ", Console.BufferWidth);
      Console.CursorVisible = true;
      Console.CursorSize = 100;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("\n Greetings Noble Knight!  What is your name?  ");
      string name = Console.ReadLine();
      Console.Write($"\n Welcome Sir {name}!  You have arrived just in time.  Our forces are failing\n and the enemy grows more powerful everyday.  I fear that if we don't act now, our people will\n be driven from their homes by the Dark Lord Grimtol.  Our fellowship of warriors grows smaller\n with each cowardly attack by Lord Grimtol's legions of orcs.  These dark times have left us\n with one final course of action: we must cut off the head of the snake!  We must assassinate\n the Dark Lord Grimtol.");
      Console.Write("\n\n Our sources have identifed a small tunnel that leads into the rear of\n the castle.  We need a brave and strong knight such as yourself to enter the castle and slay\n the Dark Lord.");
      Console.Write($"\n\n Will you help us Sir {name}?  ");
      string response = Console.ReadLine().ToLower();
      if (response == "y" || response == "yes")
      {
        Game game = new Game(name);
        game.StartGame();
      }
      else
      {
        Console.WriteLine("\n\n Then be on your way coward!");
      }
      Console.ResetColor();
    }
  }
}
