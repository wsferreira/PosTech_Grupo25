using PosTech.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Cadastro.Consumer.Interfaces
{
    public interface IContatoServiceCriacaoConsumer
    {
        void Cadastrar(Contato contato);
    }
}
