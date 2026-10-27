
using pi.Models;

namespace pi.Repository;

public interface IProdutorRepository
{
    ProdutorModel Add(ProdutorModel cliente);
    ProdutorModel SearchByCNPJ(string cnpj);
}