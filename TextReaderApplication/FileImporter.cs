using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReaderApplication {
    internal class FileImporter: IFileImporter {
    
    
        public void ImportFile() {
            List<string> lines = File.ReadAllLines(@"C:\Users\Ben Sztucki\Desktop\words-english.txt").ToList();
            //moot 
            //morel
            lines.Sort();

            int startindex = lines.IndexOf("burg");
            int endindex = lines.IndexOf("bush");

            int numberToSelect = endindex - startindex;
            Console.WriteLine($"start: {startindex}, end: {endindex}");
            Console.WriteLine($"start: {"burg"}, end: {"bush"}");

            List<string> WordsInRange =  lines.Skip(startindex).Take(numberToSelect).ToList();

            string previousValidWord = ""; 
            foreach (string word in WordsInRange) {
               
                //check length
                if(word.Length != 4) {
                    continue;
                }

                //check is valid word
               // Console.WriteLine($"4 letter word: {word}");

                //see if current and previous word differ by 1 or more character
                List<char> currentletters = word.ToCharArray().ToList();
                List<char> letters = previousValidWord.ToCharArray().ToList();

                int i = 0;
                int matchingLetterCount = 0;
                while (i < letters.Count) {
                    if(currentletters[i] == letters[i]) {
                        matchingLetterCount++;
                    }

                    i++;
                }

                if (matchingLetterCount >= 3 || previousValidWord == "") {                   
                    previousValidWord = word;
                    Console.WriteLine($"valid word: {word}");
                }
            }


        }


       
    }
}
