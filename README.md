# Welcome to Projet-STIMULUS!
## Basic workflow to create a issue
1. In the project window, click the __+__ icon at the bottom of the __Todo__ or __backlog__ column depending on where you want to create the issue. Choose a clear title that can easily be recognise.
```
Example:
Import devis from pdf.
```
> Try to put the issue in the right sprint, you can always change it after creating it.
2. Do a description of the issue. A description should be formated like this:
```
Format:
*The current behavior of the application*
*Why it is problematic/why we want to improve it*
*What should we do to fix it*
Example:
Currently, the application can only import devis with a form.
This cause a issue where it takes too long to import devis into the application.
The solution for this issue is to add a import from pdf option.
```
3. A good practice is to assign the issue to someone and to do an estimate of how much days it will take when creating it.
4. Once ready, click the button __convert to issue__. This will open the issue an allow creating branch to fix it.

 

## Basic workflow to start fixing a issue.
1. Assign the issue to yourself.
2. Move the issue from __Todo to In progress__.
3. Enter an estimate of how much days you think it will take you to fix it.
4. Once you finish adding comments to the issue and setting everything up, open the issue in the issue tab.
> This can be done in the project tab by clicking the __Open in new tab__ button.
5. Click the create a branch link on the right panel. This branch will be used to do changes link to the issue.
> Make sure the branch source is main. Also make sure to let the name of the branch to the default name, wich should be the issue number followed by the issue title.
6. On your local repository folder, open a terminal.
7. Checkout the new branch link to your issue. You may now start to fix your issue.
```
git fetch origin
git checkout *branch-name*
```
> Make sure you are not working on main before you start changing things. Use the command git status to make sure you are on the right branch.
> if you made a mistake and started working in main, refer yourself to the __restore mistake__ section.
8. The best way to work locally is to do a commit at each step of fixing your issue.
```
git add -p                    # Used to add by line. Press y or n on your keyboard to add the line to the staged change or not.
git restore .                 # Used to restore all unstaged change. Caution this command will discard all unstaged changes
git commit -m "*commit-name*" # Create a local commit with your changes.
```

 

## Basic workflow to finish fixing a issue.
1. Once you are ready to get your code reviewed, you need to push the code to the online branch. This will create a pull request. By default, your pull request will need a reviewer to validate it.
```
git push
```
2. Assign a reviewer to your pull request. Put someone you trust. The reviewer is as much responsible that the pull request does not break main.
3. If the reviewer ask for change, discuss with him and modify the code until both party are satisfied with the code being merge to main.
4. If the code as conflict with master, it is important to solve them before merging. This can be done by cherry picking your changes and rebasing them onto main.
5. Once the reviewer accept the pull request. The autor can now click the merge button to merge the branch to main.
6. Once you merged in main, close the branch in the github repository and move the issue to done and close it.
> Its possible that the issue close itself when you merge your pull request to master, but verify to make sure it is.

 

## Restore mistake.
### Started to fix a issue in the wrong branch!
1. If you created a commit for your changes, you will need to reset soft to get back all your changes as unstaged change.
   To do this, you will first need to gather the commit number of the original commit before all your changes.
   It is important to take the commit number of the commit before you started to change things. To find the number do this command:
```
git log --one-line --decorate
```
2. Reset to the older commit like mentionned before.
```
git reset --soft *commit number*
```
3. Stash your change.
```
git stash
```
4. Create the new branch based on your ticket with the right parents.
> There should never be 2 branch with the same name, so you should try to delete the original branch before creating the second branch.
> At least put a relevant name to the new branch
5. Move to the ticket branch in local.
```
git checkout *branch-name*
```
6. Pop your changes in the new branch.
