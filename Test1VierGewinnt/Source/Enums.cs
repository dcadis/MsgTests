using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1VierGewinnt.Source
{
    public class Enums
    {
        /// <summary>
        /// Spiel Stautus
        /// </summary>
        public enum SpielStatus
        {
            KeinSpielstein = 0,
            YellowsTurn = 1,
            RedsTurn = 2,
            Draw = 3,
            RedWins = 4,
            YellowWins = 5
        }

        /// <summary>
        /// Spieler/Seiten
        /// </summary>
        public enum Seiten
        {
            Red = 0,
            Yellow = 1
        }

        /// <summary>
        /// Darstellung des Spalte des Spielbrett
        /// </summary>
        public enum AplhaNumeric
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4,
            F = 5,
            G = 6
        }
    }
}
