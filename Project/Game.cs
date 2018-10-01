using System.Collections.Generic;
using System.Threading;
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
    private bool BedUsed { get; set; }
    private bool PrisonerFreed { get; set; }
    private bool PrisonerEscaped { get; set; }
    private bool CaptInQtrs { get; set; }
    private bool MessengerInRoom { get; set; }
    private bool WarroomDoorLocked { get; set; }
    private bool VialUsed { get; set; }


    public void StartGame()
    {
      Console.Clear();
      Setup();
      while (Playing)
      {
        Console.Write($"\n What is your next move Sir {CurrentPlayer.PlayerName}?  ");
        GetUserInput();
      }
      Console.Clear();
      Console.WriteLine("\n Thank you for your valient efforts brave knight!  Safe journeys!");
    }

    public void Setup()
    {
      //REINITIALIZE PROPERTIES
      Commands = new Dictionary<string, string>();
      Directions = new List<string>();
      Playing = true;
      CurrentPlayer.UniformOn = false;
      BedUsed = false;
      CurrentPlayer.Dead = false;
      PrisonerFreed = false;
      CaptInQtrs = true;
      PrisonerEscaped = false;
      WarroomDoorLocked = true;
      MessengerInRoom = true;
      VialUsed = false;

      //BUILD ROOMS
      Room Entrance = new Room("Entrance", "Your are standing at the rear entrance to Castle Grimtol.  \n Down a dark tunnel to the East, you see the flickering, faint glow of a torch.");
      Room HallWest = new Room("Hallway", "You find yourself in a small hall.  You see passages to the North, East, West and South.");
      Room Barracks = new Room("Barracks", "You see a room with several sleeping guards.  The rooms smells of sweaty men and beer farts.");
      Room Courtyard = new Room("Castle Courtyard", "You step into a large castle courtyard.  There is flowing fountain in the middle of the grounds \n and a few guards patrolling the area.  You see corridors to the North and South.");
      Room CaptQtrs = new Room("Captain's Quarters", "With the Captain gone, you are finally able to search the room.   You see a few small tables \n and maps of the countryside sprawled out. There are doors on the North and East walls.");
      Room HallSouth = new Room("Hallway", "You find yourself in a small hall.  There does not appear to be anything of interest here. \nYou see passages to the East and West and an open area to the North.");
      Room GuardRoom = new Room("Guard Room", "Pushing open the door of the guard room, you look around and notice the room is empty.  \n There are a few small tools in the corner and a chair propped against the wall near a door on \n the North wall that likely leads to the dungeon.");
      Room Dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon, you notice a harsh chill to the air. \n You see a wall full of empty shackles as you round the final bend of the stairs descending into the dungeon.");
      Room HallNorth = new Room("Hallway", "You find yourself in a small hall. There does not appear to be anything of interest here.  \n You see a passage to the East and an open area to the South. To the North is a large wooden door.  \n You see an ominous green light coming from under the door and here the muffled sounds of a creepy voice");
      Room ThroneRoom = new Room("Throne Room", "As you unlock the door and swing it wide you see an enormous hall stretching out before you. \n At the opposite end of the hall sitting on his throne you see the Dark Lord. \n The Dark Lord shouts at you demanding to know why you dared to interrupt him during his Ritual of Evil Summoning.  \n You turn to run, but the door slams shut and locks. Dumbfounded you mutter an incoherent response. \n Becoming more enraged the Dark Lord complains that you just ruined his concentration and \n he will now have to start the ritual over... Quickly striding towards you he smirks and \n says 'at least I know have a sacrificial volunteer.' He plunges his jewel encrusted dagger \n into your heart and your world slowly fades away.");
      Room SquireTower = new Room("Squire Tower", "Ascending the stairs to the squire tower, you see a messenger passed out drunk in his bed.  \n His messenger overcoat is hanging from his bed post.  There is a hallway to the west \n and a door to the North.  However, the messenger's bed is blocking the door.");
      Room WarRoom = new Room("War Room", "Stepping into the War Room, you see several maps spread across tables.  One the maps, \n several villages have been marked for 'purification.'  You also notice several dishes of prepared \n food to the side and steins filled with beer... perhaps the war council will be meeting soon?", true);

      //ADD EXITS TO ROOMS
      Entrance.Exits.Add("EAST", HallWest);
      HallWest.Exits.Add("WEST", Entrance);
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
      ThroneRoom.Exits.Add("SOUTH", HallNorth);
      SquireTower.Exits.Add("WEST", HallNorth);
      SquireTower.Exits.Add("NORTH", WarRoom);
      WarRoom.Exits.Add("SOUTH", SquireTower);

      //BUILD ITEMS
      Item bed = new Item("BED", "The bed closest to you is empty", false);
      Item uniform = new Item("UNIFORM", "there are several uniforms tossed about the room.  One appears to be about your size.");
      Item note = new Item("NOTE", "On one of the tables you see  note from the captain instructing the Dark Lord's messenger to \n\treport to the Captain immediately!");
      Item key = new Item("KEY", "Hanging on the wall is a silver key.");
      Item vial = new Item("VIAL", "Sitting on the Captain's desk is a small vial containing a green liquid.  \n\tPerhaps a poison or magic potion?");
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
      Console.WriteLine("\n Thank you noble knight!  An orc who is symathetic to our cause will lead\n you to the rear entrance to Castle Grimtol   Once you sneak through the tunnel, you will need\n to find a way to disguise yourself and kill the Dark Lord.  This grave mission will require\n all of your wit and cunning.");

      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("\n At any time you may type 'help' at the command line to see a list of\n valid game commands");
      Console.ForegroundColor = ConsoleColor.Green;

      Console.WriteLine($"\n Good luck Brave {CurrentPlayer.PlayerName}");
      Thread.Sleep(5000);
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
          Console.WriteLine("\n Did not recognize that command.");
          break;
      }
    }

    //Attempt to Move in the direction specificed by the user
    public void Go(string direction)
    {
      //invalid direction entered
      if (!Directions.Contains(direction))
      {
        System.Console.WriteLine("\n I did not understand that direction.  Please try again brave knight");
        return;
      }
      //cannot attempt entry to Warroom while messenger is in the squire tower
      Console.Clear();
      if (CurrentRoom.Name == "Squire Tower" && MessengerInRoom && direction == "NORTH")
      {
        System.Console.WriteLine("\n As you approach the messenger, he startles awake and sees you.\n  He yells out 'WHO ARE YOU AND WHAT ARE YOU DOING HERE?'  You quickly exit back through the\n hallway to the west.  If only you had a way to distract the messenger...");
        Thread.Sleep(8000);
        Go("WEST");
        return;
      }

      //exiting the warroom to the tower after using the vial is bad!
      if (CurrentRoom.Name == "War Room" && VialUsed && direction == "SOUTH")
      {
        System.Console.WriteLine("\n As you exit through the door into the tower, the messenger\n dashes up the stairs with the Captain and his guard.  'THERE HE IS' yells the messenger.  As\n three burly guards approach you with their swords drawn, you realize that your mission has\n failed...");
        CurrentPlayer.Dead = true;
        Thread.Sleep(5000);
        EndGame();
      }
      //Try to exit the room
      Console.Clear();
      CurrentRoom = CurrentRoom.LeaveRoom(direction);

      //special conditions for certain rooms
      if (CurrentRoom.Name == "Castle Courtyard") EnterCourtYard();
      else if (CurrentRoom.Name == "Captain's Quarters") EnterCaptQtrs();
      else if (CurrentRoom.Name == "Dungeon") EnterDungeon();
      else if (CurrentRoom.Name == "Squire Tower") EnterSquireTower();
      else if (CurrentRoom.Name == "Throne Room") EnterThroneRoom();

      //all other rooms
      else
      {
        CurrentRoom.Visited = true;
        Look();
      }
    }

    public void UseItem(string itemName)
    {
      //Item must be in the player inventory or the current room to be used
      Item myitem = CurrentPlayer.Inventory.Find(item => item.Name == itemName);
      if (myitem == null)
      {
        myitem = CurrentRoom.Items.Find(item => item.Name == itemName);
      }
      if (myitem == null)
      {
        System.Console.WriteLine("\n You do not have that item available for use.");
        return;
      }

      switch (myitem.Name)
      {
        case "BED":
          UseBed();
          break;
        case "UNIFORM":
          CurrentPlayer.UniformOn = true;
          CurrentPlayer.Inventory.Remove(myitem);
          System.Console.WriteLine("\n You put on the guard's uniform ...it fits well.  Now you will\n be more covert.");
          break;
        case "KEY":
          UseKey();
          break;
        case "VIAL":
          UseVial();
          break;
        case "NOTE":
          UseNote();
          break;
        case "HAMMER":
          UseHammer();
          break;
        case "LOCK":
          Console.WriteLine("\n The lock is not useful here.");
          break;
        case "OVERCOAT":
          UseOvercoat();
          break;
        case "WINDOW":
          UseWindow();
          break;
      }
    }
    //Take an item from the room and add it to player inventory
    public void TakeItem(string itemName)
    {
      Item item = CurrentRoom.TakeItem(itemName);
      if (item == null)
      {
        Console.WriteLine($"\n Cannot take {itemName} from this room");
      }
      else
      {
        CurrentPlayer.Inventory.Add(item);
        Console.WriteLine($"\n {item.Name} added to your inventory.");
      }
    }

    //Review the room description
    public void Look()
    {
      Console.WriteLine($"\n {CurrentRoom.Name}:  {CurrentRoom.Description}");
      CurrentRoom.PrintItems();
    }

    //Display invenotory contents
    public void Inventory()
    {
      CurrentPlayer.PrintInventory();
    }

    //Display list of valid game commands
    public void Help()
    {
      Console.WriteLine("\n VALID GAME COMMANDS: ");
      foreach (KeyValuePair<string, string> command in Commands)
      {
        Console.WriteLine($"\t{command.Key} {command.Value}");
      }
    }

    //Quit game
    public void Quit()
    {
      Console.Write("\n Are you sure you want to quit?  ");
      string confirm = Console.ReadLine().ToLower();
      if (confirm == "y" || confirm == "yes") Playing = false;
    }

    //Reinitializes the game
    public void Reset()
    {
      CurrentPlayer.Inventory.RemoveAll(item => item is Item);
      Directions.RemoveAll(direction => direction is string);
      StartGame();
    }

    //Ends the game when the player either dies or defeats the Dark Lord
    private void EndGame()
    {
      string message = "";
      if (CurrentPlayer.Dead)
      {
        message = "Alas, poor knight, you have died.  The rebellion has failed.";
      }
      else if (PrisonerEscaped)
      {
        message = $"You are a true hero of the rebellion!  The Dark Lord's reign of terror is over\n and you freed our tribal leader.  Generations of our people will sing songs praising your\n name Sir {CurrentPlayer.PlayerName}";
      }
      else
      {
        message = $"You have succeeded where so many others have failed.  The Dark Lord's reign of\n terror is over.  Thank you brave Sir {CurrentPlayer.PlayerName}";
      }
      Console.WriteLine($"\n {message}");
      Thread.Sleep(3000);
      Console.Write($"\n\n Would you like to play again brave knight?  ");
      string choice = Console.ReadLine().ToLower();
      if (choice == "y" || choice == "yes") Reset();
      else Quit();
    }

    private void EnterCourtYard()
    {
      Look();
      Thread.Sleep(3000);
      if (CurrentPlayer.UniformOn && !CurrentRoom.Visited)
      {
        Console.WriteLine("\n One of the guards sees you and strolls over.  Checking your uniform to\n ensure it is in order, he says 'Glad you are here, it's been a long shift and I'm ready for\n a beer.  Report in to the Captain before coming on shift.' He then walks away towards\n another guard.");
        CurrentRoom.Visited = true;
      }
      else if (CurrentPlayer.UniformOn && CurrentRoom.Visited)
      {
        Console.WriteLine("\n One of the guards nods at you as they stroll by.");
      }
      else
      {
        Console.WriteLine("\n From your left you hear a shout .. 'Who the blazes are you?!'  Quickly\n he sounds the alarm and several guards with crossbows turn and fire upon you.  You realize\n you made a grave mistake as the first bolt slams into your body...");
        CurrentPlayer.Dead = true;
        Thread.Sleep(5000);
        EndGame();
      }
    }

    private void EnterCaptQtrs()
    {
      if (CaptInQtrs)
      {
        if (!CurrentRoom.Visited)
        {
          Console.WriteLine("\n You enter a room that appears to be the quarters of a senior\n officer.  The captain on shift looks up from his desk, puts down his beer, belches and\n greets you. ...'New recruit huh. Well lets stick you in the guard room; you can't screw\n things up there. Go relieve Private Miyamoto.");
          CurrentRoom.Visited = true;
          Thread.Sleep(9000);
        }
        else
        {
          Console.WriteLine("\n The Captain Looks up from his beer and yells at you 'What are your\n doing back here? I told you to stay in the guard room!'");
          Thread.Sleep(3000);
        }
        if (CurrentPlayer.Inventory.Exists(item => item.Name == "LOCK"))
        {
          Console.WriteLine("\n Pulling the broken lock out of your armor, you tell him there has\n been a prison break.  He quickly dons his cap and stumbles out of the room towards the\n dungeon.");
          Thread.Sleep(3000);
          CaptInQtrs = false;
          Look();
        }
        else
        {
          Console.WriteLine("\n As you quickly exit out the hallway to the East, you notice there are\n numerous maps and other item of interest in the room.  You know you need to come back here\n to look around.");
          Thread.Sleep(6000);
          Go("EAST");
        }
      }

      else//Captain is not in his quarters
      {
        Look();
      }
    }

    private void EnterDungeon()
    {
      CurrentRoom.Visited = true;
      Look();
      if (!PrisonerFreed)
      {
        Thread.Sleep(4000);
        Console.WriteLine("\n But then you notice a man sitting in shackles. As you approach him you\n see a small lock binding him to the wall with chains. As you near the prisoner his face\n turns to a deep frown.... (PRISONER) 'You look familiar, Hey thats right I know you from the\n village. Have you seriously turned your back on us and joined this squad of goons?' He sighs\n defeated...");
      }
      else if (PrisonerFreed && !PrisonerEscaped)
      {
        Thread.Sleep(3000);
        System.Console.WriteLine("\n But looking deeper into the dungeon towards the rear exit, you\n find what appears to be the remains of a prisoner.  Must have been an unsuccessful escape\n attempt.");
      }
    }

    private void EnterSquireTower()
    {
      if (!MessengerInRoom)
      {
        System.Console.WriteLine("\n The messenger has still not returned.  There is a hallway to the\n west and a door on the North wall.");
      }
      else Look();
    }

    private void EnterThroneRoom()
    {
      System.Console.WriteLine("\n As you unlock the door and swing it wide you see an enormous hall\n stretching out before you. At the opposite end of the hall sitting on his throne you see the\n Dark Lord. The Dark Lord shouts at you demanding to know why you dared to interrupt him during\n his Ritual of Evil Summoning.  You turn to run, but the door slams shut and locks. Dumbfounded\n you mutter an incoherent response. Becoming more enraged the Dark Lord complains that you just\n ruined his concentration and he will now have to start the ritual over... Quickly striding\n towards you he smirks and says 'at least I know have a sacrificial volunteer.' He plunges his\n jewel encrusted dagger into your heart and your world slowly fades away.");
      Thread.Sleep(6000);
      CurrentPlayer.Dead = true;
      EndGame();
    }

    private void UseBed()
    {
      if (BedUsed)
      {
        Console.WriteLine("\n As you drift out of a sleep-induced fog, you see a large, angry guard\n standing over you. 'What do you think you're doing ... Hey, you're not Leroy!  QUICK,\n JENKINS, SIEZE THIS INTRUDER!!  Jenkins, a bit over-zealous swings his broadsword, cleaving\n you in half...");
        CurrentPlayer.Dead = true;
        Thread.Sleep(3000);
        EndGame();
      }
      else
      {
        Console.WriteLine("\n You climb into the bed and pretend to be asleep. A few minutes later\n several guards walk into the room. One approaches you to wake you... (GUARD) 'Hey Get Up!\n it's your turn for watch, Go relieve Shigeru in the Guard Room.' As he turns his back to\n yell at other sleeping guards, you quickly climb out of the bed and slip escape the\n room via the hallway to the south.");
        BedUsed = true;
        Thread.Sleep(6000);
        //IF CASTLE LAYOUT CHANGED, THIS LINE MAY ALSO NEED TO BE CHANGED
        Go("SOUTH");
      }
    }
    private void UseHammer()
    {
      if (CurrentRoom.Name == "Dungeon" && !PrisonerFreed)
      {
        Console.WriteLine("\n Pulling the hammer out of your satchel, you smash the lock and free\n the old man.  You pick up the broken lock and put it in your satchel.  It may be useful for\n something.");
        PrisonerFreed = true;
        Item Lock = new Item("LOCK", "a broken pad lock");
        CurrentPlayer.Inventory.Add(Lock);
        Thread.Sleep(3000);
        Console.WriteLine("\n The old man hugs you.  'Thank you for rescueing me brave knight. I am\n going to sneak out before the guards come back.  Although I'm not sure how far I'll make it\n wearing these prison rags.  I hope no one sees me... Good luck on your quest!");
      }
      else
      {
        Console.WriteLine("\n The hammer does you no good here");
      }
    }

    private void UseOvercoat()
    {
      if (CurrentRoom.Name == "Dungeon" && PrisonerFreed)
      {
        Console.WriteLine("\n You pull the messenger's overcoat out of your armor and hand it to the\n old man.  He slips in on and thanks you.  'With this coat, I will be able to sneak out of\n here much more easily!");
        PrisonerEscaped = true;
      }
      Console.WriteLine("\n The overcoat is not helpful here.");
    }

    private void UseKey()
    {
      if (CurrentRoom.Name == "Squire Tower")
      {
        System.Console.WriteLine("\n You approach the door on the North Wall.  You pull the key from\n your pocket, insert it into the lock and turn.  There is loud click and the door slowly\n swings open");
        Room Warroom = (Room)CurrentRoom.Exits["NORTH"];
        Warroom.DoorLocked = false;

      }
      else System.Console.WriteLine("\n The key does not work here");
    }

    private void UseVial()
    {
      if (CurrentRoom.Name == "War Room")
      {
        System.Console.WriteLine("\n Pulling the vial out of your pocket and popping the cork, you\n poor some of the mysterious liquid into the most ornate looking steins on the table.  You\n toss the empty vial out of the window.  As you turn to leave, you hear voices approaching\n from the squire tower.");
        VialUsed = true;
      }
      else System.Console.WriteLine("\n The vial is not useful here.");
    }

    private void UseNote()
    {
      if (CurrentRoom.Name == "Squire Tower" && MessengerInRoom)
      {
        System.Console.WriteLine("\n As you approach the sleeping messenger, you pull the note from\n your armor.  Shaking him awake, hand him the note and state urgently, 'Quick, the Captain\n needs to see you immediately!'  Looking at the note, his eyes widen and he jumps out of bed,\n quickly exiting down the stairs.");
        MessengerInRoom = false;
      }
      else System.Console.WriteLine("\n The note is not useful here");
    }

    private void UseWindow()
    {
      if (VialUsed)
      {
        Console.Clear();
        System.Console.WriteLine("\n Dashing over to the window and looking out, you see a small\n ledge running around the tower about 3 feet below the window.  You climb out onto the ledge\n and scoot along the wall to the south");
        Thread.Sleep(3000);
        System.Console.WriteLine("\n You hear several voice laughings loudly at their impending\n conquest of the entire land.  One of the voices offers a toast to the Dark Lord.  There is a\n loud chorus of voices singing 'To his highness!' and then silence as the group drinks.\n Suddenly you hear a stein fall to the floor and a loud gasp.  Several distressed voices cry\n out 'My Lord, what is wrong?!'  As you jump to the ground and make your way to the exit, you\n know that the poison found its mark and the mission is a success!");
        Thread.Sleep(8000);
        EndGame();
      }

    }

    public Game(string name)
    {
      CurrentPlayer = new Player(name);
    }
  }
}