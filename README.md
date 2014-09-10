### Depressurizer
for v0.5.1.0

-----

### Summary

Depressurizer is a program aimed at making it a bit easier to manage large Steam game libraries.

In addition to providing a way to quickly and easily modify games' assigned categories, it also lets you mark them as Favorites or as hidden.

Depressurizer can also autocategorize your games for you. Currently, it can do so based on the genres attached to the game in the Steam store as well as the Store flags (like MultiPlayer, Controller Support, etc.)

It also saves your configuration information independently of Steam, providing an automatic backup in the event that Steam loses your configuration.

-----

### Requirements

 - Depressurizer requires the .NET Framework 4.5.1.
	
 - It must be run on the same computer on which you use Steam, or one one of the computers on which you use Steam.

 - To obtain an accurate list of the games on your account, it also requires that your profile on http://www.steamcommunity.com be set to "public".

-----
### Usage Guide

#### Getting Started

Download the latest version of Depressurizer from the [project's Release page](https://github.com/rallion/depressurizer/releases).
		
The first time you run Depressurizer, it will ask you for your Steam directory. If it is not automatically and correctly detected, fill this in.
		
Next, it will ask you to set up a profile. The easiest thing to do here is to simply select your profile from the "Select User" list and click OK. However, if you also want to manage any non-Steam games that you have added to Steam, first click the "Ignored Games" tab and uncheck "Ignore external games".
		
After a moment, you should see the game list fill up with all your games. You can now categorize them as you wish.

When you are ready to save your changes, you MUST FIRST COMPLETELY CLOSE
STEAM. You can this by clicking on Steam > Exit in the client, or by right
clicking the Steam icon in your system tray and clicking Exit. 

Once Steam is closed, all you need to do is click on File > Save in Depressurizer. This will save your profile and, if you haven't changed any settings, will also automatically update your Steam config files with your changes. When you re-open Steam, your games should be organized.

By default, Depressurizer will automatically load and update your profile the next time you launch the program.

#### Manual Categorization
	
There are several ways to manually modify your games within Depressurizer.
		
**1) The Game Panel**

Changes made to the checkboxes at the bottom of the screen will automatically apply to ALL selected games. The checkboxes will update to reflect the current categories of the selected games.

Entering text and clicking the "Add Category and Assign" button will create the entered category if it does not exist, and then assign all selected games to it.

**2) Drag and Drop**

If you select games in the list, and drag them to a category in the list on the left, you can add them to the selected category. You can drag them to "favorites" to set them as favorites, or to Uncategorized to clear their categories.

If you hold Ctrl when you drag items, you can remove them from the selected category instead of adding them.

**3) Context menu**
	
If you right click on the game list, there are options to add categories, remove categories, or alter the Favorite setting for the selected games.

#### Autocategorization

Autocategorization in Depressurizer is based around different schemes that determine what categories to add to (or remove from) each game. These schemes are configurable, and are referred to as "AutoCats".

You can autocategorize some of your games by selecting them and clicking the Autocategorize button below the game list. This will apply the AutoCat selected in the list below the button.

You can autocategorize all games by using the "Autocat All" item in the Tools menu.

To modify, delete or create new AutoCats, click the "Edit AutoCats..." item in the Profile menu.

There are currently two types of AutoCat:

 - **Genre**: This type autocategorizes games based on the genres it is assigned in the Steam store. It has several configuration options.

  - *Prefix* (optional): This is just a text prefix added to the beginning of all genre names assigned by this scheme.

  - *Max Categories*: This is the maximum number of categories that will be assigned to each game. If a particular game has more categories, they will be ignored. 0 indicates no maximum.

  - *Remove Others*: This will remove other categories ONLY if they could potentially be added by this scheme. If a game already belongs to a category that has the name of a genre, with the Prefix, it will be removed before new categories are added to it.

  - *Ignored Genres*: If you don't want a particular genre to be added as a category, select it here.

 - **Flags**: This refers to the items on the right-hand side of the Steam Store pages that indicate certain features. For example, all games with the "Local Co-op" feature listed will be placed into a "Local Co-op" category.

  - *Prefix (optional)*: This is just a text prefix added to the beginning of all genre names assigned by this scheme.

  - *Included Flags*: Only the items selected in this list will be used.

#### Definitions of Terms and Procedures

When you **Download** your game list, the program is going to the Steam Community site to get a list of your games. This does not alter any categories, it only adds games to your list.

When you **Import** from Steam, the program is loading category and other information from your Steam config file. This is not guaranteed to have entries for all your games, but it includes data for any game that is currently categorized, favorited, or hidden. If ALL of your games fall into one of these categories, this can be a way to get a full list if you can't download. Note that if Steam is running, the imported data may not be up to date. This step is also where your non-Steam games are loaded, if you have them enabled.

When you **Save**, you are saving your Depressurizer profile data. By default, this ALSO exports to Steam, but this can be disabled in your profile settings.

When you **Export**, you are manually pushing your data to Steam.

-----
### FAQ / Troubleshooting

##### WILL THIS MESS UP MY STEAM / GET ME VAC BANNED?

No. The only things that Depressurizer does is to write to your configuration files, and it doesn't do anything that you couldn't do using the client itself.

##### WHY DOES MY PROFILE NEED TO BE PUBLIC?

Because at this time, your profile page is the only reasonably accurate list of your games that the program has access to.
	
##### WHY ARE SOME OF MY GAMES MISSING, EVEN AFTER I DOWNLOAD MY GAME LIST?

Probably because they aren't on your community profile. There are many cases where certain games don't show up for some reason. To get them to show up in the program, you can either add them manually by clicking the "Add Game" button, or just add them to any category from within Steam and then do an Import.

##### WHY DO I HAVE EXTRA THINGS LIKE DLC IN MY GAME LIST?

The program relies on its database to filter out DLC, but sometimes the database is wrong, and sometimes it gets out of date. To remove a specific item, just select it and click "Delete Game". This will (by default) remove it from your list and ignore it going forward. If it's a matter of the item being newer than the latest version of the database, you could also update the database yourself.

##### WHY DO SOME OF MY GAMES NOT AUTOCATEGORIZE AT ALL?

The program relies on the Steam Store data for autocategorization info. Sometimes, a game that is on your account might not HAVE a Store page anymore, so the database won't have any data on it.

##### HOW DO I UPDATE THE DATABASE MYSELF?

Click on Tools > Database Editor. Click Fetch List in the upper right. Then, click Update New in the lower right. This might take some time. If you click Stop (NOT CANCEL) it will save what you've gotten so far and you can come back later. Click File > Save to save your changes.
