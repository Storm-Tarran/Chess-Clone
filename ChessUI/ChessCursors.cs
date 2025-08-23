using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ChessUI
{
    public static class ChessCursors
    {
        public static readonly Cursor WhiteCursor = LoadCursors("Assest/CursorW.cur");
        public static readonly Cursor BlackCursor = LoadCursors("Assest/CursorB.cur");

        private static Cursor LoadCursors(string cursorFileName)
        {
            Stream stream = Application.GetResourceStream(new Uri(cursorFileName, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
