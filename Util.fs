namespace TalkToYourFunctions

open System

module Util =
    let getAge (birthdate: DateTime) =
        let today = DateTime.Today
        let age = today.Year - birthdate.Year
        if (birthdate > today.AddYears(age)) then age - 1 else age

    let ordinal (num: int) =
        let ones = num % 10
        let tens = floor ((num |> float) / 10.0) % 10.0
        if (tens = 1.0) then
            "th"
        else
            match ones with
            | 1 -> "st"
            | 2 -> "nd"
            | 3 -> "rd"
            | _ -> "th"

    let getLeapAge (birthdate: DateTime) =
        let today = DateTime.Today

        birthdate.Year
        |> Seq.unfold (fun year ->
            if (year > today.Year) then None
            else Some(DateTime.IsLeapYear year, year+1))
        |> Seq.filter id
        |> Seq.length

    let isLeapYearBaby (birthdate: DateTime) =
        DateTime.IsLeapYear birthdate.Year &&
        birthdate.Month = 2 &&
        birthdate.Day = 29

    let isBirthday (birthdate: DateTime) =
        let today = DateTime.Today
        birthdate.Month = today.Month && birthdate.Day = today.Day

    let birthdateForYear (year: int) (birthdate : DateTime) =
        if (birthdate.Month = 2 && birthdate.Day = 29 && not (DateTime.IsLeapYear year)) then
            DateTime(year, 3, 1) // Leap year birthday
        else
            DateTime(year, birthdate.Month, birthdate.Day)


    let isBelated (birthdate: DateTime) =
        let tolerance = 150.0
        let today = DateTime.Today
        let thisYear = today.Year
        let lastYear = thisYear - 1

        let thisYearsBirthdate = birthdateForYear thisYear birthdate
        let lastYearsBirthdate = birthdateForYear lastYear birthdate

        today.AddDays(-tolerance) < thisYearsBirthdate && thisYearsBirthdate < today ||
        today.AddDays(-tolerance) < lastYearsBirthdate && lastYearsBirthdate < today

    let getGreeting givenName birthdate =
        let age = getAge birthdate
        let ageStr =
            if (age > 0) then
                sprintf "%d%s " age (ordinal age)
            else ""

        let msg =
            if (isBirthday birthdate) then
                sprintf "Happy %sbirthday %s!!" ageStr givenName
            elif (isBelated birthdate) then
                sprintf "Looks like I missed it %s. But happy belated %sbirthday anyway!" givenName ageStr
            else
                sprintf "Happy %sbirthday in advance, %s. Hope it's a good one!" ageStr givenName

        if (isLeapYearBaby birthdate) then
            let leapAge = getLeapAge birthdate
            let leapAgeStr =
                if (leapAge > 0) then
                    sprintf "You turn what, %d this year? Just kidding. Anyways... " leapAge
                else ""
            sprintf "Wow, a leap year baby! %s%s" leapAgeStr msg
        else msg