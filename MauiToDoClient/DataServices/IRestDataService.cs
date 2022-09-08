using GoogleGson;
using MauiToDoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiToDoClient.DataServices
{
    public interface IRestDataService
    {
        Task<List<ToDo>> GetAllToDosAsync();
        Task AddToDoAsync(ToDo toDo);
        Task UpdateToDoAsync(ToDo toDo);
        Task DeleteToDoAsync(int id);
    }
}
