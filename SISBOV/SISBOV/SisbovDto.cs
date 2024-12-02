using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISBOV
{
    public class SisbovDto
    {
        public Guid Id { get; set; }
        public string Inicial { get; set; }
        public string Final { get; set; }
        public int? BrincosPorCaixa { get; set; }
    }
}
