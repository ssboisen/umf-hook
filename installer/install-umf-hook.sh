#!/bin/sh
wget -x -O .git\hooks\umf-hook.exe --no-check-certificate https://github.com/downloads/ssboisen/umf-hook/umf-hook.exe
wget -x -O .git\hooks\pre-commit --no-check-certificate https://raw.github.com/ssboisen/umf-hook/master/githook/pre-commit
echo "umf-hook has been installed"
