using Imtudou.Core.CommonEnum;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Data
{
    public class SqlOptions
    {
        [Required(ErrorMessage = "sql connection string is required")]
        public string ConnectionString { get; set; }

        [Required(ErrorMessage = "DataBaseTypeEnum is required")]
        public DataBaseTypeEnum DataBaseType { get; set; }

        public SqlOptions()
        {
        }

        public SqlOptions(string connectionString, DataBaseTypeEnum dataBase)
        {
            ConnectionString = connectionString;
            DataBaseType = dataBase;
        }

    }
}
