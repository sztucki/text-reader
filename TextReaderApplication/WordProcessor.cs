using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReaderApplication {
    public class WordProcessor : IWordProcessor {
        static HttpClient client = new HttpClient();

        public async Task<List<string>> GetValidWords(List<string> wordList, string startWord, string endWord) {

            List<string> validWords = new List<string>();

            int startindex = wordList.IndexOf(startWord);
            int endindex = wordList.IndexOf(endWord);

            int numberToSelect = endindex - startindex;

            List<string> WordsInRange = wordList.Skip(startindex).Take(numberToSelect).ToList();

            string previousValidWord = "";
            foreach (string currentWord in WordsInRange) {


                if (!IsValidCharacterLength(currentWord)) {
                    continue;
                }

                if (! await IsValidWordAsync(currentWord)) {
                    continue;
                }

                if (!DoWordsDifferByMoreThanOneCharacter(previousValidWord, currentWord)) {
                    continue;
                }

                previousValidWord = currentWord;
                Console.WriteLine($"valid word: {currentWord}");

            }

            return validWords;
        }

        private bool IsValidCharacterLength(string word) {
            if (word.Length == 4) {
                return true;
            }
            return false;
        }

        private bool DoWordsDifferByMoreThanOneCharacter(string previousWord, string currentWord) {
            //see if current and previous word differ by 1 or more character
            List<char> currentLetters = currentWord.ToCharArray().ToList();
            List<char> previousLetters = previousWord.ToCharArray().ToList();

            int i = 0;
            int matchingLetterCount = 0;
            while (i < previousLetters.Count) {
                if (currentLetters[i] == previousLetters[i]) {
                    matchingLetterCount++;
                }

                i++;
            }

            if (matchingLetterCount >= 3 || previousWord == "") {
                return true;
            }

            return false;
        }



        
        public async Task<bool> IsValidWordAsync(string word) {


            //for the purposes of this we just want to see if we can a valid response to ensure the word is valid; 
            HttpResponseMessage response = await client.GetAsync($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}");
            if (!response.IsSuccessStatusCode) {
                return false;
            }

            return true;
        }
    }
}
