# Tafs.Activities.Extensions

This library recreates some of the types found in Microsoft.Activities.Extensions. It is intended to be used as a replacement library for those running .NET 6 and newer.

This is NOT a 1:1 recreation of the original library, nor is it a port of Microsoft's code to .NET 6. This library is intended to provide a replacement for those using the Microsoft.Activities.Extensions library, but is entirely new development.

If you find you are missing a type that existed in the original library, or would like to propose a new type, please feel free to make a proposal in a new issue.

## Conditions

Conditions are a set of activities which return a `bool`. This typically takes the form of a `CodeAcivity<bool>`. These types are intended for use inside of a Retry scope or another activity which expect a boolean return.

While an activity may exist, often times there are better ways to handle these operations. For instance, `IsTrue` simply returns whether the provided boolean value is true. In most circumstances, one would typically use the initial boolean value where appropraite. This activity only allows for mapping into an activity, like the Condition field of a Retry scope.

## Extensions

This contains helpful tools for programmers developing custom activities. Users developing in UiPath will not find this helpful.

## Statements

Statements are a set of helpful activities which are difficult to do in UiPath. Examples include adding items to a dictionary, clearing a dictionary, or executing functions that have no return type.
