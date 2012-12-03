umf-hook
========

How often have you created a commit and even pushed it without realising that you forgot to save all changes to source or project-files in Visual Studio?

umf-hook is a small C# application and Git pre-commit hook that ensure that you don't create commits when you currently have unsaved modified files in Visual Studio.

Installation
------------
Build and copy umf-hook.exe and pre-commit to your .git/hooks folder and optionally assign a solution file-name by editing the commit-msg file. If no solution is assigned umf-hook will look in repo-root and repo-root/src for a file ending in .sln

Or fire off the following command in the root of your repository

```bash
curl -L http://tinyurl.com/umf-hook | bash
```

Caveats
-------
* Large solutions might be slow since querying for unsaved modified files in the solution is done by recursing through the solution tree-structure.
* If the solution is busy (starting up, building, etc) an COMException is thrown from the Visual Studio interop and the user is notified.
* At the moment it doesn't work with solution-items since calling the Saved property on those results in a COMException.

Acknowledgment
--------------
Thanks to adabyron from Stack Overflow for the initial Visual Studio extensibility solution: http://stackoverflow.com/a/13034842/359137