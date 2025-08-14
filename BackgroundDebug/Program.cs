using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSMxCore.Background;
using TSMxCore.Background.Reports;
using TSMxCore.Persistence;
using TSMxCore.Repositorys;
using TSMxCore.Service;
using TSMxCore.Tools;

namespace BackgroundDebug
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var emp = 13;
            var user = "gmedina";
            var connection = "Data Source=LTPF4GSQ4A\\MSSQLSERVER2019;Initial Catalog=TSMX_DEMO;Integrated Security=SSPI;Persist Security Info=True;";
            var tsmxContext = new TSMxContext(emp, user, connection);
            var unitOfWork = new UnitOfWork(tsmxContext);
            RepositoryCollection repos = new RepositoryCollection();
            RepositoryService.Initialize(repos, tsmxContext);

            var query = new CheckBoxParametrosQuery {facturaId = 6701929};
            string filename = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");
            JSONSerializer.SerializerJSONToFile(filename, query);

            VanguardiaBackground vanguardiaBackground = new VanguardiaBackground(tsmxContext,unitOfWork, repos, 5);
            vanguardiaBackground.Run(new string[] { "VANGUARDIA_TXT", emp.ToString(),user,  filename});
        }

    }
}
