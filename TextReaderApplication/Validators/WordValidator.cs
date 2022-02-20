namespace TextReaderApplication.Validators {
    using FluentValidation;
    using TextReaderApplication.Models;

    public class WordValidator : AbstractValidator<WordPair> {
        static HttpClient client = new HttpClient();
        public WordValidator() {
            RuleFor(s => s.CurrentWord).Must(IsValidCharacterLength);
            RuleFor(s => s.CurrentWord).MustAsync((x, cancellation) => IsValidWordAsync(x));
            RuleFor(s => s.CurrentWord).Must((x, currentWord) =>
    DoWordsDifferByMoreThanOneCharacter(x.PreviousWord, currentWord));
        }



        /// <summary>
        /// Checks to see if the word is the correct length
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private bool IsValidCharacterLength(string word) {
            if (word.Length == 4) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// See if words differ by more than one character
        /// </summary>
        /// <param name="previousWord"></param>
        /// <param name="currentWord"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Checks to see if the passed in word can be found in the dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
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
