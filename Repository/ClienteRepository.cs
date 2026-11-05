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

    public ClienteModel SearchById(int id)
    {
        return _bancoContext.Cliente.FirstOrDefault(x => x.IdCliente == id);
    }

    public void Edit(ClienteModel cliente)
    {
        _bancoContext.Cliente.Update(cliente);
        _bancoContext.SaveChanges();
    }
    public void Delete(int id)
    {
        var cliente = _bancoContext.Cliente.FirstOrDefault(x => x.IdCliente == id);
        if (cliente != null)
        {
            _bancoContext.Cliente.Remove(cliente);
            _bancoContext.SaveChanges();
        }
    }
}
