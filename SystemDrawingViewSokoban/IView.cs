using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filer;

namespace SystemDrawingViewSokoban
{
    public interface IView
    {
        void Start();
        void Stop();
        void Show<T>(T input);
        void RebuildLevel(List<List<Parts>> grid);
        void CreateLevel(List<List<Parts>> grid);
        void MoveCount(int move);
    }
}
