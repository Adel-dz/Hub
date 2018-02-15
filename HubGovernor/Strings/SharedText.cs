using DGD.HubCore.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyLib;
using DGD.HubGovernor.DB;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Strings
{


    class SharedText: SharedTextRow, ITaggedRow
    {
        public SharedText(uint id, string txt):
            base(id, txt)
        {
            Assert(id > 0);
            Assert(!string.IsNullOrWhiteSpace(txt));
        }

        public SharedText()
        { }



        public bool Disabled { get; set; }

        public override string ToString() => $"(ID:{ID}, Text:{Text})";



        //protected:
        protected override void DoRead(IReader reader)
        {
            base.DoRead(reader);
            Disabled = reader.ReadBoolean();
        }

        protected override void DoWrite(IWriter writer)
        {
            base.DoWrite(writer);
            writer.Write(Disabled);
        }
    }
}
