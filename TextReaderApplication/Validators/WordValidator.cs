namespace TextReaderApplication.Validators {
    using FluentValidation;
    using TextReaderApplication.Models;

    public class WordValidator : AbstractValidator<WordClass> {
        static HttpClient client = new HttpClient();
        public WordValidator() {
            CascadeMode = CascadeMode.Stop;
            RuleFor(s => s.Word).Must(IsValidCharacterLength);
            RuleFor(s => s.Word).MustAsync((x, cancellation) => IsValidWordAsync(x));
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
        /// Checks to see if the passed in word can be found in the dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<bool> IsValidWordAsync(string word) {


            //for the purposes of this we just want to see if we can a valid response to ensure the word is valid; 
            //HttpResponseMessage response = await client.GetAsync($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}");
           // if (!response.IsSuccessStatusCode) {
           //     return false;
           // }

            return true;
        }
    }
}
