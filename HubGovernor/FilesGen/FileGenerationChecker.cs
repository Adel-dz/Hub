using DGD.HubGovernor.DB;
using DGD.HubGovernor.Log;
using easyLib.DB;

namespace DGD.HubGovernor.FilesGen
{
    sealed class FileGenerationChecker: ITableChecker
    {
        public uint TableID => AppContext.TableManager.TablesGeneration.ID;


        public bool Check()
        {
            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Countries.Name}");
            logger.PutLine("Les élemnets suivants ont un numerà de géneration invalide:");

            using (IDatumProvider dp = AppContext.TableManager.TablesGeneration.DataProvider)
            {
                dp.Connect();

                bool anyErr = false;

                if (dp.Count > 0)
                    foreach (FileGeneration d in dp.Enumerate())
                        if (d.Generation == 0)
                        {
                            logger.PutLine(d.ToString());
                            anyErr = true;
                        }

                if (anyErr)
                    logger.Flush();

                return !anyErr;
            }
        }


        public void Dispose()
        { }
    }
}
