using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filer;
using GameNS;

namespace SystemDrawingViewSokoban
{
    public class Controller
    {
        protected IView View;
        protected IFileable GameFileable = game;

        static MockFiler Mock = new MockFiler("Level 1", "########\n#.     #\n# #$ #.#\n#     $#\n#  $  .#\n#      #\n#@     #\n########");
        static Game game = new Game(Mock);
        public Controller(IView view)
        {
            View = view;
        }

        public void Go()
        {
            game.Load("h:\theFileNameDoesNotMatterAsItReturnsAString");
            game.GridLoad();
            View.CreateLevel(game.Grid);
        }
        public void KeyPressed(Direction direction)
        {
            game.Move(direction);
            View.MoveCount(game.GetMoveCount());
            View.CreateLevel(game.GetGrid());
            if (!game.ScanGrid(Parts.Goal)
                    && !game.ScanGrid(Parts.PlayerOnGoal))
            {
                View.Stop();
            }
        }

        public void Reset()
        {
            game.Restart();
            View.RebuildLevel(game.GetGrid());
        }

        public void Save(string fileName)
        {
            BuilderTemp(fileName, GameFileable);
        }

        public void BuilderTemp(string fileName, IFileable callBack)
        {
            string empty = "";
            int rows = callBack.GetRowCount();
            int columns = callBack.GetColumnCount();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    char part = (char)callBack.WhatsAt(row, column);
                    empty += part.ToString();

                }
                empty += "\r\n";
            }
            System.IO.File.WriteAllText(fileName, empty);
        }
    }
}
