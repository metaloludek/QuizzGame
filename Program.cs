using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

class Program
{
    static void Main()
    {
        Console.WriteLine("Podaj pelna sciezke do pliku questions.xlsx");
        string filePath = Console.ReadLine();
       
        var workbook = new XLWorkbook(filePath);
        var worksheet = workbook.Worksheet("Arkusz1");
        var questions = new List<Question>();

        // Wczytanie pytań z pliku XLSX 

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            var question = new Question
            {
                Text = row.Cell("A").Value.ToString(),
                AnswerA = row.Cell("B").Value.ToString(),
                AnswerB = row.Cell("C").Value.ToString(),
                CorrectAnswer = row.Cell("D").Value.ToString(),
            };
            questions.Add(question);
        }

        // mieszanie pytan 
        var random = new Random();
        questions = questions.OrderBy(q => random.Next()).ToList();

        // Start 
        Console.Clear();
        Console.WriteLine("Witam w Quiz! Lewa Strzalka - odpowiedz A, prawa strzalka - odpowiedz B.");
        Console.WriteLine("Gra konczy się pierwszym bledem lub wyczerpaniem puli pytan. Powodzenia!\n");
        Console.ReadKey();

        int correctAnswers = 0;
        foreach (var question in questions)
        {
            Console.Clear();
            Console.WriteLine($"Aktualny wynik: {correctAnswers}\n");
            Console.WriteLine(question.Text);
            Console.WriteLine($"A: {question.AnswerA}");
            Console.WriteLine($"B: {question.AnswerB}");
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
            } 
            while (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow);
            if (key.Key == ConsoleKey.LeftArrow && question.CorrectAnswer == "A" ||
                key.Key == ConsoleKey.RightArrow && question.CorrectAnswer == "B")
            {
                Console.WriteLine("Wlasciwa odpowiedz!\n");
                correctAnswers++;
            }
            else
            {
                Console.WriteLine("Zla odpowiedz! Sprobuj ponownie (wcisnij dowolny klawisz)");
                break;
            }
        }
        Console.WriteLine($"Udzieliles {correctAnswers} poprawnych odpowiedzi. Dowolny klawisz rozpoczyna gre od nowa");
        Console.ReadKey(true);
        Console.Clear();
        Main();
    }
}
class Question

{
    public string Text { get; set; }
    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string CorrectAnswer { get; set; }
}