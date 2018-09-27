using System.Collections.Generic;
using System;

namespace CastleGrimtol.Project
{
  public class Game : IGame
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; set; }
    public Dictionary<string, string> Commands { get; set; }
    public List<string> Directions { get; set; }
    public bool UniformOn { get; set; }

    public void StartGame()
    {
      Setup();
      while (Playing)
      {
        Console.Write($"\nWhat is your next move Sir {CurrentPlayer.PlayerName}?  ");
        GetUserInput();
      }
      Console.Clear();
      Console.WriteLine("\nThank you for your valient efforts brave knight!  Safe journeys!");
    }

    public void Setup()
    {
      //BUILD ROOMS
      Room Entrance = new Room("Entrance", "Your are standing at the rear entrance to Castle Grimtol.  Down a dark tunnel to the East, you see the flickering, faint glow of a torch.");
      Room HallWest = new Room("Hallway", "You find yourself in a small hall.  You see passages to the North, East, West and South.");
      Room Barracks = new Room("Barracks", "You see a room with several sleeping guards.  The rooms smells of sweaty men and beer farts.");
      Room Courtyard = new Room("Castle Courtyard", "You step into a large castle courtyard.  There is flowing fountain in the middle of the grounds and a few guards patrolling the area.  You see corridors to the North and South.");
      Room CaptQtrs = new Room("Captain's Quarters", "As you approach the Captain's Quarters, you swallow hard and realize your lips are dry.  Stepping into the room you see a few small tables and maps of the countryside sprawled out.  There are doors on the North and East walls.");
      Room HallSouth = new Room("Hallway", "You find yourself in a small hall.  There does not appear to be anything of interest here.  You see passages to the East and West and an open area to the North.");
      Room GuardRoom = new Room("Guard Room", "Pushing open the door of the guard room, you look around and notice the room is empty.  There are a few small tools in the corner and a chair propped against the wall near a door on the North wall that likely leads to the dungeon.");
      Room Dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon, you notice a harsh chill to the air.  Landing at the base of the stairs, you find the what appears to be the remains of a prisoner.  You also notice a man sitting in shackles. As you approach him you notice a small lock binding him to the wall with chains. As you near the prisoner his face turns to a deep frown.... (PRISONER) 'You look familiar, Hey thats right I know you from the village. Have you seriously turned your back on us and joined this squad of goons?' He sighs defeated...");
      Room HallNorth = new Room("Hallway", "You find yourself in a small hall. There does not appear to be anything of interest here.  You see passages to the North and East and an open area to the South.");
      Room ThroneRoom = new Room("Throne Room", "As you unlock the door and swing it wide you see an enormous hall stretching out before you. At the opposite end of the hall sitting on his throne you see the Dark Lord. The Dark Lord shouts at you demanding to know why you dared to interrupt him during his Ritual of Evil Summoning.  You turn to run, but the door slams shut and locks. Dumbfounded you mutter an incoherent response. Becoming more enraged the Dark Lord complains that you just ruined his concentration and he will now have to start the ritual over... Quickly striding towards you he smirks and says 'at least I know have a sacrificial volunteer.' He plunges his jewel encrusted dagger into your heart and your world slowly fades away.");
      Room SquireTower = new Room("Squire Tower", "Ascending the stairs to the squire tower, you see a messenger pass out drunk in his bed.  His messenger overcoat is hanging from his bed post.  There are doors on the West and North walls.");
      Room WarRoom = new Room("War Room", "Stepping into the War Room, you see several maps spread across tables.  One the maps, several villages have been marked for 'purification.'  You also notice several dishes of prepared food to the side and steins filled with beer... perhaps the war council will be meeting soon?");

      //ADD EXITS TO ROOMS
      Entrance.Exits.Add("EAST", HallWest);
      HallWest.Exits.Add("NORTH", Barracks);
      HallWest.Exits.Add("SOUTH", CaptQtrs);
      HallWest.Exits.Add("EAST", Courtyard);
      Barracks.Exits.Add("SOUTH", HallWest);
      Courtyard.Exits.Add("WEST", HallWest);
      Courtyard.Exits.Add("SOUTH", HallSouth);
      Courtyard.Exits.Add("NORTH", HallNorth);
      CaptQtrs.Exits.Add("NORTH", HallWest);
      CaptQtrs.Exits.Add("EAST", HallSouth);
      HallSouth.Exits.Add("WEST", CaptQtrs);
      HallSouth.Exits.Add("NORTH", Courtyard);
      HallSouth.Exits.Add("EAST", GuardRoom);
      GuardRoom.Exits.Add("WEST", HallSouth);
      GuardRoom.Exits.Add("NORTH", Dungeon);
      Dungeon.Exits.Add("SOUTH", GuardRoom);
      HallNorth.Exits.Add("NORTH", ThroneRoom);
      HallNorth.Exits.Add("EAST", SquireTower);
      HallNorth.Exits.Add("SOUTH", Courtyard);
      SquireTower.Exits.Add("WEST", HallNorth);
      SquireTower.Exits.Add("NORTH", WarRoom);
      WarRoom.Exits.Add("SOUTH", SquireTower);

      //BUILD ITEMS
      Item bed = new Item("BED", "The bed closest to you is empty", false);
      Item uniform = new Item("UNIFORM", "there are several uniforms tossed about the room.  One appears to be about your size.");
      Barracks.Items.Add(bed);
      Barracks.Items.Add(uniform);
      Item note = new Item("NOTE", "On one of the tables you see  note from the captain instructing the Dark Lord's chief bodyguard to report to the Captain's Quarters immediately!");
      Item key = new Item("KEY", "Hanging on the wall is a silver key.");
      Item vial = new Item("VIAL", "Sitting on the Captain's desk is a small vial containing a green liquid.  Perhaps a poison or magic potion?");
      CaptQtrs.Items.Add(note);
      CaptQtrs.Items.Add(key);
      CaptQtrs.Items.Add(vial);
      Item hammer = new Item("HAMMER", "Amongst the tools you find a well-worn, but sturdy sledgehammer");
      GuardRoom.Items.Add(hammer);
      Item Lock = new Item("LOCK", "a broken pad lock");
      Dungeon.Items.Add(Lock);
      Item overcoat = new Item("OVERCOAT", "a medium-sized black overcoat with large pockets");
      SquireTower.Items.Add(overcoat);
      Item window = new Item("WINDOW", "an open window in the east wall", false);
      WarRoom.Items.Add(window);

      //BUILD VALID COMMAND LIST
      Commands.Add("GO", "<direction> --  moves the player in the specified direction");
      Commands.Add("USE", "<ItemName> -- uses an item in a room or from the player inventory");
      Commands.Add("TAKE", "<ItemName> -- remove an item from the room and places it in the player's inventory");
      Commands.Add("LOOK", "-- redisplays the description of the current room");
      Commands.Add("INVENTORY", "-- displays the player's inventory");
      Commands.Add("HELP", "-- displays valid game commands");
      Commands.Add("QUIT", "-- quits the current game");

      //ADD VALID DIRECTIONS      
      Directions.Add("NORTH");
      Directions.Add("EAST");
      Directions.Add("SOUTH");
      Directions.Add("WEST");


      //PLACE PLAYER AT THE ENTRANCE OF THE CASTLE TO INITIATE PLAY
      Playing = true;
      UniformOn = false;
      Console.WriteLine("\nThank you noble knight!  An orc who is symathetic to our cause will lead you to the rear entrance to Castle Grimtol   Once you sneak through the tunnel, you will need to find a way to disguise yourself and kill the Dark Lord.  This grave mission will require all of your wit and cunning.");
      Console.WriteLine($"\nGood luck Brave {CurrentPlayer.PlayerName}");
      CurrentRoom = Entrance;
      Look();
    }

    public void GetUserInput()
    {
      //split input string to distiguish command from option
      string[] input = Console.ReadLine().ToUpper().Split(' ');
      string command = input[0];
      string option = "";
      if (input.Length >= 2) option = input[1];

      //run appropriate method based on input command
      switch (command)
      {
        case "GO":
          Go(option);
          break;

        case "USE":
          UseItem(option);
          break;

        case "TAKE":
          TakeItem(option);
          break;

        case "LOOK":
          Look();
          break;

        case "INVENTORY":
          Inventory();
          break;

        case "HELP":
          Help();
          break;

        case "QUIT":
          Quit();
          break;

        default:
          Console.WriteLine("\nDid not recognize that command.");
          break;
      }
    }

    public void Go(string direction)
    {
      if (Directions.Contains(direction))
      {
        Room newRoom = CurrentRoom.LeaveRoom(direction);
        if (newRoom == null)
        {
          System.Console.WriteLine("\nOUCH - you walked into a wall clumsy knight!");
        }
        else
        {
          CurrentRoom = newRoom;
          Look();
        }
      }
      else
      {
        Console.WriteLine($"\n{direction} is not a valid direction.");
      }
    }

    public void UseItem(string itemName)
    {
      Item myitem = CurrentPlayer.Inventory.Find(item => item.Name == itemName);
      if (myitem == null)
      {
        myitem = CurrentRoom.Items.Find(item => item.Name == itemName);
      }
      if (myitem == null)
      {
        System.Console.WriteLine("You do not have that item available for use.");
        return;
      }

      switch (myitem.Name)
      {
        case "BED":

          break;
        case "UNIFORM":
          UniformOn = true;
          CurrentPlayer.Inventory.Remove(myitem);
          System.Console.WriteLine("\nYou put on the guard's uniform ...it fits well.  Now you will be more covert.");
          break;
        case "KEY":
          break;
        case "VIAL":
          break;
        case "NOTE":
          break;
        case "HAMMER":
          break;
        case "LOCK":
          break;
        case "OVERCOAT":
          break;
        case "WINDOW":
          break;
      }
    }

    public void TakeItem(string itemName)
    {
      Item item = CurrentRoom.TakeItem(itemName);
      if (item == null)
      {
        Console.WriteLine($"\nCannot take {itemName} from this room");
      }
      else
      {
        CurrentPlayer.Inventory.Add(item);
        Console.WriteLine($"\n{item.Name} added to your inventory.");
      }
    }

    public void Look()
    {
      Console.WriteLine($"\n{CurrentRoom.Name}:  {CurrentRoom.Description}");
      CurrentRoom.PrintItems();
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count == 0)
      {
        Console.WriteLine("\nYour inventory is empty!");
      }
      else
      {
        Console.WriteLine("\nYOUR CURRENT INVENTORY:");
        CurrentPlayer.Inventory.ForEach(item =>
        {
          Console.WriteLine($"\t{item.Name}");
        });
      }
    }

    public void Help()
    {
      Console.WriteLine("\nVALID GAME COMMANDS: ");
      foreach (KeyValuePair<string, string> command in Commands)
      {
        Console.WriteLine($"\t{command.Key} {command.Value}");
      }
    }

    public void Quit()
    {
      Console.Write("\nAre you sure you want to give up?  ");
      string confirm = Console.ReadLine().ToLower();
      if (confirm == "y" || confirm == "yes") Playing = false;
    }

    public void Reset()
    {
      CurrentPlayer.Inventory.RemoveAll(item => item is Item);
      Directions.RemoveAll(direction => direction is string);
      StartGame();
    }

    private void EndGame(string message)
    {
      Console.WriteLine($"\n{message}");
      Console.Write($"\nWould you like to play again Sir {CurrentPlayer.PlayerName}?  ");
      string choice = Console.ReadLine().ToLower();
      if (choice == "y" || choice == "yes") Reset();
      else Quit();
    }

    public Game(string name)
    {
      CurrentPlayer = new Player(name);
      Commands = new Dictionary<string, string>();
      Directions = new List<string>();
    }
  }
}