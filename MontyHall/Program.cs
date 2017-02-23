using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    public class GameOfMontyHall
    {
        List<Door> doors;
        Random rand = new Random();
        PlayerOfMontyHall player;
        int numberOfGames = 0;

        public List<Door> Doors
        {
            get { return doors; }
        }

        public PlayerOfMontyHall Player
        {
            get { return player; }
        }
            
        public GameOfMontyHall(int numberOfDoors, int numberOfGames, PlayerOfMontyHall player)
        {
            doors = new List<Door>(numberOfDoors);
            this.numberOfGames = numberOfGames;
            this.player = player;


            for (int i = 0; i < numberOfDoors; i++)
            {
                doors.Add(new Door());
            }
            

        }
        public void DoorInit()
        {

            bool doorIsAlreadySet = false;

            for(int i=0; i<doors.Count; i++)
            {
                doors[i].BehindTheDoor = 0;
                doors[i].IsDoorOpen = false;
                doors[i].IsSelectedByPlayer = false;
            }

            while (!doorIsAlreadySet)
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    
                    if (rand.Next(0, 1000)  == 510)
                    {
                        doors[i].BehindTheDoor = 1;
                        doorIsAlreadySet = true;
                    }
                    doors[i].IsDoorOpen = false;
                    doors[i].IsSelectedByPlayer = false;

                    if (doorIsAlreadySet)
                        break;
                }
            }
        }
        
        public void SelectRandomDoor()
        {
            player.SelectedDoor = rand.Next(1, 4);
            doors[player.SelectedDoor - 1].IsSelectedByPlayer = true;
        }

        public void OpenRandomDoor()
        {
            int indexOfSelectedDoor = 0;
            int indexOfCarDoor = 0;
            bool doorIsAlreadyOpened = false;

            FindIndexesOfSelectedAndCarDoor(ref indexOfSelectedDoor, ref indexOfCarDoor);

            if (indexOfCarDoor != indexOfSelectedDoor)
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    if (i != indexOfSelectedDoor && i != indexOfCarDoor)
                    {
                        doors[i].OpenTheDoor();
                    }
                }
            }
            else
            {
                while (!doorIsAlreadyOpened)
                {
                    for (int i = 0; i < doors.Count; i++)
                    {
                        if (i != indexOfSelectedDoor && !doorIsAlreadyOpened)
                        {
                            if (rand.Next(0, 100) == 50)
                            {
                                doors[i].OpenTheDoor();
                                doorIsAlreadyOpened = true;
                            }

                        }
                    }
                }
            }

        }

        private void FindIndexesOfSelectedAndCarDoor(ref int indexOfSelectedDoor, ref int indexOfCarDoor)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].IsSelectedByPlayer)
                    indexOfSelectedDoor = i;
                if (doors[i].BehindTheDoor == 1)
                    indexOfCarDoor = i;
            }
        }

        public bool PlayerWonTheGame()
        {
            int indexOfCarDoors = 0;
            
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].BehindTheDoor == 1)
                    indexOfCarDoors = i;
            }
            if(player.SelectedDoor == (indexOfCarDoors + 1))
            {
                player.NumberOfGamesWon++;
                return true;
            }

            return false;

        }

        public void Start()
        {
            for (int i = 0; i < numberOfGames; i++)
            {

                DoorInit();
                SelectRandomDoor();
                OpenRandomDoor();
                player.ChangeTheSelectedDoorByPlayer(doors);
                PlayerWonTheGame();
            }
        }
    }

    public class PlayerOfMontyHall
    {
        int selectedDoor = 0;
        int numberOfGamesWon = 0;

        public int NumberOfGamesWon
        {
            get { return numberOfGamesWon; }
            set { numberOfGamesWon = value; }
        }
        
        public int SelectedDoor
        {
            get { return selectedDoor; }
            set { selectedDoor = value; }
        }

        public virtual void ChangeTheSelectedDoorByPlayer(List<Door> doors) { }
        
    }

    public class PlayerWhoDontChangeTheSelectedDoor: PlayerOfMontyHall
    {

    }

    public class PlayerWhoChangeTheSelectedDoor: PlayerOfMontyHall
    {
        public override void ChangeTheSelectedDoorByPlayer(List<Door> doors)
        {
            bool selectedDoorIsAlreadyChanged = false;
            for(int i=0; i< doors.Count;i++)
            {
                if(!doors[i].IsDoorOpen && !selectedDoorIsAlreadyChanged)
                {
                    if (SelectedDoor != (i + 1))
                    {
                        doors[SelectedDoor - 1].IsSelectedByPlayer = false;
                        SelectedDoor = i + 1;
                        doors[i].IsSelectedByPlayer = true;
                        selectedDoorIsAlreadyChanged = true;
                    }
                }
            }
        }
    }

    public class Door
    {
        int behindTheDoor = 0;
        bool isDoorOpen = false;
        bool isSelectedByPlayer = false;

        public bool IsSelectedByPlayer
        {
            get { return isSelectedByPlayer; }
            set { isSelectedByPlayer = value; }
        }

        public bool IsDoorOpen
        {
            get { return isDoorOpen; }
            set { isDoorOpen = value; }
        }

        public void OpenTheDoor()
        {
            isDoorOpen = true;
        }

        public int BehindTheDoor
        {
            get { return behindTheDoor; }
            set { behindTheDoor = value; }
        }

  

    }

    class Program
    {
        static void Main(string[] args)
        {
            GameOfMontyHall game1 = new GameOfMontyHall(3, 1000, new PlayerWhoChangeTheSelectedDoor());
            GameOfMontyHall game2 = new GameOfMontyHall(3, 1000, new PlayerWhoDontChangeTheSelectedDoor());
            game1.Start();
            game2.Start();
            Console.WriteLine("Player who switched his choice: " + game1.Player.NumberOfGamesWon / 10 + "%");
            Console.WriteLine("Player who didn't switch his choice: " +  game2.Player.NumberOfGamesWon / 10+"%");
            
        }
    }
}
