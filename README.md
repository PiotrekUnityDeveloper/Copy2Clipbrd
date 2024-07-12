# Copy2Clipbrd
Copy2Clipbrd is an easy to use and open source tool that makes it easy to copy a file path using the default window's right click context menu.

![image](https://github.com/user-attachments/assets/f2d5ad27-1af4-4615-aef7-d8d8873a83f9)
![example2](https://github.com/user-attachments/assets/0982d47b-df3a-49a2-90ed-e5dc8388ad65)

# Usage
How to use:

YOU CAN DOWNLOAD THE LATEST BUILD FROM THE RELEASES TAB!

1. Launch the RUN.bat file
   - The Copy2Clipbrd window will show up.

2. Choose your context menu preferences
   - Check all the options you want to show up in the right click file menu

3. Click the large switch to activate the service
   - After you do so, you can close the Copy2Clipboard window.
  
   NOTE: DONT MOVE THE MAIN EXECUTABLE, PARENT FOLDER, OR ANY ASSETS FROM THE INITIAL LOCATION THE SERVICE WAS STARTED FROM
   (i mean it's not that dramatic to write in full caps, if you do end up relocating the files,
   please run the service again by launching the main executable copy2clipbrd.exe and pressing refresh)
   EVEN THOUGH IT SAYS ITS ON AFTER YOU MOVED THE FILES, IT MAY NOT WORK CORRECTLY. yeah it is on, but in registry the executable path is different, so yeah just remember to relauch your session


# Building from source code

This software is created from 2 main programs, the one is responsible for modifying the registry and adding the right click menu options,
and the other is ran every time you click one of the menu options. For it to work, you want to build both of them.
Luckily this is very easy to do, see below.

The One Way To Do It

The easiest way to open this project is to open both of the projects by the github 'Open with Visual Studio' option
![image](https://github.com/user-attachments/assets/fc708998-efcf-471f-a0c1-e3243b3147fa)
This option will clone the repo to your machine and open it automatically with visual studio
(assuming you have github desktop and VS installed)

The Other Way to do it

1. Download or Clone the repo
2. Unzip the contents to your disk
3. Open the folder, and locate the .sln file within it
4. Open the project file (.sln) with Visual studio 2022 (older may not work if you dont have the .Net 8.0)

NOTE: You may encounter errors while trying to build the 'copytoclipboard' project (the one used to purely copy the file path, ran with the menu item).
The most common reason for that is a missing reference to the winforms library.

Q: How do i know if this is the exact error i am having?
A: If the only errors you have is a invalid/missing reference exception (namely, the 'Clipboard' piece of code is marked as error), then this is the solution

Those are the step you want to take when an error occurs

STEP 1: Add a new reference to your project:
![image](https://github.com/user-attachments/assets/e4713177-5e6a-407d-97ff-2fb345113115)
OR
![image](https://github.com/user-attachments/assets/4ef69a47-23d8-43f3-bfb1-7323101a4d6b)

IMPORTANT NOTE: If you dont have the Reference option, and instead have the: Project Reference/Shared Project Reference or COM Reference, select the COM one
![image](https://github.com/user-attachments/assets/0e859be5-1755-4f2d-aebc-0ea5a1f54e85)


A window should pop up, you can now search for the Winforms reference, check it and click OK
![image](https://github.com/user-attachments/assets/503ec0d7-0fae-4c44-9a3c-24fe05454800)

In case you dont see the System.Windows.Forms Reference, please try searching in COM tab as well
![image](https://github.com/user-attachments/assets/ffb73935-a3c7-470c-b0ac-6b9c00076fc2)

Cant find the reference at all? You can download an unmodified copy of the .dll at this link:
[https://github.com/PiotrekUnityDeveloper/Copy2Clipbrd/blob/main/System.Windows.Forms.dll](https://github.com/PiotrekUnityDeveloper/Copy2Clipbrd/blob/main/System.Windows.Forms.dll)

Now that you downloaded it, simply go back to that reference window, click on BROWSE and select your newly downloaded .dll file
![image](https://github.com/user-attachments/assets/2f14fd29-f57a-46ae-bf6a-bbeacda05cb3)
![image](https://github.com/user-attachments/assets/aed13852-bfd3-4e46-a96e-90fe412cf2f4)

The project should reload, and you should be good to go! The next time you will build the project, the exact same .dll will be copied to the build path (if its not, then copy it there manually)

# Plans for Features

There's a couple more things i would want to add to this tool, like copying an image to clipboard with just the menu item, or copying an entire .txt files
It would be fun, useful and pretty easy to add, but for now, this tool won't be updated with those features (although, you are welcome to fork us and work it out yourself!)

The plan is to create a scripting software allowing you to write custom code that will be run on any context menu item you may want to add. From replacing a symbol in multiple filenames to another one, to idk, any ideas?
This plan remains a plan for now, but im keen on making it a thing sometime! Hit me up if you want to join in the team.

# Contrubuting

We are open to any kind of contrubutions to our software. From bug fixes to new features, rewrites. Whatever it is, ill be happy my software has helped or been useful to someone, and seeing people interested in my work is epic!

# License

I dont care about attribution! All i want to have is open source software for anyone to learn from, use or expand! That's why this project is licensed under the [DONT ASK](https://piotrekunitydeveloper.github.io/dontask/) license, which is basically a license allowing you to do anything you want with the code! Yay! #makeeverythingopensource
