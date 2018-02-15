namespace DGD.HubGovernor.Opts
{
    sealed class WindowPlacement
    {
        public WindowPlacement(int x, int y, int w, int h)
        {
            Left = x;
            Top = y;
            Width = w;
            Height = h;
        }

        public WindowPlacement(System.Drawing.Rectangle rc):
            this(rc.X, rc.Y, rc.Width, rc.Height)
        { }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }        

        public static implicit operator System.Drawing.Rectangle (WindowPlacement wp) => 
            new System.Drawing.Rectangle (wp.Left, wp.Top, wp.Width, wp.Height);
    }
}
