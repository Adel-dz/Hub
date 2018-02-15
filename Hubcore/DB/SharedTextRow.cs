using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface ISharedTextRow : IDataRow
    {
        string Text { get; }
    }



    public class SharedTextRow: DataRow, ISharedTextRow
    {
        public SharedTextRow(uint id, string txt):
            base(id)
        {
            Text = txt ?? "";
        }

        public SharedTextRow()
        {
            Text = "";
        }


        public string Text { get; private set; }
        


        //protected:
        protected override void DoRead(IReader reader)
        {
            string txt = reader.ReadString();

            if (string.IsNullOrWhiteSpace(txt))
                throw new CorruptedStreamException();

            Text = txt;
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(!string.IsNullOrWhiteSpace(Text));

            writer.Write(Text);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Text
            };
        }
    }
}
