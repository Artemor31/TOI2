
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

        public static string[] Superlative = new string[] { "ейше", "ейш" };
        public static string[] Derivational = new string[] { "ость", "ост" };

        public static string[] Adjectival;

        public static string RV;
        public static string R1;
        public static string R2;
        public static string Word;

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

            // STEP 1
            bool end2 = false;
            bool end1 = false;

            if (!DeletingLoops(5, end1, end2, PerfectiveGerund1, PerfectiveGerund2))
            {
                if (!DeletingLoops(2, end1, Reflexive))
                {
                    if (!AjectivesComparition(ref RV))
                    {
                        if (!DeletingLoops(3, end1, Adjective))
                        {
                            if (!DeletingLoops(4, end1, end2, Verb1, Verb2))
                            {
                                if (!DeletingLoops(4, end1, Noun)) { }
                            }
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

            if (R2.EndsWith(Derivational[0]))
                RV = RV.Remove(RV.Length - Derivational[0].Length);
            else if (R2.EndsWith(Derivational[1]))
                RV = RV.Remove(RV.Length - Derivational[1].Length);

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

            Console.WriteLine("финал " + RV);
            Console.ReadKey();
        }


        static bool AjectivesComparition(ref string RV)
        {
            for (int i = 0; i < Participle1.Length; i++)
            {
                for (int j = 0; j < Adjective.Length; j++)
                {
                    string CreatedSyffix = Participle1[i] + Adjective[j];

                    if (RV.Length > CreatedSyffix.Length)
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
                    if (RV.Length >= CreatedSyffix.Length)
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
            while (Loops > 0 || end1)
            {
                end1 = Step1(ref RV, Loops, true, ArrayOne);
                Loops--;
            }
            return end1;
        }
        static bool DeletingLoops(int Loops, bool end1, bool end2, string[] ArrayOne, string[] ArrayTwo)
        {
            while (Loops > 0 || end1 || end2)
            {
                end1 = Step1(ref RV, Loops, true, ArrayOne);
                end2 = Step1(ref RV, Loops, false, ArrayTwo);
                Loops--;
            }
            return (end1 || end2);
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
