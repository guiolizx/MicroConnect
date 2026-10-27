using System;
using pi.Data;
using pi.Models;

namespace pi.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly BancoContext _bancoContext;
    public ClienteRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }
    public ClienteModel Add(ClienteModel cliente)
    {
        cliente.setHash();
        _bancoContext.Cliente.Add(cliente);
        _bancoContext.SaveChanges();
        return cliente;
    }

    public ClienteModel SearchByCPF(string cpf)
    {
        return _bancoContext.Cliente.FirstOrDefault(x => x.Cpf == cpf);
    }
}
