using System.Text;

namespace ConnectK.Implementations
{
    internal class Winplementations
    {
        private int _k = 4;

        internal Winplementations(int? k)
        {
            if (k != null)
                _k = k.Value;
        }

        public bool boolWinplementation(bool[] stateOfPlay, int positionOfPlay)
        {
            bool result = false;
            int cumulativeTokens = 0;
            bool[] narrowSet = stateOfPlay.Skip(positionOfPlay - _k).Take(_k * 2).ToArray();

            for (int i = 0; i < narrowSet.Length; i++)
            {
                if (stateOfPlay[i])
                    cumulativeTokens++;
                else
                    cumulativeTokens = 0;

                if (cumulativeTokens > _k - 1)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool boolWinplementationWithoutPosition(bool[] stateOfPlay)
        {
            bool result = false;
            int cumulativeTokens = 0;

            for (int i = 0; i < stateOfPlay.Length; i++)
            {
                if (stateOfPlay[i])
                    cumulativeTokens++;
                else
                    cumulativeTokens = 0;

                if (cumulativeTokens == _k)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool stringWinplementation(string board, int positionOfPlay)
        {
            StringBuilder search = new StringBuilder();

            for (int i = 0; i < _k; i++)
                search.Append('1');

            int startPosition = 0;
            int length = 0;
            if (positionOfPlay - _k > 0)
                startPosition = positionOfPlay - _k;
            if (_k * 2 > board.Length)
                length = board.Length;

            return board.Substring(startPosition, length).IndexOf(search.ToString()) > -1;

        }

        public bool stringWinplementationWithoutPosition(string board)
        {
            StringBuilder search = new StringBuilder();

            for (int i = 0; i < _k; i++)
                search.Append('1');

            return board.IndexOf(search.ToString()) > -1;

        }

        public bool byteSearchPattern(byte[] board)
        {
            byte[] search = new byte[_k];
            for (int i = 0; i < _k; i++)
                search[i] = 1;

            return board.AsSpan().IndexOf(search) > -1;
        }

    }
}
