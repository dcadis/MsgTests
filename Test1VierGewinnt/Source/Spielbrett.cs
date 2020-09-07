using System;
using System.Collections.Generic;
using System.Text;

namespace Test1VierGewinnt.Source
{
    /// <summary>
    /// Spielbrett fuer das viergewinnt
    /// </summary>
    public class Spielbrett
    {
        #region private members and properties

        public Spielstein[,] Spielsteine { get; set; }

        int _Spalten;
        /// <summary>
        /// Spalten des Spielbrett
        /// </summary>
        public int Spalten
        {
            private set { value = _Spalten; }
            get { return _Spalten; }
        }

        /// <summary>
        /// Zeielen des Spielbrett
        /// </summary>
        int _Zeilen;
        public int Zeilen
        {
            private set { value = _Zeilen; }
            get
            {
                return _Zeilen;
            }
        }

        /// <summary>
        /// Gets a value stating if the board is full.
        /// </summary>
        public bool IstVoll
        {
            get
            {
                for (int x = 0; x <= this.Spielsteine.GetUpperBound(0); x++)
                {
                    for (int y = 0; y <= this.Spielsteine.GetUpperBound(1); y++)
                        if (Spielsteine[x, y] == null)
                            return false;
                }
                return true;
            }
        }

        #endregion private members and properties

        /// <summary>
        /// Konstructor.
        /// </summary>
        /// <param name="Zeilen">The height of the game board, must be greater than 0.</param>
        /// <param name="Spalten">The width of the game board, must be greater than 0.</param>
        internal Spielbrett(int Zeilen, int Spalten)
        {
            this._Spalten = Spalten;
            this._Zeilen = Zeilen;
            this.Spielsteine = new Spielstein[Spalten, Zeilen];
        }

        #region Methods


        /// <summary>
        /// Hinzufuegt Spielstein zu Spielfeld.
        /// </summary>
        /// <param name="Spielstein"></param>
        /// <param name="RowIndex">index fuer Zeiele vo Spielstein muss hinzugefuegt. Es ist >= 0 </param>
        /// <returns>Plaz wo Spielstein wuede hinzugefuegt</returns>
        internal Tuple<int, int> AddSpielstein(Spielstein Spielstein, int RowIndex)
        {
            if (RowIndex < 0 || RowIndex > (this.Spielsteine.GetUpperBound(0)))
                throw new OutOfGameBoardBoundsException("Der Spielstein muss innerhalb der Breite des Spielbretts eingefügt werden.");

            for (int idx = 0; idx <= this.Spielsteine.GetUpperBound(1); idx++)
            {
                if (this.Spielsteine[RowIndex, idx] == null)
                {
                    this.Spielsteine[RowIndex, idx] = Spielstein;
                    Spielstein.SetKoordinaten(RowIndex, idx);

                    return new Tuple<int, int>(RowIndex, idx);
                }
            }

            throw new SpielbrettFullException("Es konnte kein leerer Steckplatz zum Einfügen des Spielsteine in die angeforderte Zeile gefunden werden..");
        }

        /// <summary>
        /// Prints out the entire board in a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = this.Spielsteine.GetUpperBound(1); y >= 0; y--)
            {
                for (int x = 0; x <= this.Spielsteine.GetUpperBound(0); x++)
                {
                    if (this.Spielsteine[x, y] == null)
                        sb.Append("X");
                    else if (this.Spielsteine[x, y].Seite == Enums.Seiten.Red)
                        sb.Append("R");
                    else
                        sb.Append("Y");
                    sb.Append(" ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        #endregion

        internal void Reset()
        {
            Spielsteine = null;
        }
    }
}
