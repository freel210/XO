using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Field
    {
        protected int origRow;
        protected int origCol;

        public int CheckResult { get; private set; }

        public int[] values = new int[9];

        private Dictionary<int, RowCellNumbers> cells;

        public Field()
        {
            cells = new Dictionary<int, RowCellNumbers>();
            cells.Add(1, new RowCellNumbers() { First = 0, Second = 1, Third = 2 });
            cells.Add(2, new RowCellNumbers() { First = 3, Second = 4, Third = 5 });
            cells.Add(3, new RowCellNumbers() { First = 6, Second = 7, Third = 8 });
            cells.Add(4, new RowCellNumbers() { First = 0, Second = 3, Third = 6 });
            cells.Add(5, new RowCellNumbers() { First = 1, Second = 4, Third = 7 });
            cells.Add(6, new RowCellNumbers() { First = 2, Second = 5, Third = 8 });
            cells.Add(7, new RowCellNumbers() { First = 0, Second = 4, Third = 8 });
            cells.Add(8, new RowCellNumbers() { First = 2, Second = 4, Third = 6 });
        }

        public int GetIndex()
        {
            if (values[cells[CheckResult].First] == 0) return cells[CheckResult].First;
            if (values[cells[CheckResult].Second] == 0) return cells[CheckResult].Second;

            return cells[CheckResult].Third;
        }
        
        public void DrawField()
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            WriteHorizontal(0);
            WriteHorizontal(2);
            WriteHorizontal(4);
            WriteHorizontal(6);

            WriteVertical(0);
            WriteVertical(2);
            WriteVertical(4);
            WriteVertical(6);

            if (values[0] == 0)
                WriteAt("1", 1, 1, ConsoleColor.DarkGray);
            if (values[0] == 1)
                WriteAt("X", 1, 1, ConsoleColor.Green);
            if (values[0] == 10)
                WriteAt("0", 1, 1, ConsoleColor.Red);
                
            if (values[1] == 0)
                WriteAt("2", 3, 1, ConsoleColor.DarkGray);
            if (values[1] == 1)
                WriteAt("X", 3, 1, ConsoleColor.Green);
            if (values[1] == 10)
                WriteAt("0", 3, 1, ConsoleColor.Red);

            if (values[2] == 0)
                WriteAt("3", 5, 1, ConsoleColor.DarkGray);
            if (values[2] == 1)
                WriteAt("X", 5, 1, ConsoleColor.Green);
            if (values[2] == 10)
                WriteAt("0", 5, 1, ConsoleColor.Red);

            if (values[3] == 0)               
                WriteAt("4", 1, 3, ConsoleColor.DarkGray);
            if (values[3] == 1)
                WriteAt("X", 1, 3, ConsoleColor.Green);
            if (values[3] == 10)
                WriteAt("0", 1, 3, ConsoleColor.Red);

            if (values[4] == 0)
                WriteAt("5", 3, 3, ConsoleColor.DarkGray);
            if (values[4] == 1)
                WriteAt("X", 3, 3, ConsoleColor.Green);
            if (values[4] == 10)
                WriteAt("0", 3, 3, ConsoleColor.Red);

            if (values[5] == 0)
                WriteAt("6", 5, 3, ConsoleColor.DarkGray);
            if (values[5] == 1)
                WriteAt("X", 5, 3, ConsoleColor.Green);
            if (values[5] == 10)
                WriteAt("0", 5, 3, ConsoleColor.Red);

            if (values[6] == 0)
                WriteAt("7", 1, 5, ConsoleColor.DarkGray);
            if (values[6] == 1)
                WriteAt("X", 1, 5, ConsoleColor.Green);
            if (values[6] == 10)
                WriteAt("0", 1, 5, ConsoleColor.Red);

            if (values[7] == 0)
                WriteAt("8", 3, 5, ConsoleColor.DarkGray);
            if (values[7] == 1)
                WriteAt("X", 3, 5, ConsoleColor.Green);
            if (values[7] == 10)
                WriteAt("0", 3, 5, ConsoleColor.Red);

            if (values[8] == 0)
                WriteAt("9", 5, 5, ConsoleColor.DarkGray);
            if (values[8] == 1)
                WriteAt("X", 5, 5, ConsoleColor.Green);
            if (values[8] == 10)
                WriteAt("0", 5, 5, ConsoleColor.Red);

            Console.ForegroundColor = ConsoleColor.White;    
        }

        public bool IsUserWin()
        {
            return CheckEverething(3) != -1;
        }

        public bool IsComputerWin()
        {
            return CheckEverething(30) != -1;
        }
        
        private int CheckEverething(int value)
        {
            for (int i = 1; i < cells.Count + 1; i++)
                if (values[cells[i].First] + values[cells[i].Second] + values[cells[i].Third] == value) return i;

            return -1;
        }
        
        public bool IsStandOff()
        {
            int i = 1;
            foreach (var item in values)
                i *= item;

            if (i == 0) return false;

            return true;
        }

        public bool IsWinStepExist()
        {
            CheckResult = CheckEverething(20);
            return CheckResult != -1;
        }

        public bool IsDefenceStepNeed()
        {
            CheckResult = CheckEverething(2);
            return CheckResult != -1;
        }

        public bool IsAnyCornerFree()
        {
            return values[0] == 0 || values[2] == 0 || values[6] == 0 || values[8] == 0;
        }

        public bool IsAttackStepExist()
        {
            //для угла 0
            if (values[0] == 0 && values[8] == 0 && ((values[2] == 10 && values[1] == 0) || (values[6] == 10 && values[3] == 0)))
            {
                CheckResult = 0;
                return true;
            }

            //для угла 2
            if (values[2] == 0 && values[6] == 0 && ((values[0] == 10 && values[1] == 0) || (values[8] == 10 && values[5] == 0)))
            {
                CheckResult = 2;
                return true;
            }

            //для угла 6
            if (values[6] == 0 && values[2] == 0 && ((values[8] == 10 && values[7] == 0) || (values[0] == 10 && values[3] == 0)))
            {
                CheckResult = 6;
                return true;
            }

            //для угла 8
            if (values[8] == 0 && values[0] == 0 && ((values[6] == 10 && values[7] == 0) || (values[2] == 10 && values[5] == 0)))
            {
                CheckResult = 8;
                return true;
            }

            //для ячейки 1
            if (values[1] == 0 && values[7] == 0 && ((values[0] == 10 && values[2] == 0) || (values[2] == 10 && values[0] == 0)))
            {
                CheckResult = 1;
                return true;
            }

            //для ячейки 3
            if (values[3] == 0 && values[5] == 0 && ((values[0] == 10 && values[6] == 0) || (values[6] == 10 && values[0] == 0)))
            {
                CheckResult = 3;
                return true;
            }

            //для ячейки 5
            if (values[5] == 0 && values[3] == 0 && ((values[2] == 10 && values[8] == 0) || (values[8] == 10 && values[2] == 0)))
            {
                CheckResult = 5;
                return true;
            }

            //для ячейки 7
            if (values[7] == 0 && values[1] == 0 && ((values[6] == 10 && values[8] == 0) || (values[8] == 10 && values[6] == 0)))
            {
                CheckResult = 7;
                return true;
            }

            return false;
        }

        public bool IsDoubleDirectionAttackThreat()
        { 
            //для угла 0
            if (values[0] == 0 && (values[0] + values[3] + values[6] == 1) && (values[0] + values[1] + values[2] == 1))
            {
                CheckResult = 0;
                return true;
            }

            //для угла 2
            if (values[2] == 0 && (values[2] + values[5] + values[8] == 1) && (values[0] + values[1] + values[2] == 1))
            {
                CheckResult = 2;
                return true;
            }

            //для угла 6
            if (values[6] == 0 && (values[6] + values[3] + values[0] == 1) && (values[6] + values[7] + values[8] == 1))
            {
                CheckResult = 6;
                return true;
            }

            //для угла 8
            if (values[8] == 0 && (values[2] + values[5] + values[8] == 1) && (values[6] + values[7] + values[8] == 1))
            {
                CheckResult = 8;
                return true;
            }
            return false;
        }
        
        protected void WriteHorizontal(int y)
        {
            WriteAt("+", 0, y);
            WriteAt("-", 1, y);
            WriteAt("+", 2, y);
            WriteAt("-", 3, y);
            WriteAt("+", 4, y);
            WriteAt("-", 5, y);
            WriteAt("+", 6, y);
        }

        protected void WriteVertical(int x)
        {
            WriteAt("|", x, 1);
            WriteAt("|", x, 3);
            WriteAt("|", x, 5);
        }

        protected void WriteAt(string s, int x, int y, ConsoleColor c = ConsoleColor.White)
        {
            try
            {
                Console.ForegroundColor = c;
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }

    class RowCellNumbers
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Third { get; set; }
    }
}
