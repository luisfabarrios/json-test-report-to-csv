# Project Overview

## Project Name: Json test results to csv report

## Description:

This project takes test execution results from an origen folder and creates a csv report based on the related data.

In every execution the files in the InputFiles folder are compared with the ones in the OutputFiles folder creating then a report for each input file that has no match with the output list.

## Technologies:

C#
.NET Core

## Getting Started

Prerequisites:
Ensure you have .NET Core installed on your system.

Cloning the Repository:

 > git clone [Repository URL]

Building the Project:

 > dotnet build

Running the Project
Command line to be executed in the folder where the .csproj file is located

 > dotnet run

## Usage

1. Place the .json files with the test results in the InputFiles folder.
2. Run the program.

## JSON Structure for test results file

This JSON represents a list of test results

 > JSON
 > [
 >     {
 >         "testCase": "TestName",
 >         "status": "Passed",
 >         "executionTime": 16,
 >         "startTime": "08-21-2024 14:30:34",
 >         "endTime": "08-21-2024 14:30:50"
 >     }
 > ]

testCase: A string with the name of the test case
status: A string with the status of the test, has two values Passed/Failed
executionTime: An int with the execution time of the test in seconds
startTime: A string with the start time/hour of the test case
endTime: A string with the end time/hour of the test case