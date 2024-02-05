using System.IO;
using System.Numerics;
using System.Text;

namespace FibancciTextReader
{
    using System.IO;
    using System.Numerics;
    using System.Text;

    public class FibonacciTextReader : TextReader
    {
        private readonly int maxLines;
        private int currentLine;
        private BigInteger currentFibonacci;
        private BigInteger previousFibonacci;

        public FibonacciTextReader(int maxLines)
        {
            this.maxLines = maxLines;
            currentLine = 0;
            currentFibonacci = 0;
            previousFibonacci = 0;
        }

        public override string ReadLine()
        {
            if (currentLine == 0 && maxLines > 0)//might need changing
            {
                currentLine++;
                return "1: 0";
            }
            else if (currentLine == 1 && maxLines > 1)//might need changing
            {
                currentLine++;
                currentFibonacci = 1;
                return "2: 1";
            }
            else if (currentLine < maxLines)
            {
                currentFibonacci = currentFibonacci + previousFibonacci;
                previousFibonacci = currentFibonacci - previousFibonacci;
                currentLine++;

                return $"{currentLine}: {currentFibonacci}";
            }
            else
            {
                return null; // Return null after reaching the specified number of lines
            }
        }

        public override string ReadToEnd()
        {
            StringBuilder myString = new StringBuilder();
            string line;
            while ((line = ReadLine()) != null)
            {
                myString.AppendLine(line);
            }

            return myString.ToString();
        }
    }

}