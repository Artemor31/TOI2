
using System;
using System.IO;

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

        public static string[] Superlative = new string[] { "ейше", "ейш" };
        public static string[] Derivational = new string[] { "ость", "ост" };

        public static string[] Adjectival;

        public static string RV;
        public static string R1;
        public static string R2;
        public static string Word;
        public static string Prefix;

        static void Main(string[] args)
        {



            Console.WriteLine("Введите слово, вместо <ё> используйте <е>: ");
            Word = Console.ReadLine();
            if (!CheckForLetters(Word))
            {
                Console.WriteLine("В слове нет гласных");
                return;
            }
            RV = RVFind(Word, ref Prefix);
            Console.WriteLine(RV);

            // STEP 1
            bool end2 = false;
            bool end1 = false;

            if (!DeletingLoops(5, end1, end2, PerfectiveGerund1, PerfectiveGerund2))
            {
                DeletingLoops(2, end1, Reflexive);

                bool ajRet = false;
                int ajLoops = 6;

                while (ajLoops > 0 && !ajRet)
                {
                    ajRet = AjectivesComparition(ref RV,  ajLoops);
                    ajLoops--;
                }

                if (!DeletingLoops(3, end1, Adjective) && !ajRet)
                {
                    if (!DeletingLoops(3, end1, end2, Participle1, Participle2))
                    {
                        if (!DeletingLoops(4, end1, end2, Verb1, Verb2))
                        {
                            if (!DeletingLoops(4, end1, Noun)) { }
                        }
                    }
                }                
            }

            // STEP 2
            if (RV.EndsWith('и'))
            {
                RV = RV.Remove(RV.Length - 1);
            }

            // STEP 3
            R1 = R1Find(Word);
            Console.WriteLine(R1);

            R2 = R1Find(R1);
            Console.WriteLine(R2);
            if (R2.Contains(Derivational[0]) && RV.Contains(Derivational[0]))
            {
                char[] derivotional = Derivational[0].ToCharArray();
                int index = RV.IndexOfAny(derivotional);
                RV = RV.Remove(RV.Length - Derivational[0].Length);
            }
            else if (R2.Contains(Derivational[0]) && RV.Contains(Derivational[0]))
            {
                char[] derivotional = Derivational[0].ToCharArray();
                int index = RV.IndexOfAny(derivotional);
                RV = RV.Remove(RV.Length - Derivational[0].Length);
            }

            // STEP 4
            if (RV.EndsWith("нн"))
                RV = RV.Remove(RV.Length - 1);

            if (R2.EndsWith(Superlative[0]))
            {
                RV = RV.Remove(RV.Length - Superlative[0].Length);
            }
            else if (R2.EndsWith(Superlative[1]))
            {
                RV = RV.Remove(RV.Length - Superlative[1].Length);
            }

            if (RV.EndsWith("нн"))
                RV = RV.Remove(RV.Length - 1);

            if (RV.EndsWith('ь'))
                RV = RV.Remove(RV.Length - 1);

            RV = $"{Prefix}{RV}";


            Console.WriteLine("финал " + RV);

            // FileWork
            var file = File.Create("C:\\Users\\artem\\Desktop\\OutputTOI2.txt");
            file.Dispose();

            using (StreamReader sr = new StreamReader("C:\\Users\\artem\\Desktop\\Dictionary.txt"))
            {
                while (!sr.EndOfStream)
                {
                    char ch = (char)sr.Read();
                    string NextWord = ch.ToString();
                    while (ch != ' ' && ch != ',' && ch != '.' && !sr.EndOfStream) 
                    {
                        ch = (char)sr.Read();
                        NextWord += ch;
                    }
                    if (NextWord.StartsWith(RV))
                    {
                        AppendOutputFile(NextWord);
                    }
                }
            }
            Console.ReadKey();
        }

        static void AppendOutputFile(string _word)
        {
            using (StreamWriter sw = new StreamWriter("C:\\Users\\artem\\Desktop\\OutputTOI2.txt"))
            {
                sw.WriteLine(_word);
                sw.Dispose();
            }
        }
        static bool AjectivesComparition(ref string RV, int LengthOfCreatedSuffix)
        {
            for (int i = 0; i < Participle1.Length; i++)
            {
                for (int j = 0; j < Adjective.Length; j++)
                {
                    string CreatedSyffix = Participle1[i] + Adjective[j];

                    if (RV.Length > CreatedSyffix.Length && CreatedSyffix.Length == LengthOfCreatedSuffix)
                    {
                        string OurSuffix = RV.Substring(RV.Length - CreatedSyffix.Length);
                        string LetterBeforeSyf = RV.Substring(RV.Length - CreatedSyffix.Length - 1, 1);

                        if (OurSuffix == CreatedSyffix && (LetterBeforeSyf == "а" || LetterBeforeSyf == "я"))
                        {
                            RV = RV.Remove(RV.Length - CreatedSyffix.Length);
                            return true;
                        }
                    }
                }
            }
            for (int i = 0; i < Participle2.Length; i++)
            {
                for (int j = 0; j < Adjective.Length; j++)
                {
                    string CreatedSyffix = Participle2[i] + Adjective[j];
                    if (RV.Length >= CreatedSyffix.Length && CreatedSyffix.Length == LengthOfCreatedSuffix)
                    {
                        string OurSuffix = RV.Substring(RV.Length - CreatedSyffix.Length);

                        if (OurSuffix == CreatedSyffix)
                        {
                            RV = RV.Remove(RV.Length - CreatedSyffix.Length);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool DeletingLoops(int Loops, bool end1, string[] ArrayOne)
        {
            for (int i = Loops; i > 0; i--)
            {
                end1 = Step1(ref RV, Loops, false, ArrayOne);
                Loops--;

                if (end1)
                    return end1;
            }
            return end1;
        }
        static bool DeletingLoops(int Loops, bool end1, bool end2, string[] ArrayOne, string[] ArrayTwo)
        {
            for (int i = Loops; i > 0; i--)
            {
                end1 = Step1(ref RV, Loops, true, ArrayOne);
                end2 = Step1(ref RV, Loops, false, ArrayTwo);
                Loops--;

                if (end1 || end2)
                    return end1 || end2;
            }
            return end1 || end2;
        }


        static bool Step1(ref string rv, int SuffixLenght, bool InFirstGroup, string[] Suffixes)
        {
            bool ThereIsLettersA = false;
            if (InFirstGroup)
                ThereIsLettersA = CHeckForLettersAorYa(rv, SuffixLenght);
            else if (rv.Length >= SuffixLenght)
                ThereIsLettersA = true;


            if (rv.Length >= SuffixLenght && ThereIsLettersA)
            {
                string rvSyffix = rv.Substring(rv.Length - SuffixLenght);

                for (int i = 0; i < Suffixes.Length; i++)
                {
                    if (Suffixes[i].Length == SuffixLenght && Suffixes[i] == rvSyffix)
                    {
                        rv = rv.Remove(rv.Length - SuffixLenght);
                        return true;
                    }
                }
            }
            return false;
        }

        static bool CHeckForLettersAorYa(string rv, int SuffixLenght)
        {
            bool ThereIsLettersA = false;

            if (rv.Length < SuffixLenght)
                return false;
            else if (rv.Length > SuffixLenght)
                ThereIsLettersA = rv[rv.Length - SuffixLenght - 1] == 'а' || rv[(rv.Length - SuffixLenght) - 1] == 'я';

            return ThereIsLettersA;
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
        static string RVFind(string _word, ref string prefix)
        {
            for (int i = 0; i < _word.Length; i++)
            {
                for (int j = 0; j < Letters.Length; j++)
                {
                    if (_word[i] == Letters[j] && (_word.Length > i + 1))
                    {
                        prefix = _word.Substring(0, i + 1);
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
                    if (_word[i] == Letters[j] && (_word.Length > i + 2) && NotInLetters(_word[i + 1]))
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