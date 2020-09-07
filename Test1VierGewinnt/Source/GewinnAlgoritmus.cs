using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1VierGewinnt.Source
{
    public class GewinnAlgoritmus
    {
    }

    /// <summary>
    /// Interface zum Definieren eines Algorithmus zum Überprüfen auf eine Gewinnbedingung.
    /// </summary>
    public interface IGewinnAlgoritmus
    {
        bool PruefenGewinnbedingung(Spielbrett speilfeld, Spielstein spielstein);
    }

    /// <summary>
    /// Führt eine horizontale Überprüfung der Gewinnbedingungen durch
    /// </summary>
    public class HorizontalGewinnAlgoritmus : IGewinnAlgoritmus
    {
        public bool PruefenGewinnbedingung(Spielbrett speilfeld, Spielstein spielstein)
        {
            int anzahlDasselbeFarbe = 0; 

            for (int x = spielstein.XKoordinate.Value; x <= Math.Min((spielstein.XKoordinate.Value + Spiel.STEINLAENGE), speilfeld.Spielsteine.GetUpperBound(0)); x++)
            {
                if (speilfeld.Spielsteine[x, spielstein.YKoordinate.Value] != null && speilfeld.Spielsteine[x, spielstein.YKoordinate.Value].Seite == spielstein.Seite)
                    anzahlDasselbeFarbe++;
                else
                    break;
            }

            if (anzahlDasselbeFarbe >= Spiel.STEINLAENGE)
                return true; // Gewinn Bedingung

            anzahlDasselbeFarbe = 0; // Re-initialise to start the backward count

            // Check backward. We need to make sure we don't go lower than 0 in our check.
            for (int x = spielstein.XKoordinate.Value; x >= Math.Max(spielstein.XKoordinate.Value - Spiel.STEINLAENGE, 0); x--)
            {
                if (speilfeld.Spielsteine[x, spielstein.YKoordinate.Value] != null && speilfeld.Spielsteine[x, spielstein.YKoordinate.Value].Seite == spielstein.Seite)
                    anzahlDasselbeFarbe++;
                else
                    break;
            }

            return anzahlDasselbeFarbe >= Spiel.STEINLAENGE;
        }
    }

    /// <summary>
    /// Performs vertical checks for winning conditions.
    /// </summary>
    public class VerticalGewinnAlgoritmus : IGewinnAlgoritmus
    {
        public bool PruefenGewinnbedingung(Spielbrett speilfeld, Spielstein spielstein)
        {
            int countOfSameColour = 0; // Number of discs with the same colour

            // Check upwards. We need to take into consideration the height of the speilfeld when looping
            for (int y = spielstein.YKoordinate.Value; y <= Math.Min((spielstein.YKoordinate.Value + Spiel.STEINLAENGE), speilfeld.Spielsteine.GetUpperBound(1)); y++)
            {
                if (speilfeld.Spielsteine[spielstein.XKoordinate.Value, y] != null && speilfeld.Spielsteine[spielstein.XKoordinate.Value, y].Seite == spielstein.Seite)
                    countOfSameColour++;
                else
                    break;
            }

            if (countOfSameColour >= Spiel.STEINLAENGE)
                return true; // Gewinn Bedingung

            countOfSameColour = 0; // Re-initialise to start the backward count

            // Check downward. We need to make sure we don't go lower than 0 in our check.
            for (int y = spielstein.YKoordinate.Value; y >= Math.Max(spielstein.YKoordinate.Value - Spiel.STEINLAENGE, 0); y--)
            {
                if (speilfeld.Spielsteine[spielstein.XKoordinate.Value, y] != null && speilfeld.Spielsteine[spielstein.XKoordinate.Value, y].Seite == spielstein.Seite)
                    countOfSameColour++;
                else
                    break;
            }

            return countOfSameColour >= Spiel.STEINLAENGE;
        }
    }

    /// <summary>
    /// Performs diagonal checks for winning conditions.
    /// </summary>
    public class DiagonalGewinnAlgoritmus : IGewinnAlgoritmus
    {
        public bool PruefenGewinnbedingung(Spielbrett speilfeld, Spielstein spielsteine)
        {
            int countOfSameColour = 0; // Number of discs with the same colour

            // There are 4 diagonal win conditions, we have to check for all of them. It seems complex but really it is merely a combination
            // of positive, negative, X and Y

            // 1) Let's tackle positive X, positive Y first - this means we're checking for "up and right" in the 2d space (left hand rule)
            // We are incrementing both X and Y axis values
            for (int i = 0; i < Spiel.STEINLAENGE; i++)
            {
                if (spielsteine.XKoordinate.Value + i <= speilfeld.Spielsteine.GetUpperBound(0) && spielsteine.YKoordinate.Value + i <= speilfeld.Spielsteine.GetUpperBound(1))
                {
                    if (speilfeld.Spielsteine[spielsteine.XKoordinate.Value + i, spielsteine.YKoordinate.Value + i] != null && speilfeld.Spielsteine[spielsteine.XKoordinate.Value + i, spielsteine.YKoordinate.Value + i].Seite == spielsteine.Seite)
                        countOfSameColour++;
                    else
                        break; 
                }
                else
                    break;
            }

            if (countOfSameColour >= Spiel.STEINLAENGE)
                return true; // Gewinn Bedingung

            countOfSameColour = 0; // Re-initialise

            // 2) Next, positive X, negative Y - this is checking "down and right" in the 2d space
            // We are incrementing X axis values but decrementing Y axis values

            for (int i = 0; i < Spiel.STEINLAENGE; i++)
            {
                if (spielsteine.XKoordinate.Value + i <= speilfeld.Spielsteine.GetUpperBound(0) && spielsteine.YKoordinate.Value - i >= 0)
                {
                    if (speilfeld.Spielsteine[spielsteine.XKoordinate.Value + i, spielsteine.YKoordinate.Value - i] != null && speilfeld.Spielsteine[spielsteine.XKoordinate.Value + i, spielsteine.YKoordinate.Value - i].Seite == spielsteine.Seite)
                        countOfSameColour++;
                    else
                        break; // No point continuing
                }
                else
                    break; // No point continuing
            }

            if (countOfSameColour >= Spiel.STEINLAENGE)
                return true; // Win condition

            countOfSameColour = 0; // Re-initialise

            // 3) Next, negative X, positive Y - this is checking "up and left" in the 2d space
            // Decrement X axis values but increment Y axis values

            for (int i = 0; i < Spiel.STEINLAENGE; i++)
            {
                if (spielsteine.XKoordinate.Value - i >= 0 && spielsteine.YKoordinate.Value + i <= speilfeld.Spielsteine.GetUpperBound(1))
                {
                    if (speilfeld.Spielsteine[spielsteine.XKoordinate.Value - i, spielsteine.YKoordinate.Value + i] != null && speilfeld.Spielsteine[spielsteine.XKoordinate.Value - i, spielsteine.YKoordinate.Value + i].Seite == spielsteine.Seite)
                        countOfSameColour++;
                    else
                        break; // No point continuing
                }
                else
                    break; // No point continuing
            }

            if (countOfSameColour >= Spiel.STEINLAENGE)
                return true; // Win condition

            countOfSameColour = 0; // Re-initialise

            // 4) Lastly, negative X, negative Y - this is checking "down and left" in the 2d space
            // Decrement both X and Y axis values
            for (int i = 0; i < Spiel.STEINLAENGE; i++)
            {
                if (spielsteine.XKoordinate.Value - i >= 0 && spielsteine.YKoordinate.Value - i >= 0)
                {
                    if (speilfeld.Spielsteine[spielsteine.XKoordinate.Value - i, spielsteine.YKoordinate.Value - i] != null && speilfeld.Spielsteine[spielsteine.XKoordinate.Value - i, spielsteine.YKoordinate.Value - i].Seite == spielsteine.Seite)
                        countOfSameColour++;
                    else
                        break; // No point continuing
                }
                else
                    break; // No point continuing
            }

            return countOfSameColour >= Spiel.STEINLAENGE;
        }
    }
}
