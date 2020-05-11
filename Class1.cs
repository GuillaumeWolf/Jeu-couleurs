using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;


namespace Jeu_couleurs
{
    class Jeu
    {
        public void Run()
        {
            // Dictionnaire Total des couleurs
            Dictionary<int, string> AllColors = ListColor();

            // Nombre de couleur max
            int DictLength = AllColors.Count;

            // Nombre de case
            int LenTarget = ChooseLength();

            // Nombre de Couleurs
            int ColorsLength = ChooseColors(LenTarget, DictLength);

            // Modifier le dictionnaire
            AllColors = ModifieDict(AllColors, ColorsLength, DictLength);

            // Les couleurs à trouver [1]
            int[] ArrayTargett = CreatTargetColors(LenTarget, DictLength, ColorsLength);

            //Les couleurs à trouver [2]
            string[] ColorsTargetFinal = ArrayColors(ArrayTargett, AllColors);

            // Le jeu
            PlayGame(ColorsTargetFinal, AllColors);
        }


        private Dictionary<int, string> ListColor()
        {
            Dictionary<int, string> colors = new Dictionary<int, string>();
            colors.Add(0, "Purple");
            colors.Add(1, "Blue");
            colors.Add(2, "Red");
            colors.Add(3, "Yellow");
            colors.Add(4, "Green");
            colors.Add(5, "Orange");
            colors.Add(6, "Black");
            colors.Add(7, "White");
            colors.Add(8, "Pink");
            return colors;
        }


        private int ChooseLength()
        {
            Console.WriteLine("How many case do you want ?");
            int Length = 0;
            try
            {
                Length = Convert.ToInt32(Console.ReadLine());
            }
            catch(SystemException e)
            {
                Console.WriteLine("Please enter a positiv number.");
                Length = new Jeu().ChooseLength();
            }
            return Length;
        }

        private int ChooseColors(int _Length, int _DictLength)
        {
            Console.WriteLine("How many different colors do you want ? (Max {0})", _DictLength);
            int ColorsChoose;
            try
            {
                ColorsChoose = Convert.ToInt32(Console.ReadLine());
            }
            catch (SystemException e)
            {
                Console.WriteLine("Please enter a positiv number. (Max {0})", _DictLength);
                ColorsChoose = new Jeu().ChooseColors(_Length, _DictLength);
                return ColorsChoose;
            }
            while (ColorsChoose > _DictLength)
            {
                Console.WriteLine("This is too much ! ");
                ColorsChoose = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("You choose {0} case and {1} colors. Now, let's play !", _Length, ColorsChoose);
            return ColorsChoose;
        }

        private Dictionary<int, string> ModifieDict(Dictionary<int, string> _AllColors, int _ColorsLength, int _DictLength)
        {
            for (int i = 0; i < _DictLength - _ColorsLength; i++)
            {
                _AllColors.Remove(_ColorsLength + i);
            }
            /*
            Console.Write("Here the possible colors : ");
            foreach (int j in _AllColors.Keys)
                Console.Write(_AllColors[j] + ", ");
            Console.WriteLine();
            */
            return _AllColors;
        }


        private int[] CreatTargetColors(int LenTarget, int _DictLength, int _ColorsLength)
        {
            int[] ArrayTarget = new int[LenTarget];
            Random r = new Random();
            for (int i = 0; i < LenTarget; i++)
            {
                int num = r.Next(_ColorsLength);
                ArrayTarget[i] = num;
                //Console.Write(num + " ");
            }
            return ArrayTarget;
        }


        private string[] ArrayColors(int[] _ArrayTargett, Dictionary<int, string> _AllColors)
        {
            string[] _ArrayColorsTarget = new string[_ArrayTargett.Length];
            for (int i = 0; i < _ArrayTargett.Length; i++)
            {
                _ArrayColorsTarget[i] = _AllColors[_ArrayTargett[i]];
                //Console.Write(_ArrayColorsTarget[i] + "  ");
            }
            Console.WriteLine();

            return _ArrayColorsTarget;
        }



        private void PlayGame(string[] _ColorsTargetFinal, Dictionary<int, string> _AllColors)
        {
            while (true)
            {
                int LengthArray = _ColorsTargetFinal.Length;
                int LengthDict = _AllColors.Count;
                Console.WriteLine("Enter {0} colors between theese colors : ", LengthArray);
                for (int i = 0; i < LengthDict; i++)
                {
                    Console.Write(" " + _AllColors[i] + " ");
                }
                Console.WriteLine();
                bool See = false;
                string[] ColorsInput = new string[LengthArray];
                for (int i = 0; i < LengthArray; i++)
                {
                    string colorInput = Console.ReadLine();
                    ColorsInput[i] = colorInput;
                    if (colorInput == "exit")
                    {
                        Console.WriteLine("You exit the game. Goodbye.");
                        return;
                    }
                    if (colorInput == "Quick See")
                    {
                        for (int j = 0; j < _ColorsTargetFinal.Length; j++)
                        {
                            Console.Write(_ColorsTargetFinal[j] + "  ");
                        }
                        See = true;
                        continue;
                    }
                }
                if (See == true)
                {
                    continue;
                }
                bool Won = IsArrayRight(ColorsInput, _ColorsTargetFinal);

                if (Won == true)
                {
                    break;
                }     
            }
            Console.WriteLine("Do you want to play again ?\nyes or no");
            string inputNewGame = Console.ReadLine();
            while(true)
            {
                if (inputNewGame == "yes")
                {
                    Console.WriteLine("Ok, nice! Go for a new game !");
                    new Jeu().Run();
                }
                else if (inputNewGame == "no")
                {
                    Console.WriteLine("You correctly escape the game.");
                    break;
                }
                else
                {
                    Console.WriteLine("This is not a valide commande.");
                }
            }
        }

        private bool IsArrayRight(string[] _ColorsInput, string[] __ColorsTargetFinal)
        {
            //__ColorsTargetFinal[0] = "Blue";
            //__ColorsTargetFinal[0] = "Blue";
            //__ColorsTargetFinal[0] = "Purple";
            //__ColorsTargetFinal[0] = "Blue";
            int LengthArray = __ColorsTargetFinal.Length;
            int RightPlace = 0;
            int RightColor = 0;
            for (int i = 0; i < LengthArray; i++)
            {
                int alreadyCount = 0;
                for (int j = 0; j < LengthArray; j++)
                    if (_ColorsInput[i] == __ColorsTargetFinal[j])
                    {
                        if (j == i)
                        {
                            RightPlace ++;
                            RightColor -= alreadyCount;
                            break;
                        }
                        RightColor ++;
                        alreadyCount ++;
                        
                    }
            }
            if (RightPlace == LengthArray)
            {
                Console.WriteLine("You win !");
                return true;
            }
            if (RightPlace == 0 && RightColor == 0)
            {
                Console.WriteLine("There is {0} at the good place and {1} good color.", RightPlace, RightColor);
            }
            else if (RightPlace >= 0 && RightColor >= 0)
            {
                Console.WriteLine("There is {0} at the good place and {1} good color, but at the wrong place.", RightPlace, RightColor);
            }           
            return false;
        }

    }
}


