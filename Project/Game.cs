using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Game : IGame
  {
    public Room CurrentRoom { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Player CurrentPlayer { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void GetUserInput()
    {
      throw new System.NotImplementedException();
    }

    public void Go(string direction)
    {
      throw new System.NotImplementedException();
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      throw new System.NotImplementedException();
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }

    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup()
    {
      //BUILD ROOMS
      Room HallWest = new Room("Hallway", "You find yourself in a small hall.  There doesn't appear to be anything of interest here.  You see passages to the North, East and South.");
      Room Barracks = new Room("Barracks", "You see a room with several sleeping guards.  The rooms smells of sweaty men and beer farts.  The bed closest to you is empty and there are several uniforms tossed about.");
      Room Courtyard = new Room("Castle Courtyard", "You step into a large castle courtyard.  There is flowing fountain in the middle of the grounds and a few guards patrolling the area.");
      Room CaptQtrs = new Room("Captain's Quarters", "As you approach the Captain's Quarters, you swallow hard and realize your lips are dry.  Stepping into the room you see a few small tables and maps of the countryside sprawled out.");
      Room HallSouth = new Room("Hallway", "You find yourself in a small hall.  There does not appear to be anything of interest here.  You see passages to the North, East and West.");
      Room GuardRoom = new Room("Guard Room", "Pushing open the door of the guard room, you look around and notice the room is empty.  There are a few small tools in the corner and a chair propped against the wall near a door that likely leads to the dungeon.");
      Room Dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon, you notice a harsh chill to the air.  Landing at the base of the stairs, you find the what appears to be the remains of a prisoner.");
      Room HallNorth = new Room("Hallway", "You find yourself in a small hall. There does not appear to be anything of interest here.  You see passages to the North, East and South.");
      Room ThroneRoom = new Room("Throne Room", "As you unlock the door and swing it wide you see an enormous hall stretching out before you. At the opposite end of the hall sitting on his throne you see the Dark Lord. The Dark Lord shouts at you demanding to know why you dared to interrupt him during his Ritual of Evil Summoning... Dumbfounded you mutter an incoherent response. Becoming more enraged the Dark Lord complains that you just ruined his concentration and he will now have to start the ritual over... Quickly striding towards you he smirks and says 'at least I know have a sacrificial volunteer.' He plunges his jewel encrusted dagger into your heart and your world slowly fades away.");
      Room SquireTower = new Room("Squire Tower", "Ascending the stairs to the squire tower, you see a messenger pass out drunk in his bed.  His messenger overcoat is hanging from his bed post.");
      Room WarRoom = new Room("War Room", "Stepping into the War Room, you see several maps spread across tables.  One the maps, several villages have been marked for 'purification.'  You also notice several dishes of prepared food to the side ... perhaps the war council will be meeting soon?");

      //ADD EXITS TO ROOMS
      HallWest.Exits.Add("north", Barracks);
      HallWest.Exits.Add("south", CaptQtrs);
      HallWest.Exits.Add("east", Courtyard);
      Barracks.Exits.Add("south", HallWest);
      Courtyard.Exits.Add("west", HallWest);
      Courtyard.Exits.Add("south", HallSouth);
      Courtyard.Exits.Add("north", HallNorth);
      CaptQtrs.Exits.Add("north", HallWest);
      CaptQtrs.Exits.Add("east", HallSouth);
      HallSouth.Exits.Add("west", CaptQtrs);
      HallSouth.Exits.Add("north", Courtyard);
      HallSouth.Exits.Add("east", GuardRoom);
      GuardRoom.Exits.Add("west", HallSouth);
      GuardRoom.Exits.Add("north", Dungeon);
      Dungeon.Exits.Add("south", GuardRoom);
      HallNorth.Exits.Add("north", ThroneRoom);
      HallNorth.Exits.Add("east", SquireTower);
      HallNorth.Exits.Add("south", Courtyard);
      SquireTower.Exits.Add("west", HallNorth);
      SquireTower.Exits.Add("north", WarRoom);
      WarRoom.Exits.Add("south", SquireTower);

      //BUILD ITEMS
      Item uniform = new Item("guard uniform", "a mostly clean guard's uniform that just happens to be your size.");
      Barracks.Items.Add(uniform);
      Item note = new Item("note", "a note from the captain instructing the Dark Lord's chief bodyguard to report to the Captain's Quarters immediately!");
      Item key = new Item("key", "a bronze key that appears to fit a door lock");
      Item vial = new Item("vial", "a small vial containing a green liquid.  Perhaps a poison?");
      CaptQtrs.Items.Add(note);
      CaptQtrs.Items.Add(key);
      CaptQtrs.Items.Add(vial);


    }

    public void StartGame()
    {
      throw new System.NotImplementedException();
    }

    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}