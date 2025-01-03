using BooksLab.Functions;

namespace BooksLab.Functions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Validator
{

    public static bool ValidateUserId(string? input, out int userId)
    {
        return int.TryParse(input, out userId) && userId > 0;
    }

    public static bool ValidateBookTitle(string title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }

    public static bool ValidateAuthorName(string author)
    {
        return !string.IsNullOrWhiteSpace(author);
    }

    public static bool ValidateGenres(string genresInput, out List<string> genres)
    {
        genres = genresInput.Split(',')
            .Select(item => item.Trim())
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .ToList();
        return genres.Count > 0;
    }

    public static int ValidatePublicationYear()
    {
        return Inp.Input(0, DateTime.Now.Year);
    }

    public static bool ValidateAnnotation(string annotation)
    {
        return !string.IsNullOrWhiteSpace(annotation);
    }

    public static bool ValidateISBN(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        isbn = isbn.Replace("-", "").Replace(" ", ""); // Убираем дефисы и пробелы

        if (isbn.Length == 10)
        {
            return ValidateISBN10(isbn);
        }
        else if (isbn.Length == 13)
        {
            return ValidateISBN13(isbn);
        }

        return false; // ISBN не соответствует допустимым длинам
    }

    private static bool ValidateISBN10(string isbn)
    {
        if (!Regex.IsMatch(isbn, @"^\d{9}[\dX]$"))
            return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (isbn[i] - '0') * (10 - i);
        }

        char checkDigit = isbn[9];
        sum += (checkDigit == 'X') ? 10 : (checkDigit - '0');

        return sum % 11 == 0;
    }

    private static bool ValidateISBN13(string isbn)
    {
        if (!Regex.IsMatch(isbn, @"^\d{13}$"))
            return false;

        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            int digit = isbn[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        int checkDigit = 10 - (sum % 10);
        if (checkDigit == 10) checkDigit = 0;

        return isbn[12] - '0' == checkDigit;
    }
}