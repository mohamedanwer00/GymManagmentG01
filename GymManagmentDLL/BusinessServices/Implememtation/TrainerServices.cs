namespace GymManagmentBLL.BusinessServices.Implememtation
{
    internal class TrainerServices : ITrainerServices
    {
        private readonly IUnitOfWork _unitOfWork;


        public TrainerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            if (createTrainer is null || IsEmailExist(createTrainer.Email)
                || IsPhoneExist(createTrainer.Phone))
                return false;
            var trainer = new Trainer
            {
                Name = createTrainer.Name,
                Email = createTrainer.Email,
                Phone = createTrainer.Phone,
                DateOfBirth = createTrainer.DateOfBirth,
                Gender = createTrainer.Gender,
                Address = new Address
                {
                    BuildingNumber = createTrainer.BuildingNumber,
                    Street = createTrainer.Street,
                    City = createTrainer.City,
                },
                Specialities = createTrainer.Specialities,

            };

            try
            {
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }



        public bool DeleteTrainer(int trainerId)
        {
            var trainerRepository = _unitOfWork.GetRepository<Trainer>();
            var trainer = trainerRepository.GetById(trainerId);
            if (trainer is null || HasFutureSessions(trainerId))
                return false;

            try
            {
                trainerRepository.Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers is null || !trainers.Any())
                return [];

            return trainers.Select(T => new TrainerViewModel
            {
                Id = T.Id,
                Name = T.Name,
                Email = T.Email,
                Phone = T.Phone,
                Specialities = T.Specialities.ToString(),
            });
        }
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return null;

            return new TrainerViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                Gender = trainer.Gender.ToString(),
                Address = $"{trainer.Address.BuildingNumber}-{trainer.Address.Street}-{trainer.Address.City}",

                Specialities = trainer.Specialities.ToString(),
            };
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return null;

            return new TrainerToUpdateViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialities = trainer.Specialities,
                BuildingNumber = trainer.Address.BuildingNumber,
                City = trainer.Address.City,
                Street = trainer.Address.Street,
            };
        }

        public bool UpdateTrainer(int id, TrainerToUpdateViewModel trainerToUpdate)
        {
            var trainerRepository = _unitOfWork.GetRepository<Trainer>();

            var EmailExistForAnotherOldTrainer = trainerRepository
                .GetAll(X => X.Email == trainerToUpdate.Email && X.Id != id)
                .Any();

            var PhoneExistForAnotherOldTrainer = trainerRepository
                .GetAll(X => X.Email == trainerToUpdate.Email && X.Id != id)
                .Any();

            if (EmailExistForAnotherOldTrainer || PhoneExistForAnotherOldTrainer || trainerToUpdate is null)
                return false;

            var trainer = trainerRepository.GetById(id);

            if (trainer is null) return false;
            trainer.Email = trainerToUpdate.Email;
            trainer.Phone = trainerToUpdate.Phone;
            trainer.Address.BuildingNumber = trainerToUpdate.BuildingNumber;
            trainer.Address.Street = trainerToUpdate.Street;
            trainer.Address.City = trainerToUpdate.City;
            trainer.Specialities = trainerToUpdate.Specialities;

            trainer.UpdatedAt = DateTime.Now;

            try
            {
                trainerRepository.Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #region MyRegion
        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Phone == phone).Any();
        }

        private bool HasFutureSessions(int trainerId)
        {
            return _unitOfWork.GetRepository<Session>()
                .GetAll(S => S.TrainerId == trainerId && S.StartDate > DateTime.Now)
                .Any();

        }
        #endregion

    }
}
