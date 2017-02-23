using Microsoft.VisualStudio.TestTools.UnitTesting;
using MontyHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall.Tests
{
    [TestClass()]
    public class DoorTests
    {
        [TestMethod()]
        public void OpenTheDoorTest()
        {
            Door door = new Door();
            door.OpenTheDoor();

            Assert.AreEqual(true, door.IsDoorOpen);

        }

        [TestMethod()]
        public void DoorInitTest()
        {
            GameOfMontyHall game = new GameOfMontyHall(3, 1000, new PlayerWhoDontChangeTheSelectedDoor());
            int carIsBehindTheFirstDoorCounter = 0;
            int carIsBehindTheSecondDoorCounter = 0;
            int carIsBehindTheThirdDoorCounter = 0;

            for (int i = 0; i < 1000; i++)
            {
                game.DoorInit();                

                if (game.Doors[0].BehindTheDoor == 1)
                    carIsBehindTheFirstDoorCounter++;
                if (game.Doors[1].BehindTheDoor == 1)
                    carIsBehindTheSecondDoorCounter++;
                if (game.Doors[2].BehindTheDoor == 1)
                    carIsBehindTheThirdDoorCounter++;
            }

            Console.WriteLine("RandomDoorValuesTest1: " +
                carIsBehindTheFirstDoorCounter + " " +
                carIsBehindTheSecondDoorCounter + " " +
                carIsBehindTheThirdDoorCounter + "\n");

            Assert.IsTrue(carIsBehindTheFirstDoorCounter > 300 &&
                          carIsBehindTheSecondDoorCounter > 300 &&
                          carIsBehindTheThirdDoorCounter > 300);
        }

        [TestMethod()]
        public void RandomDoorValuesTest1()
        {
            GameOfMontyHall game = new GameOfMontyHall(3,1000, new PlayerWhoDontChangeTheSelectedDoor());
            int carIsBehindTheFirstDoorCounter = 0;
            int carIsBehindTheSecondDoorCounter = 0;
            int carIsBehindTheThirdDoorCounter = 0;

            for (int i = 0; i < 1000; i++)
            {
                game.DoorInit();                

                if (game.Doors[0].BehindTheDoor == 1)
                    carIsBehindTheFirstDoorCounter++;
                if (game.Doors[1].BehindTheDoor == 1)
                    carIsBehindTheSecondDoorCounter++;
                if (game.Doors[2].BehindTheDoor == 1)
                    carIsBehindTheThirdDoorCounter++;
            }

            Console.WriteLine("RandomDoorValuesTest1: " +
                carIsBehindTheFirstDoorCounter + " " +
                carIsBehindTheSecondDoorCounter + " " +
                carIsBehindTheThirdDoorCounter + "\n");

            Assert.IsTrue(carIsBehindTheFirstDoorCounter > 300 &&
                          carIsBehindTheSecondDoorCounter > 300 &&
                          carIsBehindTheThirdDoorCounter > 300);
        }

        [TestMethod()]
        public void SelectRandomDoorTest1()
        {
            GameOfMontyHall game = new GameOfMontyHall(3, 1000, new PlayerWhoDontChangeTheSelectedDoor());
            int firstDoorIsSelectedCounter = 0;
            int secondDoorIsSelectedCounter = 0;
            int thirdDoorIsSelectedCounter = 0;

            for (int i = 0; i < 1000; i++)
            {
                game.SelectRandomDoor();
                switch (game.Player.SelectedDoor)
                {
                    case 1:
                        firstDoorIsSelectedCounter++;
                        break;
                    case 2:
                        secondDoorIsSelectedCounter++;
                        break;
                    case 3:
                        thirdDoorIsSelectedCounter++;
                        break;
                    default:
                        break;
                }                

            }
            Console.WriteLine("SelectRandomDoorTest1: " + 
                firstDoorIsSelectedCounter + " " + 
                secondDoorIsSelectedCounter + " " + 
                thirdDoorIsSelectedCounter + "\n");

            Assert.IsTrue(firstDoorIsSelectedCounter > 300 &&
                              secondDoorIsSelectedCounter > 300 &&
                              thirdDoorIsSelectedCounter > 300);
        }


        [TestMethod()]
        public void OpenRandomDoorTest1()
        {
            GameOfMontyHall game = new GameOfMontyHall(3,1000, new PlayerWhoChangeTheSelectedDoor());
            int firstDoorIsOpen = 0;
            int secondDoorIsOpen = 0;
            int thirdDoorIsOpen = 0;

            for (int i = 0; i < 1000; i++)
            {
                game.DoorInit();
                game.SelectRandomDoor();
                game.OpenRandomDoor();


                if (game.Doors[0].IsDoorOpen == true)
                    firstDoorIsOpen++;
                if (game.Doors[1].IsDoorOpen == true)
                    secondDoorIsOpen++;
                if (game.Doors[2].IsDoorOpen == true)
                    thirdDoorIsOpen++;

            }

            Console.WriteLine("OpenRandomDoorTest1: " + firstDoorIsOpen + " " + secondDoorIsOpen + " "+ thirdDoorIsOpen +"\n");

            Assert.IsTrue(firstDoorIsOpen > 300 &&
                              secondDoorIsOpen > 300 &&
                              thirdDoorIsOpen > 300);
        }

        [TestMethod()]
        public void PlayerWonOneGameTest()
        {
            GameOfMontyHall game = new GameOfMontyHall(3,1000, new PlayerWhoDontChangeTheSelectedDoor());
            int indexOfCarDoors = 0;
            game.DoorInit();
            for(int i=0; i< game.Doors.Count; i++)
            {
                if (game.Doors[i].BehindTheDoor == 1)
                    indexOfCarDoors = i;
            }

            game.Player.SelectedDoor = indexOfCarDoors+1;
            game.PlayerWonTheGame();
            Assert.AreEqual(1, game.Player.NumberOfGamesWon);
            
            
        }

        [TestMethod()]
        public void StartTheGame_PlayerDontChangesSelectedDoorTest()
        {
            GameOfMontyHall game = new GameOfMontyHall(3, 1000, new PlayerWhoDontChangeTheSelectedDoor());
            game.Start();
            Console.WriteLine("NumberOfGamesWon: " + game.Player.NumberOfGamesWon);
            Assert.IsTrue(game.Player.NumberOfGamesWon < 400);
        }

        [TestMethod()]
        public void ChangeTheSelectedDoorByPlayerTest()
        {
            List<Door> doors =new List<Door>(3);
            PlayerWhoChangeTheSelectedDoor player = new PlayerWhoChangeTheSelectedDoor();
            doors.Add(new Door());
            doors.Add(new Door());
            doors.Add(new Door());
            doors[0].OpenTheDoor();
            player.SelectedDoor = 2;
            player.ChangeTheSelectedDoorByPlayer(doors);

            Assert.AreEqual(3, player.SelectedDoor);
        }

        [TestMethod()]
        public void ChangeTheSelectedDoorByPlayer1000TimesTest()
        {
            GameOfMontyHall game = new GameOfMontyHall(3, 1000, new PlayerWhoChangeTheSelectedDoor());

            int firstDoorIsSelectedCounter = 0;
            int secondDoorIsSelectedCounter = 0;
            int thirdDoorIsSelectedCounter = 0;

            for (int i = 0; i < 1000; i++)
            {
                game.DoorInit();
                game.SelectRandomDoor();
                game.OpenRandomDoor();
                game.Player.ChangeTheSelectedDoorByPlayer(game.Doors);

                switch (game.Player.SelectedDoor)
                {
                    case 1:
                        firstDoorIsSelectedCounter++;
                        break;
                    case 2:
                        secondDoorIsSelectedCounter++;
                        break;
                    case 3:
                        thirdDoorIsSelectedCounter++;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("SelectRandomDoorTest1: " +
                firstDoorIsSelectedCounter + " " +
                secondDoorIsSelectedCounter + " " +
                thirdDoorIsSelectedCounter + "\n");

            Assert.IsTrue(firstDoorIsSelectedCounter > 300 &&
                              secondDoorIsSelectedCounter > 300 &&
                              thirdDoorIsSelectedCounter > 300);
        }

        [TestMethod()]
        public void StartTheGame_PlayerChangesSelectedDoorTest1()
        {
            GameOfMontyHall game = new GameOfMontyHall(3, 1000, new PlayerWhoChangeTheSelectedDoor());
            game.Start();
            Console.WriteLine("NumberOfGamesWon: " + game.Player.NumberOfGamesWon);
            Assert.IsTrue(game.Player.NumberOfGamesWon > 600);
            
        }

        [TestMethod()]
        public void StartTheGame_PlayerChangesSelectedDoorTest2()
        {
            GameOfMontyHall game = new GameOfMontyHall(3, 10000, new PlayerWhoChangeTheSelectedDoor());
            game.Start();
            Console.WriteLine("NumberOfGamesWon: " + game.Player.NumberOfGamesWon);
            Assert.IsTrue(game.Player.NumberOfGamesWon > 6000);
        }



    }
}