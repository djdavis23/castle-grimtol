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
    private bool UniformOn { get; set; }
    private bool BedUsed { get; set; }
    private bool Dead { get; set; }
    private bool PrisonerFreed { get; set; }
    private bool PrisonerEscaped { get; set; }
    private bool CaptInQtrs { get; set; }


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
      //REINITIALIZE PROPERTIES
      Commands = new Dictionary<string, string>();
      Directions = new List<string>();
      Playing = true;
      UniformOn = false;
      BedUsed = false;
      Dead = false;
      PrisonerFreed = false;
      CaptInQtrs = true;
      PrisonerEscaped = false;

      //BUILD ROOMS
      Room Entrance = new Room("Entrance", "Your are standing at the rear entrance to Castle Grimtol.  Down a dark tunnel to the East, you see the flickering, faint glow of a torch.");
      Room HallWest = new Room("Hallway", "You find yourself in a small hall.  You see passages to the North, East, West and South.");
      Room Barracks = new Room("Barracks", "You see a room with several sleeping guards.  The rooms smells of sweaty men and beer farts.");
      Room Courtyard = new Room("Castle Courtyard", "You step into a large castle courtyard.  There is flowing fountain in the middle of the grounds and a few guards patrolling the area.  You see corridors to the North and South.");
      Room CaptQtrs = new Room("Captain's Quarters", "As you approach the Captain's Quarters, you swallow hard and realize your lips are dry.  Stepping into the room you see a few small tables and maps of the countryside sprawled out.  There are doors on the North and East walls.");
      Room HallSouth = new Room("Hallway", "You find yourself in a small hall.  There does not appear to be anything of interest here.  You see passages to the East and West and an open area to the North.");
      Room GuardRoom = new Room("Guard Room", "Pushing open the door of the guard room, you look around and notice the room is empty.  There are a few small tools in the corner and a chair propped against the wall near a door on the North wall that likely leads to the dungeon.");
      Room Dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon, you notice a harsh chill to the air.  You see a wall full of empty shackles as you round the final bend of the stairs descending into the dungeon.");
      Room HallNorth = new Room("Hallway", "You find yourself in a small hall. There does not appear to be anything of interest here.  You see a passage to the East and an open area to the South. To the North is a large wooden door.  You see an ominous green light coming from under the door and here the muffled sounds of a creepy voice");
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
      Item note = new Item("NOTE", "On one of the tables you see  note from the captain instructing the Dark Lord's messenger to report to the Captain immediately!");
      Item key = new Item("KEY", "Hanging on the wall is a silver key.");
      Item vial = new Item("VIAL", "Sitting on the Captain's desk is a small vial containing a green liquid.  Perhaps a poison or magic potion?");
      Item hammer = new Item("HAMMER", "Amongst the tools you find a well-worn, but sturdy sledgehammer");
      Item overcoat = new Item("OVERCOAT", "a medium-sized black overcoat with large pockets");
      Item window = new Item("WINDOW", "an open window in the east wall", false);

      //ADD ITEMS TO ROOMS
      Barracks.Items.Add(bed);
      Barracks.Items.Add(uniform);
      CaptQtrs.Items.Add(note);
      CaptQtrs.Items.Add(key);
      CaptQtrs.Items.Add(vial);
      GuardRoom.Items.Add(hammer);
      SquireTower.Items.Add(overcoat);
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
        else if (newRoom.Name == "Castle Courtyard") EnterCourtYard(newRoom);
        else if (newRoom.Name == "Captain's Quarters") EnterCaptQtrs(newRoom);
        else if (newRoom.Name == "Dungeon") EnterDungeon(newRoom);
        else
        {
          CurrentRoom = newRoom;
          CurrentRoom.Visited = true;
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
          UseBed();
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
          UseHammer();
          break;
        case "LOCK":
          Console.WriteLine("The lock is not useful here.");
          break;
        case "OVERCOAT":
          UseOvercoat();
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
      CurrentPlayer.PrintInventory();
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

    private void EndGame()
    {
      string message = "";
      if (Dead)
      {
        message = "Alas, poor knight, you have died.  The rebellion has failed.";
      }
      else if (PrisonerFreed)
      {
        message = $"You are a true hero of the rebellion!  The Dark Lord's reign of terror is over and you freed our tribal leader.  Generations of our people will sing songs praising your name Sir {CurrentPlayer.PlayerName}";
      }
      else
      {
        message = $"You have succeeded where so many others have failed.  The Dark Lord's reign of terror is over.  Thank you brave Sir {CurrentPlayer.PlayerName}";
      }
      Console.WriteLine($"\n{message}");
      Console.Write($"\nWould you like to play again brave knight?  ");
      string choice = Console.ReadLine().ToLower();
      if (choice == "y" || choice == "yes") Reset();
      else Quit();
    }

    private void UseBed()
    {
      if (BedUsed)
      {
        Console.WriteLine("\nAs you drift out of a sleep-induced fog, you see a large, angry guard standing over you. 'What do you think you're doing ... Hey, you're not Leroy!  QUICK, JENKINS, SIEZE THIS INTRUDER!!  Jenkins, a bit over-zealous swings his broadsword, cleaving you in half...");
        Dead = true;
        EndGame();
      }
      else
      {
        Console.WriteLine("\nYou climb into the bed and pretend to be asleep. A few minutes later several guards walk into the room. One approaches you to wake you... (GUARD) 'Hey Get Up! it's your turn for watch, Go relieve Shigeru in the Guard Room.' As he turns his back to yell at other sleeping guards, you quickly climb out of the bed and slip quietly out of the room.");
        BedUsed = true;
        //IF CASTLE LAYOUT CHANGED, THIS LINE MAY ALSO NEED TO BE CHANGED
        CurrentRoom.LeaveRoom("SOUTH");
      }
    }

    private void EnterCourtYard(Room courtyard)
    {
      CurrentRoom = courtyard;
      Look();
      if (UniformOn && !CurrentRoom.Visited)
      {
        Console.WriteLine("\nOne of the guards sees you and strolls over.  Checking your uniform to ensure it is in order, he says 'Glad you are here, it's been a long shift and I'm ready for a beer.  Report in to the Captain before coming on shift.' He then walks away towards another guard.");
        CurrentRoom.Visited = true;
      }
      else if (UniformOn && CurrentRoom.Visited)
      {
        Console.WriteLine("\nOne of the guards nods at you as they stroll by.");
      }
      else
      {
        Console.WriteLine("\nFrom your left you hear a shout .. 'Who the blazes are you?!'  Quickly he sounds the alarm and several guards with crossbows turn and fire upon you.  You realize you made a grave mistake as the first bolt slams into your body...");
        Dead = true;
        EndGame();
      }
    }

    private void EnterCaptQtrs(Room quarters)
    {
      CurrentRoom = quarters;
      if (CaptInQtrs && !CurrentRoom.Visited)
      {
        Console.WriteLine("\nThe captain on shift looks up from his desk, puts down his beer, belches and greets you. ...'New recruit huh. Well lets stick you in the guard room; you can't screw things up there. Go relieve (He pauses and glancing at his reports) private Miyamoto.'  As you exit to the East towards the Guard Room you notice there are numerous maps and other item of interest in the room.  You know you need to come back here to look around.");
        CurrentRoom.Visited = true;
        Go("EAST");
      }
      else if (CaptInQtrs && CurrentRoom.Visited)
      {
        Console.WriteLine("\nThe Captain Looks up from his beer and yells at you 'What are your doing back here? I told you to stay in the guard room!'");
        if (CurrentPlayer.Inventory.Exists(item => item.Name == "LOCK"))
        {
          Console.WriteLine("\nPulling the broken lock out of your armor, you tell him there has been a prison break.  He quickly dons his cap and stumbles out of the room towards the dungeon.");
          CaptInQtrs = false;
          Look();
        }
        else
        {
          Console.WriteLine("You rapidly exit the room to the East.");
          Go("EAST");
        }
      }
      else//Captain is not in his quarters
      {
        Look();
      }
    }

    private void EnterDungeon(Room dungeon)
    {
      CurrentRoom = dungeon;
      CurrentRoom.Visited = true;
      Look();
      if (!PrisonerFreed)
      {
        Console.WriteLine("\nBut then you notice a man sitting in shackles. As you approach him you see a small lock binding him to the wall with chains. As you near the prisoner his face turns to a deep frown.... (PRISONER) 'You look familiar, Hey thats right I know you from the village. Have you seriously turned your back on us and joined this squad of goons?' He sighs defeated...");
      }
      else if (PrisonerFreed && !PrisonerEscaped)
      {
        System.Console.WriteLine("\nBut looking deeper into the dungeon towards the rear exit, you find what appears to be the remains of a prisoner.  Must have been an unsuccessful escape attempt.");
      }
    }

    private void UseHammer()
    {
      if (CurrentRoom.Name == "Dungeon" && !PrisonerFreed)
      {
        Console.WriteLine("\nPulling the hammer out of your satchel, you smash the lookc and free the old man.  You pick up the broken lock and put it in your satchel.  It may be useful for something.");
        PrisonerFreed = true;
        Item Lock = new Item("LOCK", "a broken pad lock");
        CurrentPlayer.Inventory.Add(Lock);
        Console.WriteLine("\nThe old man hugs you.  'Thank you for rescueing me brave knight. I am going to sneak out before the guards come back.  Good luck on your quest!");
      }
      else
      {
        Console.WriteLine("\nThe hammer does you no good here");
      }
    }

    private void UseOvercoat()
    {
      if (CurrentRoom.Name == "Dungeon" && PrisonerFreed)
      {
        Console.WriteLine("\nYou pull the messenger's overcoat out of your armor and hand it to the old man.  He slips in on and thanks you.  'With this coat, I will be able to sneak out of here much more easily!");
        PrisonerEscaped = true;
      }
      Console.WriteLine("\nThe overcoat is not helpful here.");
    }

    public Game(string name)
    {
      CurrentPlayer = new Player(name);
    }
  }
}