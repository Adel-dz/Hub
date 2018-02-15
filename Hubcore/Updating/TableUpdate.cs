using DGD.HubCore.Updating;
using System.Collections.Generic;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.Updating
{
    public sealed class TableUpdate
    {
        public TableUpdate(uint tableID, IEnumerable<IUpdateAction> actions , int szDatum , uint preGen, uint postGen)
        {
            Assert(actions != null);
            
            Actions = actions;
            PreGeneration = preGen;
            PostGeneration = postGen;
            TableID = tableID;
            DatumMaxSize = szDatum;
        }

        public TableUpdate(uint tableID, IEnumerable<IUpdateAction> actions , int szDatum, uint preGen):
            this(tableID, actions,szDatum, preGen,preGen + 1)
        { }


        public uint TableID { get; }
        public uint PreGeneration { get; }
        public uint PostGeneration { get; }
        public int DatumMaxSize { get; }

        public IEnumerable<IUpdateAction> Actions { get; }
    }
}
