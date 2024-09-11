using PosTech.Entidades;
using PosTech.Repository;
using PosTech.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Cadastro.Consumer.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public void Cadastrar(Contato contato)
        {
            _contatoRepository.Cadastrar(contato);
        }
    }
}
