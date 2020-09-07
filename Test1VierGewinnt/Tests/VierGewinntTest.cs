using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Test1VierGewinnt.Source;

namespace Test1VierGewinnt.Tests
{
    [TestClass]
    public class VierGewinntTest
    {
        private static Spiel SetUp()
        {
            return new Spiel(6, 7);
        }
        
        [TestMethod]
        public void TestMethodSpielVerticalWin()
        {
            List<String> turns = new List<String>();
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("G_Red");
            turns.Add("B_Yellow");

            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.YellowWins);
        }

        [TestMethod]
        public void TestMethodSpielHorizontaWin()
        {
            List<String> turns = new List<String>();
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("C_Red");
            turns.Add("B_Yellow");
            turns.Add("D_Red");
            turns.Add("C_Yellow");
            turns.Add("E_Red");
            turns.Add("C_Yellow");
            turns.Add("F_Red");

            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.RedWins);
        }

        [TestMethod]
        public void TestMethodSpielDiagonalWin()
        {
            List<String> turns = new List<String>();
            turns.Add("C_Red");
            turns.Add("B_Yellow");
            turns.Add("D_Red");
            turns.Add("C_Yellow");
            turns.Add("D_Red");
            turns.Add("D_Yellow");
            turns.Add("E_Red");
            turns.Add("A_Yellow");
            turns.Add("E_Red");
            turns.Add("A_Yellow");
            turns.Add("E_Red");
            turns.Add("E_Yellow");

            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.YellowWins);
        }

        [TestMethod]
        public void TestMethodSpielDiagonal2Win()
        {
            List<String> turns = new List<String>();
            turns.Add("E_Red");
            turns.Add("F_Yellow");
            turns.Add("D_Red");
            turns.Add("E_Yellow");
            turns.Add("D_Red");
            turns.Add("D_Yellow");
            turns.Add("C_Red");
            turns.Add("G_Yellow");
            turns.Add("C_Red");
            turns.Add("G_Yellow");
            turns.Add("C_Red");
            turns.Add("C_Yellow");

            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.YellowWins);
        }


        [TestMethod]
        [ExpectedException(typeof(SpielbrettFullException))]
        public void TestMethodSpielSpalteVoll()
        {
            List<String> turns = new List<String>();
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("C_Red");
            turns.Add("A_Yellow");
            turns.Add("B_Red");
            turns.Add("A_Yellow");
            turns.Add("B_Red");
            turns.Add("A_Yellow");
            turns.Add("B_Red");
            turns.Add("A_Yellow");
            
            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.RedWins);
        }

        [TestMethod]
        public void TestMethodKeinSpielstein()
        {
            var spiel = SetUp();
            List<String> turns = new List<String>();
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.KeinSpielstein);
        }

        [TestMethod]
        [ExpectedException(typeof(FalschesInputTurns))]
        public void TestMethodFalscheInputData()
        {
            List<String> turns = new List<String>();
            turns.Add("A_Rede");
            var spiel = SetUp();
            spiel.AddTurns(turns);
        }

        [TestMethod]
        [ExpectedException(typeof(FalscherSpielerzugException))]
        public void TestMethodSpielYellowMovesTwice()
        {
            List<String> turns = new List<String>();
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("A_Yellow");
            
            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.RedWins);
        }

        [TestMethod]
        public void TestMethodSpielRedTurn()
        {
            List<String> turns = new List<String>();
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("A_Red");
            turns.Add("B_Yellow");
           
            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.RedsTurn);
        }

        [TestMethod]
        [ExpectedException(typeof(SpielBeebdetException))]
        public void TestMethodSpielZuVieleBewegungen()
        {
            List<String> turns = new List<String>();
            turns.Add("A_Red");
            turns.Add("B_Yellow");
            turns.Add("C_Red");
            turns.Add("B_Yellow");
            turns.Add("D_Red");
            turns.Add("C_Yellow");
            turns.Add("E_Red");
            turns.Add("C_Yellow");
            turns.Add("F_Red");
            turns.Add("C_Yellow");

            var spiel = SetUp();
            spiel.AddTurns(turns);
            Assert.AreEqual(spiel.SpielStatus, Enums.SpielStatus.RedWins);
        }
    }
}
