using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1VierGewinnt.Source
{
    /// <summary>
    /// Spielstein Klass mit identifikation des 
    /// </summary>
    public class Spielstein
    {
        #region Private Fields

        /// <summary>
        /// die Seite des Spieler, der die Spielstein hinzugefuegt hat.
        /// </summary>
        Enums.Seiten _Seite;
        public Enums.Seiten Seite
        {
            private set { _Seite = value; }
            get { return this._Seite; }
        }
        /// <summary>
        /// Die X-Achsen-Koordinate dieser Disc, wenn sie dem Spielbrett hinzugefügt wurde (>= 0)
        /// </summary>
        int? _XKoordinate = null;
        /// <summary>
        /// Die Y-Achsen-Koordinate dieser Disc, wenn sie dem Spielbrett hinzugefügt wurde (>= 0)
        /// </summary>
        int? _YKoordinate = null;

        #endregion

        /// <summary>
        /// Konstructor.
        /// </summary>
        /// <param name="Seite"></param>
        public Spielstein(Enums.Seiten Seite)
        {
            this._Seite = Seite;
        }

        #region Properties

        /// <summary>
        /// Die X-Achsen-Koordinate dieser Spielstein. Wenn dieser Spielstein zu einem Spielbrett hinzugefügt wurde, liegt es in der Verantwortung des Spielbretts, diesen Wert festzulegen.
        /// </summary>
        public int? XKoordinate { get { return _XKoordinate; } }

        /// <summary>
        /// Die Y-Achsen-Koordinate dieser Spielstein. Wenn dieser Spielstein zu einem Spielbrett hinzugefügt wurde, liegt es in der Verantwortung des Spielbretts, diesen Wert festzulegen.
        /// </summary>
        public int? YKoordinate { get { return _YKoordinate; } }

        #endregion

        #region Methods

        

        /// <summary>
        /// Koordinaten dieser Spielstein auf einem Spielbrett.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        internal void SetKoordinaten(int X, int Y)
        {
            this._XKoordinate = X;
            this._YKoordinate = Y;
        }

        #endregion
    }
}
