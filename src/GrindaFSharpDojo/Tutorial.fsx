﻿// This sample will guide you through elements of the F# language.  
//
// *******************************************************************************************************
//   To execute the code in F# Interactive, highlight a section of code and press Alt-Enter or right-click 
//   and select "Execute in Interactive".  You can open the F# Interactive Window from the "View" menu. 
// *******************************************************************************************************
//
// For more about F#, see:
//     http://fsharp.net
//
// For additional templates to use with F#, see the 'Online Templates' in Visual Studio, 
//     'New Project' --> 'Online Templates'
//
// For specific F# topics, see:
//     http://go.microsoft.com/fwlink/?LinkID=234174 (F# Development Portal)
//     http://go.microsoft.com/fwlink/?LinkID=124614 (Code Gallery)
//     http://go.microsoft.com/fwlink/?LinkId=235173 (Math/Stats Programming)
//     http://go.microsoft.com/fwlink/?LinkId=235176 (Charting)

// Contents:
//    - Integers and basic functions
//    - Booleans
//    - Strings
//    - Tuples
//    - Lists and list processing
//    - Arrays
//    - Sequences
//    - Recursive functions
//    - Record types
//    - Union types
//    - Option types            
//    - Pattern matching        

// ---------------------------------------------------------------
//         Integers and basic functions
// ---------------------------------------------------------------

module Integers = 
    let sampleInteger = 176

    /// Do some arithmetic starting with the first integer
    let sampleInteger2 = (sampleInteger/4 + 5 - 7) * 4

    /// A list of the numbers from 0 to 99
    let sampleNumbers = [ 0 .. 99 ]

    /// A list of all tuples containing all the numbers from 0 to 99 and their squares
    let sampleTableOfSquares = [ for i in 0 .. 99 -> (i, i*i) ]

    // The next line prints a list that includes tuples, using %A for generic printing
    printfn "The table of squares from 0 to 99 is:\n%A" sampleTableOfSquares


module BasicFunctions = 

    // Use 'let' to define a function that accepts an integer argument and returns an integer. 
    let func1 x = x*x + 3             

    // Parenthesis are optional for function arguments
    let func1a (x) = x*x + 3             

    /// Apply the function, naming the function return result using 'let'. 
    /// The variable type is inferred from the function return type.
    let result1 = func1 4573
    printfn "The result of squaring the integer 4573 and adding 3 is %d" result1

    // When needed, annotate the type of a parameter name using '(argument:type)'
    let func2 (x:int) = 2*x*x - x/5 + 3

    let result2 = func2 (7 + 4)
    printfn "The result of applying the 1st sample function to (7 + 4) is %d" result2

    let func3 x = 
        if x < 100.0 then 
            2.0*x*x - x/5.0 + 3.0
        else 
            2.0*x*x + x/5.0 - 37.0

    let result3 = func3 (6.5 + 4.5)
    printfn "The result of applying the 2nd sample function to (6.5 + 4.5) is %f" result3



// ---------------------------------------------------------------
//         Booleans
// ---------------------------------------------------------------

module SomeBooleanValues = 

    let boolean1 = true
    let boolean2 = false

    let boolean3 = not boolean1 && (boolean2 || false)

    printfn "The expression 'not boolean1 && (boolean2 || false)' is %A" boolean3



// ---------------------------------------------------------------
//         Strings
// ---------------------------------------------------------------

module StringManipulation = 

    let string1 = "Hello"
    let string2  = "world"

    /// Use @ to create a verbatim string literal
    let string3 = @"c:\Program Files\"

    /// Using a triple-quote string literal
    let string4 = """He said "hello world" after you did"""

    let helloWorld = string1 + " " + string2 // concatenate the two strings with a space in between
    printfn "%s" helloWorld

    /// A string formed by taking the first 7 characters of one of the result strings
    let substring = helloWorld.[0..6]
    printfn "%s" substring



// ---------------------------------------------------------------
//         Tuples (ordered sets of values)
// ---------------------------------------------------------------

module Tuples = 

    /// A simple tuple of integers
    let tuple1 = (1, 2, 3)

    /// A function that swaps the order of two values in a tuple. 
    /// QuickInfo shows that the function is inferred to have a generic type.
    let swapElems (a, b) = (b, a)

    printfn "The result of swapping (1, 2) is %A" (swapElems (1,2))

    /// A tuple consisting of an integer, a string, and a double-precision floating point number
    let tuple2 = (1, "fred", 3.1415)

    printfn "tuple1: %A    tuple2: %A" tuple1 tuple2
    


// ---------------------------------------------------------------
//         Lists and list processing
// ---------------------------------------------------------------

module Lists = 

    let list1 = [ ]            /// an empty list

    let list2 = [ 1; 2; 3 ]    /// list of 3 elements

    let list3 = 42 :: list2    /// a new list with '42' added to the beginning

    let numberList = [ 1 .. 1000 ]  /// list of integers from 1 to 1000

    /// A list containing all the days of the year
    let daysList = 
        [ for month in 1 .. 12 do
              for day in 1 .. System.DateTime.DaysInMonth(2012, month) do 
                  yield System.DateTime(2012, month, day) ]

    /// A list containing the tuples which are the coordinates of the black squares on a chess board.
    let blackSquares = 
        [ for i in 0 .. 7 do
              for j in 0 .. 7 do 
                  if (i+j) % 2 = 1 then 
                      yield (i, j) ]

    /// Square the numbers in numberList, using the pipeline operator to pass an argument to List.map    
    let squares = 
        numberList 
        |> List.map (fun x -> x*x) 

    /// Computes the sum of the squares of the numbers divisible by 3.
    let sumOfSquaresUpTo n = 
        numberList
        |> List.filter (fun x -> x % 3 = 0)
        |> List.sumBy (fun x -> x * x)


// ---------------------------------------------------------------
//         Arrays
// ---------------------------------------------------------------

module Arrays = 

    /// The empty array
    let array1 = [| |]

    let array2 = [| "hello"; "world"; "and"; "hello"; "world"; "again" |]

    let array3 = [| 1 .. 1000 |]

    /// An array containing only the words "hello" and "world"
    let array4 = [| for word in array2 do
                        if word.Contains("l") then 
                            yield word |]

    /// An array initialized by index and containing the even numbers from 0 to 2000
    let evenNumbers = Array.init 1001 (fun n -> n * 2) 

    /// sub-array extracted using slicing notation
    let evenNumbersSlice = evenNumbers.[0..500]

    for word in array4 do 
        printfn "word: %s" word

    // modify an array element using the left arrow assignment operator
    array2.[1] <- "WORLD!"

    /// Calculates the sum of the lengths of the words that start with 'h'
    let sumOfLengthsOfWords = 
        array2
        |> Array.filter (fun x -> x.StartsWith "h")
        |> Array.sumBy (fun x -> x.Length)



// ---------------------------------------------------------------
//         Sequences
// ---------------------------------------------------------------

module Sequences = 
    // Sequences are evaluated on-demand and are re-evaluated each time they are iterated. 
    // An F# sequence is an instance of a System.Collections.Generic.IEnumerable<'T>,
    // so Seq functions can be applied to Lists and Arrays as well.

    /// The empty sequence
    let seq1 = Seq.empty

    let seq2 = seq { yield "hello"; yield "world"; yield "and"; yield "hello"; yield "world"; yield "again" }

    let numbersSeq = seq { 1 .. 1000 }

    /// another array containing only the words "hello" and "world"
    let seq3 = 
        seq { for word in seq2 do
                  if word.Contains("l") then 
                      yield word }

    let evenNumbers = Seq.init 1001 (fun n -> n * 2) 

    let rnd = System.Random()

    /// An infinite sequence which is a random walk
    //  Use yield! to return each element of a subsequence, similar to IEnumerable.SelectMany.
    let rec randomWalk x =
        seq { yield x
              yield! randomWalk (x + rnd.NextDouble() - 0.5) }

    let first100ValuesOfRandomWalk = 
        randomWalk 5.0 
        |> Seq.truncate 100
        |> Seq.toList



// ---------------------------------------------------------------
//         Recursive functions
// ---------------------------------------------------------------

module RecursiveFunctions  = 
              
    /// Compute the factorial of an integer. Use 'let rec' to define a recursive function
    let rec factorial n = 
        if n = 0 then 1 else n * factorial (n-1)

    /// Computes the greatest common factor of two integers. 
    //  Since all of the recursive calls are tail calls, the compiler will turn the function into a loop,
    //  which improves performance and reduces memory consumption.
    let rec greatestCommonFactor a b =                       
        if a = 0 then b
        elif a < b then greatestCommonFactor a (b - a)           
        else greatestCommonFactor (a - b) b

    /// Computes the sum of a list of integers using recursion.
    let rec sumList xs =
        match xs with
        | []    -> 0
        | y::ys -> y + sumList ys

    /// Make the function tail recursive, using a helper function with a result accumulator
    let rec private sumListTailRecHelper accumulator xs =
        match xs with
        | []    -> accumulator
        | y::ys -> sumListTailRecHelper (accumulator+y) ys

    let sumListTailRecursive xs = sumListTailRecHelper 0 xs



// ---------------------------------------------------------------
//         Record types
// ---------------------------------------------------------------

module RecordTypes = 

    // define a record type
    type ContactCard = 
        { Name     : string;
          Phone    : string;
          Verified : bool }
              
    let contact1 = { Name = "Alf" ; Phone = "(206) 555-0157" ; Verified = false }

    // create a new record that is a copy of contact1, 
    // but has different values for the 'Phone' and 'Verified' fields
    let contact2 = { contact1 with Phone = "(206) 555-0112"; Verified = true }

    /// Converts a 'ContactCard' object to a string
    let showCard c = 
        c.Name + " Phone: " + c.Phone + (if not c.Verified then " (unverified)" else "")
        


// ---------------------------------------------------------------
//         Union types
// ---------------------------------------------------------------

module UnionTypes = 

    /// Represents the suit of a playing card
    type Suit = 
        | Hearts 
        | Clubs 
        | Diamonds 
        | Spades

    /// Represents the rank of a playing card
    type Rank = 
        /// Represents the rank of cards 2 .. 10
        | Value of int
        | Ace
        | King
        | Queen
        | Jack
        static member GetAllRanks() = 
            [ yield Ace
              for i in 2 .. 10 do yield Value i
              yield Jack
              yield Queen
              yield King ]
                                   
    type Card =  { Suit: Suit; Rank: Rank }
              
    /// Returns a list representing all the cards in the deck
    let fullDeck = 
        [ for suit in [ Hearts; Diamonds; Clubs; Spades] do
              for rank in Rank.GetAllRanks() do 
                  yield { Suit=suit; Rank=rank } ]

    /// Converts a 'Card' object to a string
    let showCard c = 
        let rankString = 
            match c.Rank with 
            | Ace -> "Ace"
            | King -> "King"
            | Queen -> "Queen"
            | Jack -> "Jack"
            | Value n -> string n
        let suitString = 
            match c.Suit with 
            | Clubs -> "clubs"
            | Diamonds -> "diamonds"
            | Spades -> "spades"
            | Hearts -> "hearts"
        rankString  + " of " + suitString

    let printAllCards() = 
        for card in fullDeck do 
            printfn "%s" (showCard card)



// ---------------------------------------------------------------
//         Option types
// ---------------------------------------------------------------

module OptionTypes = 
    /// Option values are any kind of value tagged with either 'Some' or 'None'.
    /// They are used extensively in F# code to represent the cases where many other
    /// languages would use null references.

    type Customer = { zipCode : decimal option }

    /// Abstract class that computes the shipping zone for the customer's zip code, 
    /// given implementations for the 'getState' and 'getShippingZone' abstract methods.
    [<AbstractClass>]
    type ShippingCalculator =
        abstract getState : decimal -> string option
        abstract getShippingZone : string -> int

        /// Return the shipping zone corresponding to the customer's ZIP code
        /// Customer may not yet have a ZIP code or the ZIP code may be invalid
        member this.customerShippingZone(customer : Customer) =
            customer.zipCode |> Option.bind this.getState |> Option.map this.getShippingZone



// ---------------------------------------------------------------
//         Pattern matching
// ---------------------------------------------------------------

module PatternMatching = 

    /// A record for a person's first and last name
    type Person = {     
        First : string
        Last  : string
    }

    /// define a discriminated union of 3 different kinds of employees
    type Employee = 
        | Engineer  of Person
        | Manager   of Person * list<Employee>            // manager has list of reports
        | Executive of Person * list<Employee> * Employee // executive also has an assistant

    /// count everyone underneath the employee in the management hierarchy, including the employee
    let rec countReports(emp : Employee) = 
        1 + match emp with
            | Engineer(id) -> 
                0
            | Manager(id, reports) -> 
                reports |> List.sumBy countReports 
            | Executive(id, reports, assistant) ->
                (reports |> List.sumBy countReports) + countReports assistant


    /// find all managers/executives named "Dave" who do not have any reports
    let rec findDaveWithOpenPosition(emps : Employee list) =
        emps 
        |> List.filter(function 
                       | Manager({First = "Dave"}, []) -> true       // [] matches the empty list
                       | Executive({First = "Dave"}, [], _) -> true
                       | _ -> false)                                 // '_' is a wildcard pattern that matches anything
                                                                     // this handles the "or else" case
