using DGD.HubCore.DB;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.FilesGen
{

    /*
     *  il s'agit d'une petite table dont la cardinalité convergera dans le tmeps
     *  vers un nbre const. la generation des fichiers sert a comparer entre ceux-ci 
     *  lors des maj. A chaque maj la generation des fichiers concernes est incrementee. =>
     *  pas de suppression, pas d'addition apres convergeance.
     */

    sealed class FileGeneration: FileGenerationRow
    {
        public FileGeneration(uint idTable , uint gen) : 
            base(idTable , gen)
        {
            Assert(idTable > 0);
            Assert(gen > 0);
        }
        
        public FileGeneration():
            base(0, 0)
        { }


        public static int Size => sizeof(uint) << 1;

        public override string ToString() =>
            $"(ID Table: {ID}, Table:{AppContext.TableManager.GetTable(ID).Name},  Géneration{Generation})";


        //protected:
        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                AppContext.TableManager.GetTable(ID).Name,
                Generation.ToString()
            };
        }
    }
}
