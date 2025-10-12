using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        //GetAllTrainers
        IEnumerable<Trainer> GetAllTrainers();
        //GetTrainerById
        Trainer? GetTrainerById(int id);
        //AddTrainer
        int Add(Trainer trainer);
        //UpdateTrainer
        int Update(Trainer trainer);
        //DeleteTrainer
        int Remove(int id);


    }
}
