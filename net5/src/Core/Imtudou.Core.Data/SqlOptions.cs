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

        [Required(ErrorMessage = "BaseTypeEnum is required")]
        public DataBaseTypeEnum BaseTypeEnum { get; set; }

        public SqlOptions()
        {
        }

        public SqlOptions(string connectionString, DataBaseTypeEnum dataBaseType)
        {
            ConnectionString = connectionString;
            BaseTypeEnum = dataBaseType;
        }

    }
}
