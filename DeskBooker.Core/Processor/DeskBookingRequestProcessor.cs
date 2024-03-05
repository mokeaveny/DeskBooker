using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository deskBookingRepository;
        private readonly IDeskRepository deskRepository;
        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
        {
            this.deskBookingRepository = deskBookingRepository;
            this.deskRepository = deskRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            DeskBookingResult deskBookingResult = Create<DeskBookingResult>(request);

            if (DesksAvailable(request.Date) == true)
            {
                Desk availableDesk = GetDesk(request.Date);
                DeskBooking deskBooking = Create<DeskBooking>(request);
                deskBooking.DeskId = availableDesk.Id;

                deskBookingRepository.Save(deskBooking);

                deskBookingResult.DeskBookingId = deskBooking.Id;
                deskBookingResult.Code = DeskBookingResultCode.Success;
            }
            else
            {
                deskBookingResult.Code = DeskBookingResultCode.NoDeskAvailable;
            }

            return deskBookingResult;
        }

        public bool DesksAvailable(DateTime date)
        {
            if (deskRepository.GetAvailableDesks(date).FirstOrDefault() is Desk)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Desk GetDesk(DateTime date)
        {
            return deskRepository.GetAvailableDesks(date).First();
        }

        private static T Create<T>(DeskBookingRequest request) where T : DeskBookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}