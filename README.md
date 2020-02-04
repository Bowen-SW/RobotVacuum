# RoboVac
Robot vacuum simulator, including floor plan editor and efficiency tester.


## Before cloning
Install Git Large File Storage (Git LFS, https://git-lfs.github.com/).

You should just need to install it, as the repository has already been initialized with it (do NOT run `git lfs install`). Without LFS, staging and commiting would take much longer.

LFS should work with Git clients as well.

### Clone the Repository
Anywhere you feel comfortable having it.

## After cloning
The repository will have various git related files and a folder named **RoboVac Unity**. This is a Unity project.

To open the project in Unity, start **Unity Hub**. Under **Projects**, click **ADD** and select the Unity project folder inside the repository (named **RoboVac Unity**).

## Within Unity
Because of the way Unity stores its files (scenes, prefabs, etc.), it is difficult to resolve merge conflicts. Many of these files may be binary files, so merge conflicts are inevitable.

Because of this, in the `master` branch I will be creating 6 different scenes, similar to the different branches of the repository. This is so that when working with the project, we don't all change the same scene. One scene will be our `master` (and another our `dev`) scene that the components we work on will be combined into. 

I *highly* encourage you to work within your branch only and coordinate with the rest of the team when you need to work in the master scene. The fewer conflicts we have, the better. 
