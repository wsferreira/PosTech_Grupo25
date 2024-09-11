using PosTech.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Cadastro.Consumer.Services
{
    public interface IContatoService
    {
        void Cadastrar(Contato contato);
    }
}
