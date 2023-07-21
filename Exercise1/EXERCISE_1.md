# Exercise 1 #

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
    My implementation could be optimized by conducting more tests on specific cases for the application, like world size and item/bot density, to tweak up variables that are related to the "Search For Nearest" functionality. Depending on the cluster of items within the radius of the player, there may be some value in using the Jobs System to gather the distance of each item/bot within the player's radius. The project could also be restructured better to fit even more in line with SOLID principles. My ExerciseManager class and its ExerciseManager scriptableObject are too big, which breaks the Single Responsibility Principle; with more time, I would have broken both of these down more. Also, using the Unity Profiler would also be helpful in identifying any uncaught issues with performance.
 2. How much time did you spend on your implementation?
    I was able to put about 9 - 10 hours into my implementation.
 3. What was most challenging for you?
    The most challenging aspect of this assessment was working to accomplish everything within the short amount of time I had. I would usually plan out things in more detail before starting a project, but given the time constraints I jumped right in and tried my best to structure things in a SOLID way. I had to cut some corners to get things done quickly, and I believe that made things more challenging for me.
 4. What else would you add to this exercise?
    I wanted to add NavAgents to the Bots to see them move around in the scene randomly; this was one of the requirements I had in mind when implementing my final approach to finding the nearest item/bot to the Player. I believe my implementation would work well with Bot and even Item movement.

## Optional ##

* Add Unit Tests
* Add XML docs
* Optimize finding nearest
* Add new Items/Bots automatically on key press
* Read/write Item/Bot/Player state to a file and restore on launch
* Restructure your code to leverage [SOLID](https://en.wikipedia.org/wiki/SOLID) principles. (comment and tag any revisions for this)

## Next Steps ##

* Confirm you've addressed the functional goals
* Answer the questions above by adding them to this file
* Commit and push the entire repository, with your completed project, back into a repository host of your choice (bitbucket, github, gitlab, etc.)
* Share your project URL with your Virbela contact (Recruiter or Hiring Manager)

## If you have questions ##

* Reach out to your Virbela contact (Recruiter or Hiring Manager)
