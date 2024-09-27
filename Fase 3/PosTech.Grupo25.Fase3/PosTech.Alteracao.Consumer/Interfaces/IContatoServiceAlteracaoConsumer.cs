using PosTech.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Alteracao.Consumer.Interfaces
{
    public interface IContatoServiceAlteracaoConsumer
    {
        void Alterar(Contato contato);
    }
}
