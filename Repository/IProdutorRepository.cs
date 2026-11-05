
using pi.Models;

namespace pi.Repository;

public interface IProdutorRepository
{
    ProdutorModel Add(ProdutorModel cliente);
    ProdutorModel SearchByCNPJ(string cnpj);
    ProdutorModel SearchById(int id);
    void Edit(ProdutorModel produtor);
    void Delete(int id);
}