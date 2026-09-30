using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Gordiichuk3v4
{
    public class BookCollection
    {
        private string _name;
        private readonly List<string> _books = new List<string>();

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва колекції не може бути порожньою.");
                }
                _name = value;
            }
        }

        public int Count => _books.Count;

        public BookCollection(string name)
        {
            Name = name;
        }

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= _books.Count)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі колекції.");
                }
                return _books[index];
            }
            set
            {
                if (index < 0 || index >= _books.Count)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі колекції.");
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва книги не може бути порожньою.");
                }
                _books[index] = value;
            }
        }

        public static BookCollection operator +(BookCollection collection, string bookTitle)
        {
            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                throw new ArgumentException("Назва книги не може бути порожньою.");
            }

            collection._books.Add(bookTitle);
            return collection;
        }

        public static bool operator ==(BookCollection c1, BookCollection c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null)) return false;

            return c1._books.SequenceEqual(c2._books);
        }

        public static bool operator !=(BookCollection c1, BookCollection c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            if (obj is BookCollection other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _books.Count);
        }

        public void PrintAll()
        {
            Console.WriteLine($"--- Колекція: {Name} (Кількість: {Count}) ---");
            for (int i = 0; i < _books.Count; i++)
            {
                Console.WriteLine($"[{i}] {_books[i]}");
            }
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота №3 (Варіант 4) ===");
            Console.WriteLine("Студент: Гордійчук\n");

            BookCollection myLibrary = new BookCollection("Програмування та ООП");

            myLibrary += "C# 10 та .NET 6";
            myLibrary += "Об'єктно-орієнтоване програмування в C#";
            myLibrary += "Паттерни проектування";

            myLibrary.PrintAll();

            Console.WriteLine($"Перегляд через індексатор [0]: {myLibrary[0]}");
            myLibrary[0] = "C# 12 та .NET 8 (Оновлено)";
            Console.WriteLine($"Після зміни через індексатор [0]: {myLibrary[0]}\n");

            BookCollection secondLibrary = new BookCollection("Копія");
            secondLibrary += "C# 12 та .NET 8 (Оновлено)";
            secondLibrary += "Об'єктно-орієнтоване програмування в C#";
            secondLibrary += "Паттерни проектування";

            Console.WriteLine($"Чи однакові колекції за вмістом? {myLibrary == secondLibrary}");
        }
    }
}
