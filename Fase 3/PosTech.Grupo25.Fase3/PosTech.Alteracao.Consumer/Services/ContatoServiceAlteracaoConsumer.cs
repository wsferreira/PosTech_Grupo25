using PosTech.Alteracao.Consumer.Interfaces;
using PosTech.Entidades;
using PosTech.Repository;
using PosTech.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Alteracao.Consumer.Services
{
    public class ContatoServiceAlteracaoConsumer : IContatoServiceAlteracaoConsumer
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoServiceAlteracaoConsumer(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public void Alterar(Contato contato)
        {
            Contato cont = VerificaContatoExiste(contato.Id);
            cont.Nome = contato.Nome;
            cont.Email = contato.Email;
            cont.Telefone = contato.Telefone;
            cont.RegiaoId = contato.RegiaoId;

            _contatoRepository.Alterar(cont);
        }
        private Contato VerificaContatoExiste(int id)
        {
            var cont = _contatoRepository.ObterPorId(id);
            if (cont == null)
            {
                throw new DomainException("Contato não encontrado.");
            }
            return cont;
        }
    }
}
