# Copy2Clipbrd
Copy2Clipbrd is an easy to use and open source tool that makes it easy to copy a file path using the default window's right click context menu.

![image](https://github.com/user-attachments/assets/f2d5ad27-1af4-4615-aef7-d8d8873a83f9)
![example2](https://github.com/user-attachments/assets/0982d47b-df3a-49a2-90ed-e5dc8388ad65)

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

A window should pop up, you can now search for the Winforms reference, check it and click OK
![image](https://github.com/user-attachments/assets/503ec0d7-0fae-4c44-9a3c-24fe05454800)

In case you dont see the System.Windows.Forms Reference, please try searching in COM tab as well
![image](https://github.com/user-attachments/assets/ffb73935-a3c7-470c-b0ac-6b9c00076fc2)

Cant find the reference at all?
