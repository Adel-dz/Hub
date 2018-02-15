using System;

namespace easyLib
{
    public sealed class CorruptedFileException : CorruptedStreamException
    {
        public CorruptedFileException(string filePath, string message = null, Exception innerException = null) :
            base(message?? CreateMessage(filePath), innerException)
        {            
            CorruptedFile = filePath;
        }

        public string CorruptedFile { get; private set; }



        //private:
        static string CreateMessage(string filePath)
        {
            if (filePath == null)
                return null;

            string fileName;

            try
            {
                fileName = System.IO.Path.GetFileName(filePath);
            }
            catch
            {
                return null;
            }

            string msg = string.Format("Une erreur c’est produite lors d’une opération sur le fichier {0}.\n" +
                "Il est probable que le fichier en question soit corrompu.", fileName);

            return msg;
        }
    }
}
