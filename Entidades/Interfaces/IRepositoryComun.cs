using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IRepositoryComun <T>
    {
        Task<int> CrearAsync(T entity);
        Task<T> ObtenerPorIdAsync(int id);
        Task<List<T>> ObtenerTodosAsync();
        Task<T> ObtenerPorEmail(string email);
        Task<bool> ActualizarAsync(T entity);
        Task<bool> EliminarAsync(int id);
    }
}
