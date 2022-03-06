using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TextReaderApplication {
    using System.Threading.Tasks;
    using TextReaderApplication.Models;
    public class WordProcessor : IWordProcessor {
        private readonly IValidator<WordClass> _wordValidator;
        public WordProcessor(IValidator<WordClass> wordValidator) {
            _wordValidator = wordValidator;
        }
        /// <summary>
        /// Gets a list of the valid words 
        /// </summary>
        /// <param name="wordList"></param>
        /// <param name="startWord"></param>
        /// <param name="endWord"></param>
        /// <returns></returns>
        public List<string> GetValidWords(List<string> wordList, string startWord, string endWord) {
            
            List<string> validWords = new List<string>();

            int startindex = wordList.IndexOf(startWord);
            int endindex = wordList.IndexOf(endWord);

            int numberToSelect = endindex - startindex;


            wordList.Sort();
            //had to add in the plus one here due to the last item not returning
            List<string> WordsInRange = wordList.Skip(startindex).Take(numberToSelect + 1).ToList();


            //get all of the words that differ by 1 letter from the first word.
            
            foreach (string currentWord in WordsInRange) {
                WordClass word = new WordClass(currentWord);
                var status = _wordValidator.Validate(word);
                if (!status.IsValid) {
                    continue;
                }
                validWords.Add(word.Word);


            }


            return validWords;
        }

        public List<string> ProcessWords(List<string> wordsInRange, string startWord, string endWord) {      
            wordsInRange.Sort();
            var wordVariationDictionary = new Dictionary<string, List<string>>();
            foreach (string word in wordsInRange) {
                AddWordToDictionary(word, wordVariationDictionary);
            }

            var wordTrees = new Dictionary<string, List<List<string>>>();
            wordTrees[startWord] = new List<List<string>> { new List<string>() { startWord } };

            //creating a queue to be able to process the words
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(startWord);
            List<string> processedWords = new List<string>();
            while (queue.Count > 0) {

                //get an item from the queue to process
                string currentWord = queue.Dequeue();
                if (currentWord == endWord) {
                    return wordTrees[endWord].OrderBy(x => x.Count).First();
                }
                else {
                    if (processedWords.Contains(currentWord)) {
                        continue;
                    }

                    processedWords.Add(currentWord);

                    //Here we want to loop through each character of the current word so that we can get through all the different variations
                    // eg *end, b*end
                    foreach (char character in currentWord) {    
                        string treatedWord = currentWord.Replace(character, '*');

                        //if the word isnt in the dictionary ignore it
                        if (!wordVariationDictionary.ContainsKey(treatedWord)) {
                            continue;
                        }

                        //here we are proccessing through all of the possible word variations
                        foreach (string wordVaryingByOneCharacter in wordVariationDictionary[treatedWord]) {
                            if (processedWords.Contains(wordVaryingByOneCharacter)) {
                                continue;
                            }

                            //loop through all of the current word trees for the current word
                            foreach (var path in wordTrees[currentWord]) {
                                List<string> newPath = new List<string>(path);
                                newPath.Add(wordVaryingByOneCharacter);


                            //if the word we are currently looping through doesnt have its own tree yet then add one
                                if (!wordTrees.ContainsKey(wordVaryingByOneCharacter)) {
                                    wordTrees[wordVaryingByOneCharacter] = new List<List<string>>() { newPath };
                                }

                                //if the path is already in the list and this one is short then add it in. 
                                else if (wordTrees[wordVaryingByOneCharacter][0].Count > newPath.Count) {
                                    wordTrees[wordVaryingByOneCharacter].Add(newPath);
                                }                                                              
                            }
                            queue.Enqueue(wordVaryingByOneCharacter);
                        }
                    }
                }
            }

            return new List<string>();

        }



        private void AddWordToDictionary(string word, Dictionary<string, List<string>> wordVariationDictionary) {
            foreach (char character in word) {
                if (wordVariationDictionary.ContainsKey(word.Replace(character, '*'))) {
                    wordVariationDictionary[word.Replace(character, '*')].Add(word);
                }
                else {
                    wordVariationDictionary.Add(word.Replace(character, '*'), new List<string>() { word });
                }
            }
        }
    }
}



