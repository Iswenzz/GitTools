# GitTools

[![Checks](https://img.shields.io/github/checks-status/Iswenzz/GitTools/master?logo=github)](https://github.com/Iswenzz/GitTools/actions)
[![CodeFactor](https://img.shields.io/codefactor/grade/github/Iswenzz/GitTools?label=codefactor&logo=codefactor)](https://www.codefactor.io/repository/github/iswenzz/GitTools)
[![CodeCov](https://img.shields.io/codecov/c/github/Iswenzz/GitTools?label=codecov&logo=codecov)](https://codecov.io/gh/Iswenzz/GitTools)
[![License](https://img.shields.io/github/license/Iswenzz/GitTools?color=blue&logo=gitbook&logoColor=white)](https://github.com/Iswenzz/GitTools/blob/master/LICENSE)

GitTools is a CLI utility software that has features such as copying commits from one repository to another and making false commits. 
It can execute commands using a set of filters, such as start and end dates and committer email.

## Instructions

```sh
$ ./GitTools.exe --help
GitTools 1.0.0
Iswenzz (c) 2021

  copycommits    Copy commits from one repository to another.
  commit         Create a commit at a specific date.
  help           Display more information on a specific command.
  version        Display version information.
```
```sh
$ ./GitTools.exe copycommits --help
USAGE:
Copy the commits on a specific date:
  gittools copycommits -u Iswenzz -e alexisnardiello@gmail.com -i "C:\Repository\A" -o "C:\Repository\B" --since-date 25/06/1999 --until-date 04/07/2021
  
  -u, --user                 Required. The user.
  -e, --email                Required. The email.
  -f, --filter               Filter the commits by email.
  -i, --input-repository     Required. The input repository path.
  -o, --output-repository    Required. The output repository path.
  --since-date               Get commits since a specific date.
  --until-date               Get commits until a specific date.
  --empty                    Copy as empty commit.
  --help                     Display this help screen.
  --version                  Display version information.
```

## Building (Any Platform)
_Pre-Requisites:_
1. [Visual Studio](https://visualstudio.microsoft.com/) or [Dotnet SDK](https://dotnet.microsoft.com/download)

### [Download](https://github.com/Iswenzz/GitTools/releases)

## Contributors

***Note:*** If you would like to contribute to this repository, feel free to send a pull request, and I will review your code. 
Also feel free to post about any problems that may arise in the issues section of the repository.
