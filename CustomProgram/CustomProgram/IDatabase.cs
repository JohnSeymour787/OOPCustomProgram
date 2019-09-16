using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{
    interface IDataBase
    {
        /// <summary>
        /// Loads data from the StreamReader for the database's primary list
        /// </summary>
        /// <param name="streamReader">
        /// StreamReader used to read in the data for the database. Must be opened before passing.
        /// </param>
        void LoadData(StreamReader streamReader);


        /// <summary>
        /// Saves data from the database's primary list to the StreamWriter
        /// </summary>
        /// <param name="streamWriter">
        /// StreamWriter used to write to the textfile for the database's data. Must be opened before passing.
        /// </param>
        void SaveData(StreamWriter streamWriter);
    }
}