using System;
using System.Collections.Generic;
using System.Linq;

namespace TypingGame.Logic
{
    public class StringFileReader : IStringFileReader
    {
        private IFileStreamReader fileStreamReader;

        public StringFileReader(IFileStreamReader fileStreamReader)
        {
            this.fileStreamReader = fileStreamReader;
        }

        public List<string> GetBatchOfStrings(int requestedNoOfElements)
        {
            var noOfElements = requestedNoOfElements > SourceFileConfiguration.BATCH_SIZE ? requestedNoOfElements : SourceFileConfiguration.BATCH_SIZE;
            var lineNumbers = GetLineNumbers(noOfElements);
            int index = 0;
            var batchOfStrings = new List<string>();

            for (int i = 0; i < SourceFileConfiguration.MAX_LINE_NUMBER; i++)
            {
                if (index >= lineNumbers.Length || fileStreamReader.EndOfStream)
                {
                    break;
                }

                var line = fileStreamReader.ReadLine();

                if(lineNumbers[index] == i)
                {
                    batchOfStrings.Add(line);
                    index++;
                }
            }

            return ShuffleBatch(batchOfStrings);
        }

        private int[] GetLineNumbers(int noOfElements)
        {
            Random random = new Random();
            var lineNumbers = new int[noOfElements];

            for (int i = 0; i < noOfElements; i++)
            {
                lineNumbers[i] = (random.Next(SourceFileConfiguration.MAX_LINE_NUMBER));
            }

            Array.Sort(lineNumbers);

            return lineNumbers;
        }

        private List<string> ShuffleBatch(List<string> inputBatch)
        {
            var random = new Random();
            return inputBatch.OrderBy(x => random.Next()).ToList();
        }
    }
}
