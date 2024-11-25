using PosTech.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Exclusao.Consumer.Interfaces
{
    public interface IContatoServiceExclusaoConsumer
    {
        void Deletar(int id);
    }
}
