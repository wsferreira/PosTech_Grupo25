using PosTech.Exclusao.Consumer.Interfaces;
using PosTech.Entidades;
using PosTech.Repository;
using PosTech.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Exclusao.Consumer.Services
{
    public class ContatoServiceExclusaoConsumer : IContatoServiceExclusaoConsumer
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoServiceExclusaoConsumer(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public void Deletar(int id)
        {
            _contatoRepository.Deletar(id);
        }
    }
}
