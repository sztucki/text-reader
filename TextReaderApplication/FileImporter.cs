using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReaderApplication {
    internal class FileImporter : IFileImporter {
       

  
        public async Task<List<string>> GetFileWords(string filePath) {
            List<string> lines = await Task.Run(() => File.ReadAllLines(filePath).ToList());
           return lines;
            //moot 
            //morel
            //lines.Sort();

            //int startindex = lines.IndexOf("burg");
            //int endindex = lines.IndexOf("bush");

            //int numberToSelect = endindex - startindex;
            //Console.WriteLine($"start: {startindex}, end: {endindex}");
            //Console.WriteLine($"start: {"burg"}, end: {"bush"}");

            //List<string> WordsInRange = lines.Skip(startindex).Take(numberToSelect).ToList();

            //string previousValidWord = "";
            //foreach (string word in WordsInRange) {

            //    //check length
            //    if (word.Length != 4) {
            //        continue;
            //    }

            //    //check is valid word
            //    // Console.WriteLine($"4 letter word: {word}");

            //    //see if current and previous word differ by 1 or more character
            //    List<char> currentletters = word.ToCharArray().ToList();
            //    List<char> letters = previousValidWord.ToCharArray().ToList();

            //    int i = 0;
            //    int matchingLetterCount = 0;
            //    while (i < letters.Count) {
            //        if (currentletters[i] == letters[i]) {
            //            matchingLetterCount++;
            //        }

            //        i++;
            //    }

            //    if (matchingLetterCount >= 3 || previousValidWord == "") {
            //        previousValidWord = word;
            //        Console.WriteLine($"valid word: {word}");
            //    }
            //}


        }

        /// <summary>
        /// Ensure that the file path is valid before trying to read it
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> IsValidFilePath(string filePath) {
            return await Task.Run(() => File.Exists(filePath));
        }


        /// <summary>
        /// Ensure that a text file is being imported
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> IsValidFileType(string filePath) {
            string fileType = await Task.Run(() => Path.GetExtension(filePath));
            if (fileType != "txt") {
                return false;
            }

            return true;
        }
 
    }
}
