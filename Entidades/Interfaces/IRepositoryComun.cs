using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IRepositoryComun <T>
    {
        Task<T> CrearAsync(T entity);
        Task<T> ObtenerPorIdAsync(int id);
        Task<List<T>> ObtenerTodosAsync();
        Task<bool> ActualizarAsync(T entity);
        Task<bool> EliminarAsync(int id);
    }
}
