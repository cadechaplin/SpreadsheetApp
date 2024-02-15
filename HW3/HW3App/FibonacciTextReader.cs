namespace HW3AvaloniaApp
{
    using System.IO;
    using System.Numerics;
    using System.Text;

    /// <summary>
    /// Class that takes a integer in the constructor and then is able to return Fibonacci
    /// numbers up until this number using ReadLine(), ReadToEnd() returns a string of all
    /// numbers until limit set in constructor. This class inherets from a TextReader so that
    /// it can be passed to a function accepting TextReader.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        // ReSharper disable InconsistentNaming
        private readonly int maxLines;
        private int currentLine;
        private BigInteger currentFibonacci;
        private BigInteger previousFibonacci;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// /// <param name="maxLines">Determines how many fibonacci numbers will be able to be returned.</param>
        public FibonacciTextReader(int maxLines)
        {
            this.maxLines = maxLines;
            this.currentLine = 0;
            this.currentFibonacci = 0;
            this.previousFibonacci = 0;
        }

        /// <summary>
        /// Calculates next Fibonacci Number.
        /// </summary>
        /// <returns>
        /// Returns a single line in the form of a string containing the
        /// currentLine number and the correlating Fibonacci Number. Returns null if
        /// maxLines is reached.
        /// </returns>
        public override string? ReadLine()
        {
            if (this.currentLine == 0 && this.maxLines > 0)
            {
                this.currentLine++;
                return "1: 0";
            }
            else if (this.currentLine == 1 && this.maxLines > 1)
            {
                this.currentLine++;
                this.currentFibonacci = 1;
                return "2: 1";
            }
            else if (this.currentLine < this.maxLines)
            {
                this.currentFibonacci = this.currentFibonacci + this.previousFibonacci;
                this.previousFibonacci = this.currentFibonacci - this.previousFibonacci;
                this.currentLine++;

                return $"{this.currentLine}: {this.currentFibonacci}";
            }
            else
            {
                return null; // Return null after reaching the specified number of lines
            }
        }

        /// <summary>
        /// Runs ReadLine() until null is returned, which means the max numbers has been reached.
        /// </summary>
        /// <returns>
        /// Returns all string containing the currentLine and Fibonacci number for all remaining
        /// lines from currentLine to maxLines.
        /// </returns>
        public override string ReadToEnd()
        {
            StringBuilder myString = new StringBuilder();
            while (true)
            {
                string? line = this.ReadLine();
                if (line == null)
                {
                    break;
                }

                myString.AppendLine(line);
            }

            return myString.ToString();
        }
    }

}