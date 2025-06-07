# Intentional Error Documentation - ProductionData (Prop Efficiency)

## Overview

An intentional error was found in the backend data model related to the `Efficiency` property of the `ProductionData` entity.

## Problem Description

In the backend model, the `Efficiency` property is incorrectly defined as a `string`, as shown below:

## How It Affects the Application

UI display issues: The value might be treated as plain text rather than a number.

Sorting and filtering: Any sorting logic will treat the value as a string, leading to incorrect order.

Validation problems: It's harder to ensure the value is between 0 and 100 if it's a string.

Data inconsistency: Input like `eighty` or `null` can be incorrectly accepted and saved.

```csharp
public string Efficiency { get; set; }  // Incorrect

public decimal Efficiency { get; set; }  // Correct
