using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketScope.Servicos
{
    public static class BenchmarkService
    {
        public static string GerarLog(DateTime timestamp, int conexoes, double memoriaMB)
        {
            return $"{timestamp:HH:mm:ss} | {conexoes,15} | {memoriaMB,10:F2} MB";
        }
    }
}
