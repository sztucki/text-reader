using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TextReaderApplication {
    using System.Threading.Tasks;
    using TextReaderApplication.Models;
    public class WordProcessor : IWordProcessor {
        private readonly IValidator<WordPair> _wordValidator;
        public WordProcessor(IValidator<WordPair> wordValidator) {
            _wordValidator = wordValidator;
        }
        /// <summary>
        /// Gets a list of the valid words 
        /// </summary>
        /// <param name="wordList"></param>
        /// <param name="startWord"></param>
        /// <param name="endWord"></param>
        /// <returns></returns>
        public async Task<List<string>> GetValidWords(List<string> wordList, string startWord, string endWord) {

            List<string> validWords = new List<string>();

            int startindex = wordList.IndexOf(startWord);
            int endindex = wordList.IndexOf(endWord);

            int numberToSelect = endindex - startindex;

            List<string> WordsInRange = wordList.Skip(startindex).Take(numberToSelect).ToList();

            
            foreach (string currentWord in WordsInRange) {
                WordPair pair = new WordPair();
                pair.CurrentWord = currentWord;

               var status =  _wordValidator.Validate(pair);
                if (!status.IsValid) {
                    continue;
                }
                pair.PreviousWord = currentWord;
                Console.WriteLine($"valid word: {currentWord}");
                validWords.Add(currentWord);   


            }

            return validWords;
        }
    }
}
