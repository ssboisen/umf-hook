umf-hook
========

How often have you created a commit and even pushed it without realising that you forgot to save all changes to source or project-files in Visual Studio?

umf-hook is a small C# application and Git pre-commit hook that ensure that you don't create commits when you currently have unsaved modified files in Visual Studio.

Installation
------------
Build and copy umf-hook.exe and commit-msg to your .git/hooks folder and optionally assign a solution file-name by editing the commit-msg file. If no solution is assigned umf-hook will look in repo-root and repo-root/src for a file ending in .sln


Acknowledgment
--------------
Thanks to adabyron from Stack Overflow for the initial Visual Studio extensibility solution: http://stackoverflow.com/questions/4724381/get-the-reference-of-the-dte2-object-in-visual-c-sharp-2010