# Text Reader console application

## Process for creating this application 

### The console application

The first step was deciding whether to create a .net 5 or .net 6 console application. For this is decided to do with the latter as it's a bit newer and I haven't looked at it yet. 

From there I need to look at the altnerative setup in the program.cs file and get that set up so I could bebug accoringly.

To make things a bit easier during testing I have the minimum logging level to warnings so you don't have text appearing once the console application starts. 

###T ackling the problem

Now that i had a console application the next step was to review the problem which i needed to solve. Based on this the first step was to be able to get hold of the words in the file.

The easiest option to this was to read all the lines of the file out into a list so that I could loop through them more easily. Then from here i would be able to loop through the words and do the checks on the words. 

Initially I just wanted to ensure that i would be able to complete the challenge. So i wanted to make sure that i could get the word comparison working correctly. From here I was then able to start in adding the addtional validation to ensure that the acceptance criteria in the brief were being met. 

The interesting bit here was making sure that I was able ensure that the words that i was processing were valid. For this a simple check to a Dictionary API make things easy. 

### The information input and fluent validtion

When creating the input section i wanted to ensure that the user would input the correct information. initally to do this i had inline validation. This seemed to work but a little crudely so i decided to bring in fluent validation. This make things a lot easier and ensured that i could abstract the needed valdition. Once this was implemented I decided to go one step further and continue to abstract the word validation.  

### Folder structure

With everything i needed in place i wanted to split files out into more relevant folders like i normally would to make things easier to find and to add a bit more structure to the application.