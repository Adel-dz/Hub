using DGD.HubCore.DB;
using DGD.HubCore.Updating;
using easyLib;
using System;
using System.Collections.Generic;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Updating
{
    interface IUpdateIncrement: IDataRow
    {
        uint PreDataGeneration { get; }
        DateTime CreationTime { get; }
        bool IsDeployed { get; }
        DateTime DeployTime { get; }        
    }



    sealed class UpdateIncrement: DataRow, IUpdateIncrement
    {
        DateTime? m_tmDeploy;


        public UpdateIncrement(uint id , uint dataGen , DateTime tmCreation) :
            base(id)
        {
            PreDataGeneration = dataGen;
            CreationTime = tmCreation;
        }

        public UpdateIncrement(uint id , uint dataGen) :
            this(id , dataGen , DateTime.Now)
        { }

        public UpdateIncrement()
        { }

        public IEnumerable<TableUpdate> Updates
        {
            get
            {
                Assert(ID > 0);
                string filePath = System.IO.Path.Combine(AppPaths.DeployCacheFolder , ID.ToString("X"));
                return UpdateEngin.LoadTablesUpdate(filePath, AppContext.DatumFactory);
            }
        }

        public DateTime CreationTime { get; private set; }
        public uint PreDataGeneration { get; private set; }
        public bool IsDeployed => m_tmDeploy != null;

        public DateTime DeployTime
        {
            get
            {
                Assert(IsDeployed);

                return m_tmDeploy.Value;
            }

            set
            {
                Assert(IsDeployed == false);

                m_tmDeploy = new DateTime?(value);
            }
        }

        public static int Size => sizeof(uint) + sizeof(uint) + sizeof(long) + sizeof(long);


        public override string ToString()
        {
            string strDeploy = IsDeployed ? DeployTime.ToString() : "Jamais";

            return $"(ID:{ID}, Ver. données:{PreDataGeneration}, Crée le:{CreationTime}, " + $"Publier le:{strDeploy})";
        }



        //protected:
        protected override void DoRead(IReader reader)
        {
            PreDataGeneration = reader.ReadUInt();
            CreationTime = reader.ReadTime();

            long ticks = reader.ReadLong();

            if (ticks != 0)
                m_tmDeploy = new DateTime(ticks);
            else if (m_tmDeploy != null)
                m_tmDeploy = null;
        }

        protected override void DoWrite(IWriter writer)
        {
            writer.Write(PreDataGeneration);
            writer.Write(CreationTime);

            writer.Write(IsDeployed ? m_tmDeploy.Value.Ticks : 0L);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                PreDataGeneration.ToString(),
                CreationTime.ToString(),
                IsDeployed? DeployTime.ToString() : ""
            };
        }

    }
}
