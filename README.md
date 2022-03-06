# Text Reader console application

## Process for creating this application 

### The console application

The first step was deciding whether to create a .net 5 or .net 6 console application. For this is decided to do with the latter as it's a bit newer and I haven't looked at it yet. 

From there I need to look at the altnerative setup in the program.cs file and get that set up so I could bebug accoringly.

To make things a bit easier during testing I have the minimum logging level to warnings so you don't have text appearing once the console application starts. 

### Tackling the problem

Apologies for solving the wrong problem.

After looking at the video I realised the problem that you were asking me to solve was a lot more complicated that what I had orinially implemented. 

Based on the problem it appeared that there were going to be a very large number of possible options which I would have to loop through and narrow down. 

The first step was to do some intial searching on possible ways in order to solve the problem. This lead to be looking at Breadth-first search which is something that I haven't come across previously.

From there I was able to plan out the steps that would be needed in order to get the list of words that would be needed. The first thing I was going to need was a list of all the possible words and variations that could exist for a particular word. This would allow me to construct word trees of all the possbile options. 

One of the main issues I then had was thinking about how I was going to be able to loop through the a list of words and be able to add addtional items to the loop mid flight. This pointed me in the direction of the group class.

Next i needed to process through the possible options for a word passed into the queue. I could then pass thing into my varation dictionary and then see if that word appeared in my word tree list. From here I just needed to ensure that only the shortest paths got added to the list. 






### The information input and fluent validtion

When creating the input section I wanted to ensure that the user would input the correct information. initally to do this I had inline validation. This seemed to work but a little crudely so I decided to bring in fluent validation. This make things a lot easier and ensured that I could abstract the needed valdition. Once this was implemented I decided to go one step further and continue to abstract the word validation.  

### Folder structure

With everything I needed in place I wanted to split files out into more relevant folders like I normally would to make things easier to find and to add a bit more structure to the application.


### Unit Testing 

I have added a Unit Test solution and the relevant files for what I would like to test. However, I haven't had the time to actually write the tests due to other commitments. 

### Things to improve

If I had a bit more time to complete the exercise I would have liked to add in some addition validation to ensure that the start and end words exist in the file before choosing them. 

As previously mentioned ideally I would have also liked to add in the unit testing. 

Some additional exception handling logic would also have been helpful so that useful messages could be displayed to the user if an error did occur. The exceptions could be logged to the debug console or to a local database depending on how much time is available. 

It would also have been nice to have checked to see if the output path existed and if it didn't create the file rather than depending on it being there. 

Given I know the format of the file I was able to create something relatively quickly. However, it would have been nice to be able to check the format or the file to ensure that there is a new word on every line before importing it and maybe having the ability to break words on to new lines if needed. 