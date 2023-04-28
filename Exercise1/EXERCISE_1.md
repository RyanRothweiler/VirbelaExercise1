# Information and Usage
- All optional challenges implemented.
- Project is in 2020.3.47f1
- Playmode and Edit mode test are included.
- Save file
	- Press S during gameplay to save. 
	- Save file is written to C:\Users\{user}\AppData\LocalLow\DefaultCompany\VirbelaExercise1RyanRothweiler
	- Save file will be automatically loaded on start, if it exists. Delete file to prevent it from loading.
	- Save file will save and load all HighlightItems. Even ones placed manually in the scene. This can result in duplicating objects if they're not deleted before loading. This is an improvement that could be made to the save system.
- There are four radial spawners in the scene. 
	- Two which spawn bots, and two which spawn items.
	- All spawners will spawn on start.
	- The first Bot spawner is setup to spawn a bot on pressing B.
	- The first Item spawner is setup to spawn a bot on pressing I.
- Highlight nearest options
	- I implemented two ways to highlight.
		- HighlightOneBehavior will highlight the nearest item or bot.
		- HighlightTypeBehavior will highlight the nearest either item or bot as defined in a local spawnType variable.
			- If spawnType is set to Bot, it will only highlight bots and never items.
	- The Player prefab has one HighlightOneBehavior and two HighlightTypeBehaviors.
		- By default the HighlightOneBehavior is enabled and the HighlightTypeBehaviors are both disabled. This setup will highlight the nearest item, be it item or bot doesn't matter, as defined by the exercise.
		- If you disable the HighlightOneBehavior and enable the two HighlightTypeBehaviors, then the nearest Item AND the nearest bot will be highlighted. This was not requested by the exercise but I included it because it seemed like a fun extension.

# Exercise 1

In this exercise you'll configure a Unity scene and write scripts to create an interactive experience. As you progress through the steps, feel free to add comments to the code about *why* you choose to do things a certain way. Add comments if you felt like there's a better, but more time intensive way to implement specific functionality. It's OK to be more verbose in your comments than typical, to give us a better idea of your thoughts when writing the code.

## What you need ##

* Unity 2020 (latest, or whatever you have already)
* IDE of your choice
* Git

## Instructions ##

This test is broken into multiple phases. You can implement one phase at a time or all phases at once, whatever you find to be best for you.

### Phase 1 ###

**Project setup**:

 1. Create a new Unity project inside this directory, put "Virbela" and your name in the project name.
 1. Configure the scene:
     1. Add a central object named "Player"
     1. Add 5 objects named "Item", randomly distributed around the central object
 1. Add two C# scripts named "Player" and "Item" to your project
     1. Attach the scripts to the objects in the scene according to their name, Item script goes on Item objects, Player script goes on Player object.
     1. You may use these scripts or ignore them when pursuing the Functional Goals, the choice is yours. You're free to add any additional scripts you require to meet the functional goals.

**Functional Goal 1**:

When the game is running, make the Item closest to Player turn red. One and only one Item is red at a time. Ensure that when Player is moved around in the scene manually (by dragging the object in the scene view), the closest Item is always red.

### Phase 2 ###

**Project modification**:

 1. Add 5 objects randomly distributed around the central object with the name "Bot"
 1. Add a C# script named "Bot" to your project.
 1. Attach the "Bot" script to the 5 new objects.
     1. Again, you may use this script or ignore it when pursing the Functional Goals.

**Functional Goal 2**:

When the game is running, make the Bot closest to the Player turn blue. One and only one object (Item or Bot) has its color changed at a time. Ensure that when Player is moved around in the scene manually (by dragging the object in the scene view), the closest Item is red or the closest Bot is blue.

### Phase 3 ###

**Functional Goal 3**:

Ensure the scripts can handle any number of Items and Bots.

**Functional Goal 4**:

Allow the designer to choose the base color and highlight color for Items/Bots at edit time.

## Questions ##

 1. How can your implementation be optimized?
	- A KD tree datastructure is already quite fast , I'm not aware of other faster structures but I'm sure they exist. The KD implementation could be improved by adding balancing, and starting by inserting the center most point. Most of the optimization will be in reducing the set of points searched using other context. For example if the game has rooms then only search the points within the same room as the player. 
 2. How much time did you spend on your implementation?
	 -  Total of 11.5 hours currently, as of writing. Likely another 0.5 hours to finish this documentation.
		 - 2.5 hours for the nearest nighbor implementations of naive and KD tree.
		 - 1 hour for test writing and setup
		 - 2.5 hours to dependency injection work
		 - 1 hour for spawning
		 - 2 hours for highlight abstractions and two highlight methods
		 - 2.5 hours for miscelaneous. Project setup, reading exercise, xml comments, etc
 3. What was most challenging for you?
	 - The dependency injection. Its not something I'm very familiar with. I'm aware of it, but haven't used it in practice besides tinkering.
 4. What else would you add to this exercise?
	 1. Possibly additional gameplay. Additions like collisions, scene changes, player input, and prefab setup might give additional insight.

## Optional ##

* Find a way to make use of dependency injection when implementing the functional goals. Feel free to use an existing framework or create your own.
* Add Unit Tests
* Add XML docs
* Optimize finding nearest
* Add new Items/Bots automatically on key press
* Read/write Item/Bot/Player state to a file and restore on launch

## Next Steps ##

* Confirm you've addressed the functional goals
* Answer the questions above by adding them to this file
* Commit and push the entire repository, with your completed project, back into a repository host of your choice (bitbucket, github, gitlab, etc.)
* Share your project URL with your Virbela contact (Recruiter or Hiring Manager)

## If you have questions ##

* Reach out to your Virbela contact (Recruiter or Hiring Manager)
