using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1VierGewinnt.Source
{
    /// <summary>
    /// Spiel fuer vier gewinnt 
    /// </summary>
    public class Spiel
    {
        #region Private Fields and Properties

        int _Zeilen; //zeilen fuer Spielbrett
        int _Spalten; //spalten fuer Spielbrett

        /// <summary>
        /// Laenge des Spielstein fuer einem Gewinn
        /// </summary>
        public const int STEINLAENGE = 4;

        private const string DELIMITER = "_"; //Trennzeichen fuer input turns

        Spielbrett _Spielbrett = null;
        
        Enums.SpielStatus _AkutellesStatus;
        /// <summary>
        /// Staus des Spiel Vier Gewinnt
        /// </summary>
        public Enums.SpielStatus SpielStatus
        {
            get { return _AkutellesStatus; }
        }

        /// <summary>
        /// Algorithms for checking for winning state.
        /// </summary>
        List<IGewinnAlgoritmus> gewinnAlgoritmus = null;


        /// <summary>
        /// Gets a reference to the board currently in play.
        /// </summary>
        Spielbrett Spielbrett
        {
            get { return _Spielbrett; }
        }

        #endregion Private Fields and Properties

        /// <summary>
        /// Konstructor.
        /// </summary>
        /// <param name="Zeilen">Zeilen nummer fuer Spielbrett (>0)</param>
        /// <param name="Spalten">Spalte nummer fuer Spielbrett</param>
        public Spiel(int Zeilen, int Spalten)
        {
            if (Zeilen <= 0 || Spalten <= 0)
                throw new InvalidSpielbrettDimensionsException("Die Höhe und Breite der Spielbrett muss größer als 0 sein.");

            if (Zeilen < STEINLAENGE || Spalten < STEINLAENGE)
                throw new InvalidSpielbrettDimensionsException("Spielbrett ist zu klein.");

            this.gewinnAlgoritmus = new List<IGewinnAlgoritmus>(){
                    new DiagonalGewinnAlgoritmus(),
                    new HorizontalGewinnAlgoritmus(),
                    new VerticalGewinnAlgoritmus()};

            this._Zeilen = Zeilen;
            this._Spalten = Spalten;
            this._AkutellesStatus = Enums.SpielStatus.KeinSpielstein;
            _Spielbrett = new Spielbrett(Zeilen, Spalten);

        }

        #region Methods

        /// <summary>
        /// Fügt eine Liste von Zügen hinzu
        /// </summary>
        /// <param name="turns"></param>
        public void AddTurns(List<String> turns)
        {
            foreach (string turn in turns)
            {
                Tuple<int, Enums.Seiten> turnTuple = ParseTurn(turn);

                var spielstein = new Spielstein(turnTuple.Item2);
                AddSpielstein(spielstein, turnTuple.Item1);
            }
        }

        /// <summary>
        /// Parse das string turn und gibt zurueck das angegebene Spalte und Seite des Turns
        /// </summary>
        /// <param name="turn"></param>
        /// <returns>tupe fuer Spalte und Seite</returns>
        private Tuple<int, Enums.Seiten> ParseTurn(string turn)
        {
            if (!turn.Contains(DELIMITER) || turn.IndexOf(DELIMITER) == 0)
                throw new FalschesInputTurns("Falsches Format angegeben. Richtiges Format: A_Red");

            var strSpieler = turn.Substring(turn.IndexOf(DELIMITER) + 1);

            Enums.Seiten spieler;
            if (string.IsNullOrEmpty(strSpieler) || !Enum.TryParse<Enums.Seiten>(strSpieler, true, out spieler))
                throw new FalschesInputTurns(string.Format("Falsches Spieler angegeben {0}", strSpieler));

            var strSpalte = turn.Substring(0, 1);
            Enums.AplhaNumeric alphaNum;
            if (!Enum.TryParse(strSpalte, true, out alphaNum))
                throw new FalschesInputTurns(string.Format("Falsche Spalte angegeben {0}.", strSpalte));

            return new Tuple<int, Enums.Seiten>((int)alphaNum, spieler);
        }

        /// <summary>
        /// Spielstein zu Spielnrett hinzufuegen
        /// </summary>
        /// <param name="spielstein"></param>
        /// <param name="spalteIndex"></param>
        public void AddSpielstein(Spielstein spielstein, int spalteIndex)
        {
            if (this._AkutellesStatus != Enums.SpielStatus.KeinSpielstein &&
                this._AkutellesStatus != Enums.SpielStatus.RedsTurn && 
                this._AkutellesStatus != Enums.SpielStatus.YellowsTurn)
                throw new SpielBeebdetException(String.Format("Das Spiel ist bereits beendet! {0}", SpielStatus));

            Console.WriteLine(string.Format("Spielstein {0}", spielstein));
            if (this._AkutellesStatus == Enums.SpielStatus.YellowsTurn && spielstein.Seite == Enums.Seiten.Red)
                throw new FalscherSpielerzugException("Derzeit ist Gelb an der Reihe. Spielen Sie ein gelbes Spielstein ab.");
            else if (this._AkutellesStatus == Enums.SpielStatus.RedsTurn && spielstein.Seite == Enums.Seiten.Yellow)
                throw new FalscherSpielerzugException("Derzeit ist Rot an der Reihe. Spielen Sie eine rotes Spielstein ab.");

            this._Spielbrett.AddSpielstein(spielstein, spalteIndex);

            if (!CheckForWinOrDraw()) // Need to switch the sides that are currently playing around
            {
                if (this._AkutellesStatus == Enums.SpielStatus.KeinSpielstein)
                    this._AkutellesStatus = spielstein.Seite == Enums.Seiten.Red ? Enums.SpielStatus.YellowsTurn : Enums.SpielStatus.RedsTurn;
                else if (this._AkutellesStatus == Enums.SpielStatus.RedsTurn)
                    this._AkutellesStatus = Enums.SpielStatus.YellowsTurn;
                else
                    this._AkutellesStatus = Enums.SpielStatus.RedsTurn;
            }
        }

        /// <summary>
        /// Checks the board for a winner or a draw, and updates the game state accordingly.
        /// Überprüft das Spielbrett auf einen Gewinner oder ein Unentschieden und aktualisiert den Spielstatus entsprechend.
        /// </summary>
        /// <returns>True if there was a win or draw, false otherwise.</returns>
        private bool CheckForWinOrDraw()
        {
            if (this.Spielbrett != null)
            {
                for (int X = 0; X <= this._Spielbrett.Spielsteine.GetUpperBound(0); X++)
                {
                    for (int Y = 0; Y <= this._Spielbrett.Spielsteine.GetUpperBound(1); Y++)
                    {
                        var discCurrentlyChecking = this._Spielbrett.Spielsteine[X, Y];
                        if (discCurrentlyChecking != null)
                        {
                            if (this.gewinnAlgoritmus != null &&
                                this.gewinnAlgoritmus.Any(algo => algo.PruefenGewinnbedingung(this._Spielbrett, discCurrentlyChecking)))
                            {
                                this._AkutellesStatus = discCurrentlyChecking.Seite == Enums.Seiten.Red ?
                                    Enums.SpielStatus.RedWins :
                                    Enums.SpielStatus.YellowWins;
                                return true;
                            }
                        }
                    }
                }
                // Check for a draw
                if (this._Spielbrett.IstVoll)
                {
                    this._AkutellesStatus = Enums.SpielStatus.Draw;
                    return true;
                }
            }
            return false;
        }


        #endregion
    }

}
