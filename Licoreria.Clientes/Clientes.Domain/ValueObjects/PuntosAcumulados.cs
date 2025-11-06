using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.ValueObjects
{
    public class PuntosAcumulados
    {
        public decimal Puntos { get; private set; }

        private PuntosAcumulados() { }

        public PuntosAcumulados(decimal puntos)
        {
            if (puntos < 0)
            {
                throw new ArgumentOutOfRangeException("Los puntos no pueden ser negativos.");
            }
            Puntos = puntos;
        }

        public PuntosAcumulados Sumar(decimal valor) => new(valor + Puntos);
        public PuntosAcumulados Restablecer() => new(0);
    }
}
