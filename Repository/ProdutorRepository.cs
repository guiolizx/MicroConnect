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

    public ProdutorModel SearchById(int id)
    {
        return _bancoContext.Produtor.FirstOrDefault(x => x.idProdutor == id);
    }

    public void Edit(ProdutorModel produtor)
    {
        var existingProdutor = _bancoContext.Produtor.FirstOrDefault(x => x.idProdutor == produtor.idProdutor);
        Console.WriteLine($"Editing produtor with ID: {produtor.idProdutor}");
        if (existingProdutor != null)
        {
            existingProdutor.Nome = produtor.Nome;
            existingProdutor.Cnpj = produtor.Cnpj;
            existingProdutor.Email = produtor.Email;
            existingProdutor.Telefone = produtor.Telefone;
            existingProdutor.Endereco = produtor.Endereco;

            _bancoContext.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var produtor = _bancoContext.Produtor.FirstOrDefault(x => x.idProdutor == id);
        if (produtor != null)
        {
            _bancoContext.Produtor.Remove(produtor);
            _bancoContext.SaveChanges();
        }
    }
}
