Project Overview
Project Name: Json test results to csv report

Description:
This project takes test execution results from an origen folder and creates a csv report based on the related data.
In every execution the files in the InputFiles folder are compared with the ones in the OutputFiles folder creating then a report for each input file that has no match with the output list.

Technologies:
C#
.NET Core

Getting Started
Prerequisites:
Ensure you have .NET Core installed on your system.

Cloning the Repository:
Bash
git clone [Repository URL]

Building the Project:
Command Line:
Bash
dotnet build

Running the Project
Command Line:
Bash
dotnet run

Usage
1. Place the .json files with the test results in the InputFiles folder.
2. Run the program.

JSON Structure for test results file
This JSON represents a list of test results

