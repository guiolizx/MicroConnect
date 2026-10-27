
using pi.Models;

namespace pi.Repository;

public interface IClienteRepository
{
    ClienteModel Add(ClienteModel cliente);
    ClienteModel SearchByCPF(string cpf);
}