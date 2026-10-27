using System;
using pi.Data;
using pi.Models;

namespace pi.Repository;

public class ProdutorRepository : IProdutorRepository
{
    private readonly BancoContext _bancoContext;
    public ProdutorRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }
    public ProdutorModel Add(ProdutorModel produtor)
    {
        produtor.setHash();
        _bancoContext.Produtor.Add(produtor);
        _bancoContext.SaveChanges();
        return produtor;
    }

    public ProdutorModel SearchByCNPJ(string cnpj)
    {
        return _bancoContext.Produtor.FirstOrDefault(x => x.Cnpj== cnpj);
    }
}
