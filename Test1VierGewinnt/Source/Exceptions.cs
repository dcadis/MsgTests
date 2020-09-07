using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1VierGewinnt.Source
{
    public class OutOfGameBoardBoundsException : Exception
    {
        public OutOfGameBoardBoundsException()
        {

        }
        public OutOfGameBoardBoundsException(string Message)
            : base(Message)
        { }
    }

    public class SpielbrettFullException : Exception
    {
        public SpielbrettFullException()
        {

        }
        public SpielbrettFullException(string Message)
            : base(Message)
        { }
    }

    public class InvalidSpielbrettDimensionsException : Exception
    {
        public InvalidSpielbrettDimensionsException()
        {

        }
        public InvalidSpielbrettDimensionsException(string Message)
            : base(Message)
        { }
    }

    public class FalscherSpielerzugException : Exception
    {
        public FalscherSpielerzugException()
        {

        }
        public FalscherSpielerzugException(string Message)
            : base(Message)
        { }
    }

    public class FalschesInputTurns : ArgumentException
    {
        public FalschesInputTurns()
        {

        }
        public FalschesInputTurns(string Message)
            : base(Message)
        { }
    }

    public class SpielBeebdetException : ArgumentException
    {
        public SpielBeebdetException()
        {

        }
        public SpielBeebdetException(string Message)
            : base(Message)
        { }
    }
}
