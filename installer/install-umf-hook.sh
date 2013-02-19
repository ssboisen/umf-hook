#!/bin/sh
curl -s -L -o .git/hooks/umf-hook.exe https://github.com/downloads/ssboisen/umf-hook/umf-hook.exe
curl -s -L -o .git/hooks/pre-commit https://raw.github.com/ssboisen/umf-hook/master/githook/pre-commit
echo "umf-hook has been installed"
