using GymManagmentBLL.BusinessServices.View_Models.TrainerVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.Interfaces
{
    public interface ITrainerServices
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);

        TrainerViewModel? GetTrainerDetails(int trainerId);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        bool UpdateTrainer(int id, TrainerToUpdateViewModel trainerToUpdate);
        bool DeleteTrainer(int trainerId);

    }
}
