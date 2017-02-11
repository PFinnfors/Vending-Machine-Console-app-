using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Machine
    {

        static void Main(string[] args)
        {

            //Main vending machine loop
            bool machineON = true;
            while (machineON)
            {
                //Change and money denominations
                int[] changeTypes = new int[] { 0, 1, 5, 10, 20, 50, 100, 500, 1000 }; //SEK
                int[] change = new int[] { 100, 0 }; //0 = user, 1 = machine

                //Creates the items and their interaction messages
                Items DrinkItems = new Drinks("Soda", 20, "It's a can of Pepsi.", "You gulped down the pepsi!", 0);
                Items FoodItems = new Food("Sandwhich", 35, "It's a chicken sandwhich.",
                    "You put ketchup on your chicken sandwhich, you monster!", 0);
                Items SnackItems = new Snacks("Chips", 15, "A bag of mostly air, with some chips.",
                    "You chewed some chips, but mostly air.", 0);

                //Displays available items
                Console.WriteLine(DrinkItems.ItemName + ":\t\t" + DrinkItems.ItemPrice + " SEK");
                Console.WriteLine(FoodItems.ItemName + ":\t" + FoodItems.ItemPrice + " SEK");
                Console.WriteLine(SnackItems.ItemName + ":\t\t" + SnackItems.ItemPrice + " SEK");

                //Lets user input as much change as they want until they're done
                PutInChange(changeTypes, change);

                //Entire process of selecting items to buy / examine
                #region Selection choice

                bool selectLoop = true;
                while (selectLoop)
                {
                    //Displays available items with menu
                    DisplayItems(DrinkItems, FoodItems, SnackItems);

                    //Displays change in machine and available to user
                    ChangeStatus(change);

                    Console.WriteLine("\nSelect an item:\n");

                    //Gets key for selection
                    char selectItem = 'X';
                    selectItem = GetMenuChoice(selectItem);

                    //Used for error message
                    bool enoughChange = false;

                    //Uses polymorphism in each selection to access functions in Items class
                    switch (selectItem)
                    {
                        //Examines Soda
                        case '1':
                            {
                                if (DrinkItems is Items)
                                {
                                    var d = (DrinkItems as Items);
                                    d.Examine();
                                    enoughChange = true;
                                }
                                break;
                            }
                        //Buying Soda
                        case '2':
                            {
                                //Can only buy if price is less or equal to change in the machine
                                if (DrinkItems.ItemPrice <= change[1] && DrinkItems is Items)
                                {
                                    var d = (DrinkItems as Items);
                                    d.Buy();
                                    DrinkItems.ItemCount++;
                                    change[1] -= DrinkItems.ItemPrice;
                                    enoughChange = true;
                                }
                                break;
                            }
                        //Examines Sandwhich
                        case '3':
                            {
                                if (FoodItems is Items)
                                {
                                    var f = (FoodItems as Items);
                                    f.Examine();
                                    enoughChange = true;
                                }
                                break;
                            }
                        //Buying Sandwhich
                        case '4':
                            {
                                if (FoodItems.ItemPrice <= change[1] && FoodItems is Items)
                                {
                                    var f = (FoodItems as Items);
                                    f.Buy();
                                    FoodItems.ItemCount++;
                                    change[1] -= FoodItems.ItemPrice;
                                    enoughChange = true;
                                }
                                break;
                            }
                        //Examines Chips
                        case '5':
                            {
                                if (SnackItems is Items)
                                {
                                    var s = (SnackItems as Items);
                                    s.Examine();
                                    enoughChange = true;
                                }
                                break;
                            }
                        //Buying Chips
                        case '6':
                            {
                                if (SnackItems.ItemPrice <= change[1] && SnackItems is Items)
                                {
                                    var s = (SnackItems as Items);
                                    s.Buy();
                                    SnackItems.ItemCount++;
                                    change[1] -= SnackItems.ItemPrice;
                                    enoughChange = true;
                                }
                                break;
                            }
                        default: //Error message for wrong keypresses still allowed by the method (shouldn't need to do this, but it will do)
                            {
                                WriteColor("Red", "That's not something you can buy!");
                                enoughChange = true;
                                break;
                            }
                    }

                    //Shows this message if enoughChange check hasn't passed
                    if (!enoughChange)
                    {
                        WriteColor("Red", "\n\nYou can't afford it!\n\n");
                    }

                    //Displays updated change
                    ChangeStatus(change);

                    Console.WriteLine("\n\nDo you want to do something else? [Y/N]\n");

                    //Breaks out of buy mode if user doesn't press Y when prompted
                    //Not proud of how input-sensitive this part of the code is, but it will do
                    char selectChar = Console.ReadKey(true).KeyChar;
                    selectChar = Char.ToUpper(selectChar);
                    selectLoop = (selectChar == 'Y') ? true : false;

                }

                #endregion Selection choice

                //Gives back unused change from machine
                ChangeBack(changeTypes, change);

                WriteColor("Yellow", "\n\nYou're done with the vending machine.");

                //Section for using what was bought. Loops as long as there are items left in inventory
                bool itemsLeft = true;
                while (itemsLeft)
                {
                    if (DrinkItems.ItemCount > 0 || FoodItems.ItemCount > 0 || SnackItems.ItemCount > 0)
                    {
                        Console.Write(" Do you want to use something you bought?\n");
                        Console.WriteLine("0) Exit");

                        if (DrinkItems.ItemCount > 0)
                        {
                            Console.WriteLine($"1) {DrinkItems.ItemName}");
                        }
                        if (FoodItems.ItemCount > 0)
                        {
                            Console.WriteLine($"2) {FoodItems.ItemName}");
                        }
                        if (SnackItems.ItemCount > 0)
                        {
                            Console.WriteLine($"3) {SnackItems.ItemName}");
                        }

                        Console.WriteLine("");

                        char useSelect = 'X';
                        useSelect = GetMenuChoice(useSelect);

                        //Calls on the use functions triggered by user selections, decrements count for loop
                        switch (useSelect)
                        {
                            case '0':
                                {
                                    itemsLeft = false;
                                    break;
                                }
                            case '1':
                                {
                                    if (DrinkItems.ItemCount > 0 && DrinkItems is Items)
                                    {
                                        var di = (DrinkItems as Items);
                                        di.Use();
                                        DrinkItems.ItemCount--;
                                    }
                                    break;
                                }
                            case '2':
                                {
                                    if (FoodItems.ItemCount > 0 && FoodItems is Items)
                                    {
                                        var fi = (FoodItems as Items);
                                        fi.Use();
                                        FoodItems.ItemCount--;
                                    }
                                    break;
                                }
                            case '3':
                                {
                                    if (SnackItems.ItemCount > 0 && SnackItems is Items)
                                    {
                                        var si = (SnackItems as Items);
                                        si.Use();
                                        SnackItems.ItemCount--;
                                    }
                                    break;
                                }

                        }

                    }
                    else //If we have no items, we don't have to worry about using them
                    {
                        itemsLeft = false;
                    }
                }

                Console.ReadKey();

                //Program ends after main loop
                machineON = false;
            }
        }

        //Allows user to put in as much change as they want and have
        static void PutInChange(int[] changeTypes, int[] change)
        {
            bool changeLoop = true;
            while (changeLoop)
            {
                ChangeStatus(change);

                Console.WriteLine("\nInsert your change:");
                WriteColor("Green", "[0]: Exit"
                    + "  [1]: 1"
                    + "  [2]: 5"
                    + "  [3]: 10"
                    + "  [4]: 20"
                    + "\n[5]: 50"
                    + "  [6]: 100"
                    + "  [7]: 500"
                    + "  [8]: 1000\n");

                //User puts in change
                char choice = 'X';
                choice = GetMenuChoice(choice);

                //Goes through the calculations for each denomination transferred on selection
                bool changeCheck = false;
                switch (choice)
                {
                    #region item options
                    case '0':
                        {
                            Console.Write("0 SEK\n");

                            Console.Write("\nYou didn't put anything in.\n\n");

                            Thread.Sleep(1000);


                            changeCheck = true;
                            changeLoop = false;
                            break;
                        }
                    case '1':
                        {
                            Console.Write("1 SEK\n");

                            if (change[0] >= changeTypes[1])
                            {
                                change[0] = change[0] - changeTypes[1];
                                change[1] = change[1] + changeTypes[1];
                                changeCheck = true;
                            }


                            Thread.Sleep(1000);

                            break;
                        }
                    case '2':
                        {
                            Console.Write("5 SEK\n");

                            if (change[0] >= changeTypes[2])
                            {
                                change[0] = change[0] - changeTypes[2];
                                change[1] = change[1] + changeTypes[2];
                                changeCheck = true;
                            }


                            Thread.Sleep(1000);

                            break;
                        }
                    case '3':
                        {
                            Console.Write("10 SEK\n");

                            if (change[0] >= changeTypes[3])
                            {
                                change[0] = change[0] - changeTypes[3];
                                change[1] = change[1] + changeTypes[3];
                                changeCheck = true;
                            }

                            Thread.Sleep(1000);

                            break;
                        }
                    case '4':
                        {
                            Console.Write("20 SEK\n");

                            if (change[0] >= changeTypes[4])
                            {
                                change[0] = change[0] - changeTypes[4];
                                change[1] = change[1] + changeTypes[4];
                                changeCheck = true;
                            }

                            Thread.Sleep(1000);

                            break;
                        }
                    case '5':
                        {
                            Console.Write("50 SEK\n");

                            if (change[0] >= changeTypes[5])
                            {
                                change[0] = change[0] - changeTypes[5];
                                change[1] = change[1] + changeTypes[5];
                                changeCheck = true;
                            }

                            Thread.Sleep(1000);

                            break;
                        }
                    case '6':
                        {
                            Console.Write("100 SEK\n");

                            if (change[0] >= changeTypes[6])
                            {
                                change[0] = change[0] - changeTypes[6];
                                change[1] = change[1] + changeTypes[6];
                                changeCheck = true;
                            }

                            Thread.Sleep(1000);

                            break;
                        }
                    case '7':
                        {
                            Console.Write("500 SEK\n");

                            if (change[0] >= changeTypes[7])
                            {
                                change[0] = change[0] - changeTypes[7];
                                change[1] = change[1] + changeTypes[7];
                                changeCheck = true;
                            }

                            Thread.Sleep(1000);

                            break;
                        }
                    case '8':
                        {
                            Console.Write("1000 SEK\n");

                            if (change[0] >= changeTypes[8])
                            {
                                change[0] = change[0] - changeTypes[8];
                                change[1] = change[1] + changeTypes[8];
                                changeCheck = true;
                            }

                            Thread.Sleep(1000);

                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
                if (!changeCheck)
                {
                    WriteColor("Red", "\nYou don't have enough change!");
                }
                #endregion item options
            }
        }

        //Gives back change when transaction is finished
        static void ChangeBack(int[] changeTypes, int[] change)
        {
            /* Runs an algorithm counting down until it has gone through all denominations, 
            reminder: change[0] = user, change[1] = in machine */
            for (int d = 8; d > 0; d--)
            {
                //Displays message only if user gets the change back
                bool dTrue = (change[1] >= changeTypes[d]) ? true : false;
                if (dTrue)
                {
                    Console.Write("\nThe machine gave you a ");
                    WriteColor("Green", $"{changeTypes[d]}");
                    Console.Write(" back.\n");
                }

                //When money in the machine is more than or equal to the denomination d(8-1)
                if (change[1] >= changeTypes[d])
                {
                    //Takes remainder to get what's left after the current denomination is subtracted
                    if (d > 1)
                    { change[1] = change[1] % changeTypes[d]; }
                    else
                    { change[1]--; }

                    //Gives the current denomination back to user
                    change[0] += changeTypes[d];
                }
                //if we're on last denomination and it didn't finish, start over
                d = (d == 1 && change[1] > 0) ? 8 : d;
            }
        }

        //Displays money in the machine and on the person
        static void ChangeStatus(int[] change)
        {
            Console.WriteLine("\nYour change: " + change[0] + "  |  Change in the machine: " + change[1]);
        }

        //Item menu with options
        static void DisplayItems(Items DrinkItems, Items FoodItems, Items SnackItems)
        {
            Console.Write("\n" + DrinkItems.ItemName + " : " + DrinkItems.ItemPrice + " SEK\n");
            WriteColor("Yellow", "\t1) Examine\n");
            WriteColor("Green", "\t2) Buy\n");

            Console.Write("\n" + FoodItems.ItemName + " : " + FoodItems.ItemPrice + " SEK\n");
            WriteColor("Yellow", "\t3) Examine\n");
            WriteColor("Green", "\t4) Buy\n");

            Console.Write("\n" + SnackItems.ItemName + " : " + SnackItems.ItemPrice + " SEK\n");
            WriteColor("Yellow", "\t5) Examine\n");
            WriteColor("Green", "\t6) Buy\n");
        }

        //Gets general menu input for numbers, I use letters in this program to tell this method that it's needed
        public static char GetMenuChoice(char inputValue)
        {
            //Loops while a valid number option hasn't been put in, such as 'X'
            while (!(inputValue >= '1' && inputValue <= '6'))
            {
                //Gets the menu choice variable
                inputValue = Console.ReadKey(true).KeyChar;

                return inputValue;
            }
            return inputValue;
        }

        //Created to clutter screen less with formatting
        public static void WriteColor(string color, string text)
        {
            Type type = typeof(ConsoleColor);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, color);
            Console.Write(text);
            Console.ResetColor();
        }


    }
}
