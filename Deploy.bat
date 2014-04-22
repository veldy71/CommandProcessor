@echo off
mkdir %3 /a
xcopy %1%2 %3 /y
if exist %1%2.config xcopy %1%2.config %3 /y
if exist %1.pdb xcopy %1.pdb %3 /y
if exist %1.XML xcopy %1.XML %3 /y