# DigaoRunner

## Run scripts locally and remotely

In my work environment, I frequently need to create scripts to automate processes. Whether it's to copy files from one place to another for deploying an application or even for more complex tasks involving multiple steps, such as installing services, configuring Windows settings, reading files and extracting information, or any other activity that can be automated.

These are typically tasks where it's not worth writing a specific program. In such cases, we usually resort to a **Batch script (.BAT or .CMD)** or the more modern **PowerShell**.

But let's be honest, for any of these scripts, the language is not very practical, and the script ends up being hard to read and understand, and it's not efficient. For example, error handling in these scripts is cumbersome, and typically, the processes continue even if an error occurs in the middle of the script.

With all this in mind, I developed Digao Runner, which is essentially a debugger for scripts with the **.drs (Digao Runner Script)** extension, where you can write the script using **C# code** in a very practical and easy way. The code is very clean, has **exception handling**, and you can also configure **input fields** for the user to fill in at runtime, like parameters or variables of the script.
