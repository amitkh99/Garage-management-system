using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class InputGetter
    {
        internal enum eYesNo
        {
            Yes,No
        }

        // This static method get Type and string - print to console the string which is an explantion of what parameter the user should enter.
        // And then according to the Type the method get input from the user and return an object of it.
        internal static object GetInputByTypeOnFly(Type i_Type, string i_InputExplain)
        {
            object inputObj = null;
            string printString = "\nPlease enter " + i_InputExplain;
            Console.WriteLine(printString);
            while (inputObj == null)
            {
                try
                {
                    if (i_Type == typeof(int))
                    {
                        inputObj = int.Parse(Console.ReadLine());
                    }
                    else if (i_Type == typeof(float))
                    {
                        inputObj = float.Parse(Console.ReadLine());
                    }
                    else if (i_Type == typeof(bool))
                    {
                        Console.WriteLine("(true/false)");
                        inputObj = bool.Parse(Console.ReadLine());
                    }
                    else if (i_Type == typeof(string))
                    {
                        string str = Console.ReadLine();
                        if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                        {
                            throw new FormatException();
                        }
                        else
                        {
                            inputObj = str;
                        }
                    }
                    else if (i_Type == typeof(eColor))
                    {
                        inputObj = GetEnumChooise<eColor>();
                    }
                    else if (i_Type == typeof(eNumberOfDoors))
                    {
                        inputObj = GetEnumChooise<eNumberOfDoors>();
                    }
                    else if (i_Type == typeof(eLicenseType))
                    {
                        inputObj = GetEnumChooise<eLicenseType>();
                    }
                    else if (i_Type == typeof(eVehicleStatus))
                    {
                        inputObj = GetEnumChooise<eVehicleStatus>();
                    }
                    else if (i_Type == typeof(eYesNo))
                    {
                        inputObj = GetEnumChooise<eYesNo>();
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }

            return inputObj;
        }

        internal static int GetOperation()
        {
            int returnOperation;
            int.TryParse(Console.ReadLine(),out returnOperation);
            return returnOperation;
        }

        // This return a instance of the template enum according to chooise of user.
        internal static T GetEnumChooise<T>()
        {
            string[] enumOptions = Enum.GetNames(typeof(T));
            PrintEnumOptions(enumOptions);
            T[] enumValues = (T[])Enum.GetValues(typeof(T));
            bool validChooise = false;
            T res = enumValues[0];

            while (!validChooise)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    if (input >= 1 && input <= enumOptions.Length)
                    {
                        res = enumValues[input - 1];
                        validChooise = true;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException("Input out of range", enumOptions.Length, 1);
                    }
                }
                catch (FormatException fx)
                {
                    Console.WriteLine("Invalid input");
                }
                catch (ValueOutOfRangeException vore)
                {
                    Console.WriteLine(string.Format(vore.Message));
                }
            }

            return res;
        }

        internal static void PrintEnumOptions(string[] i_enumOptions)
        {
            StringBuilder stringToPrint = new StringBuilder("Please choose desired option:\n");
            int i = 1;

            foreach (string option in i_enumOptions)
            {
                stringToPrint.AppendFormat("{0}. {1}\n", i++, option);
            }

            Console.WriteLine(stringToPrint);
        }
    }
}
