using PosTech.Cadastro.Consumer.Interfaces;
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
    public class ContatoServiceCriacaoConsumer : IContatoServiceCriacaoConsumer
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoServiceCriacaoConsumer(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public void Cadastrar(Contato contato)
        {
            _contatoRepository.Cadastrar(contato);
        }
    }
}
