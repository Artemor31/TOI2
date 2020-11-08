
using System;

namespace TOI2
{
    static class Program
    {
        public static char[] Letters = new char[] { 'а', 'е', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };

        public static string[] PerfectiveGerund1 = new string[] { "в", "вши", "вшись" };
        public static string[] PerfectiveGerund2 = new string[] { "ив", "ивши", "ившись", "ыв", "ывши", "ывшись", };

        public static string[] Adjective = new string[]
        {
            "ее", "ие", "ые", "ое", "ими", "ыми",
            "ей", "ий", "ый", "ой", "ем", "им",
            "ым", "ом", "его", "ого", "ему", "ому","их",
            "ых", "ую", "юю", "ая", "яя", "ою", "ею"
        };

        public static string[] Participle1 = new string[] { "ем", "нн", "вш", "ющ", "щ" };
        public static string[] Participle2 = new string[] { "ивш", "ывш", "ующ" };

        public static string[] Reflexive = new string[] { "ся", "сь" };

        public static string[] Verb1 = new string[]
        {
            "ла", "на", "ете", "йте", "ли", "й", "л", "ем",
            "н", "ло", "но", "ет", "ют", "ны", "ть", "ешь", "нно"
        };

        public static string[] Verb2 = new string[]
        {
            "ила", "ыла", "ена", "ейте", "уйте", "ите",
            "или", "ыли", "ей", "уй", "ил", "ыл", "им",
            "ым", "ен", "ило", "ыло", "ено", "ят", "ует", "уют",
            "ит", "ыт", "ены", "ить", "ыть", "ишь", "ую", "ю"
        };

        public static string[] Noun = new string[]
        {
            "а", "ев", "ов", "ие", "ье", "е", "иями",
            "ями", "ами", "еи", "ии", "и", "ией", "ей",
            "ой", "ий", "й", "иям", "ям", "ием", "ем", "ам",
            "ом", "о", "у", "ах", "иях", "ях", "ы", "ь", "ию",
            "ью", "ю", "ия", "ья", "я"
        };

        public static string[] Superlative = new string[] { "ейш", "ейше" };
        public static string[] Derivational = new string[] { "ост", "ость" };

        public static string[] Adjectival;

        public static string RV;
        public static string R1;
        public static string R2;
        public static string Word;
        static byte LenghtOfEnding = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Введите слово, вместо <ё> используйте <е>: ");
            Word = Console.ReadLine();
            if (!CheckForLetters(Word))
            {
                Console.WriteLine("В слове нет гласных");
                return;
            }
            RV = RVFind(Word);
            Console.WriteLine(RV);

            R1 = R1Find(Word);
            Console.WriteLine(R1);

            R2 = R1Find(R1);
            Console.WriteLine(R2);

            // STEP 1




            Console.ReadKey();
        }

        static void Step1(string rv)
        {
            // вопрос впорос вопрос вопрос вопрос вопрос ввопрос вопрос вопрос ворпос вопро вопрос вопрос вопрос вопрос вопрос вопрос вопрос
            bool ThereIsLettersA = rv[rv.Length - 4] == 'а' || rv[rv.Length - 4] == 'и';

            if (rv.Length == 3)
                ThereIsLettersA = true;

            if ( (rv.Length > 3 && ThereIsLettersA) || ( rv.Length == 3) )
            {
                string s1 = rv.Substring(rv.Length - 3);
                for (int i = 0; i < PerfectiveGerund1.Length; i++)
                {
                    if (PerfectiveGerund1[i].Length == 3 && ThereIsLettersA)
                    {

                    }
                }
            }
        }
        static bool CheckForLetters(string _word)
        {
            for (int i = 0; i < _word.Length; i++)
            {
                for (int j = 0; j < Letters.Length; j++)
                {
                    if (_word[i] == Letters[j])
                        return true;
                }
            }
            return false;
        }
        static string RVFind(string _word)
        {
            for (int i = 0; i < _word.Length; i++)
            {
                for (int j = 0; j < Letters.Length; j++)
                {
                    if (_word[i] == Letters[j] && (_word.Length > i + 1))
                    {
                        return _word.Substring(i + 1);
                    }
                }
            }
            return "ERROR";
        }
        static string R1Find(string _word)
        {
            for (int i = 0; i < _word.Length; i++)
            {
                for (int j = 0; j < Letters.Length; j++)
                {
                    if (_word[i] == Letters[j] && (_word.Length > i + 2) && NotInLetters(_word[i+1]) )
                    {
                        return _word.Substring(i + 2);
                    }
                }
            }
            return "ERROR";
        }
        static bool NotInLetters(char c)
        {
            for (int i = 0; i < Letters.Length; i++)
                if (c == Letters[i]) return false;
            return true;
        }
    }
}
