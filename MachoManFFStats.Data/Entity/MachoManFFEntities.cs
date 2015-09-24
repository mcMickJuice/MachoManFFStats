using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachoManFFStats.Data.Entity
{
    public partial class MachoManFFEntities : DbContext
    {
        private const string _connStringFormat =
            @"metadata=res://*/Entity.MachoManFFDataModel.csdl|res://*/Entity.MachoManFFDataModel.ssdl|res://*/Entity.MachoManFFDataModel.msl;provider=System.Data.SqlClient;provider connection string='{0}'";

        public MachoManFFEntities(string connString)
            :base(string.Format(_connStringFormat,connString))
        {
            
        }
    }
}
