using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clientes.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clientes.Domain.Aggregates
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string NombreCompleto { get; private set; }
        public string Estado { get; private set; }
        public PuntosAcumulados PuntosAcumulados { get; private set; }

        private readonly List<Premio> _premios = new();
        public IReadOnlyCollection<Premio> Premios => _premios.AsReadOnly();

        public Cliente(string nombre)
        {
            Id = Guid.NewGuid();
            NombreCompleto = nombre;
            PuntosAcumulados = new PuntosAcumulados(0);
            Estado = "ACTIVO";
        }

        public void AgregarPuntos(decimal puntos)
        {
            PuntosAcumulados = PuntosAcumulados.Sumar(puntos);
        }

        public void ResetearPuntos()
        {
            PuntosAcumulados = new PuntosAcumulados(0);
        }

        public void EntregarPremio(string descripcion, Guid productoId)
        {
            ResetearPuntos();
            var premio = new Premio(Id, descripcion, productoId);
            _premios.Add(premio);
        }
    }
}
