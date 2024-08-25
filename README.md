# SQLite In-Memory Database Example in C#

This project demonstrates using an SQLite in-memory database in a C# application to manage allocation records.

## Functionality

1. Connects to an SQLite in-memory database.
2. Creates a table named `Allocations`.
3. Inserts sample allocation data.
4. Validates and displays the data based on predefined rules.

## Table Structure

- `Id`: Primary key, auto-incremented.
- `ExConjoint`: Ex-spouse identifier.
- `Enfants`: Children names.
- `Montant`: Allocation amount.
- `Periode`: Allocation period (e.g., "Juillet, Août et Septembre 2024").
- `Deces`: Boolean indicating if the ex-spouse is deceased.

## How to Run

1. Install [.NET SDK](https://dotnet.microsoft.com/download).
2. Install the SQLite package:

   ```bash
   dotnet add package System.Data.SQLite
