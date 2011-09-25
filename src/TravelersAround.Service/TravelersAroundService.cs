using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using System.ServiceModel.Activation;
using System.ServiceModel;
using TravelersAround.DataContracts;
using TravelersAround.Model;
using TravelersAround.Model.Services;
using TravelersAround.Model.Entities;
using TravelersAround.Service.Mappers;

using log4net;
using System.IO;

namespace TravelersAround.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TravelersAroundService : ServiceBase, ITravelersAroundService
    {
        private const int RADIUS = 20;

        private IRepository _repository;
        private IMembership _membership;
        private IGeoCoder _geoCoder;
        private ILocationDeterminator _locationDeterminator;

        private readonly Guid _currentTravelerId = APIKeyService.GetAssociatedID();

        public TravelersAroundService(IRepository repository, 
                                    IMembership membership,
                                    IGeoCoder geoCoder,
                                    ILocationDeterminator locationDeterminator,
                                    ILog log)
            : base(log)
        {
            _repository = repository;
            _membership = membership;
            _locationDeterminator = locationDeterminator;
            _geoCoder = geoCoder;
        }

        public AddFriendResponse AddFriend(string friendID)
        {
            AddFriendResponse response = new AddFriendResponse();
            RelationshipService relationshipSvc = new RelationshipService(_repository);
            try
            {
                relationshipSvc.ModifyFriendsList(RelationshipService.Operation.AddFriend, _currentTravelerId, new Guid(friendID));
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public RemoveFriendResponse RemoveFriend(string friendID)
        {
            RemoveFriendResponse response = new RemoveFriendResponse();
            RelationshipService relationshipSvc = new RelationshipService(_repository);
            try
            {
                relationshipSvc.ModifyFriendsList(RelationshipService.Operation.RemoveFriend, _currentTravelerId, new Guid(friendID));
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public ListFriendsResponse ListFriends(int index, int count)
        {
            ListFriendsResponse response = new ListFriendsResponse();
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);
                if (traveler.HasFriends())
                {
                    response.FriendsList = traveler.Relationships.Skip(index).Take(count).ConvertToTravelerViewList();
                    response.MarkSuccess();
                }
                else
                {
                    response.ErrorMessage = String.Format(R.String.ErrorMessages.FriendListEmpty, traveler.TravelerID);
                }
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public ListMessagesResponse ListMessages(string folderName, int index, int count)
        {
            ListMessagesResponse response = new ListMessagesResponse();

            MessageService msgSvc = new MessageService(_repository);
            FolderType folderType;
            if (!Enum.TryParse<FolderType>(folderName, out folderType))
            {
                response.ErrorMessage = String.Format(R.String.ErrorMessages.InvalidFolderName, folderName);
            }
            else
            {
                try
                {
                    response.MessagesList = msgSvc.ListMessages(_currentTravelerId, folderType, index, count).ConvertToMessageViewList();
                    response.MarkSuccess();
                }
                catch (Exception ex)
                {
                    ReportError(ex, response);
                }
            }
            return response;
        }

        public SendMessageResponse SendMessage(SendMessageRequest sendMsgReq)
        {
            SendMessageResponse response = new SendMessageResponse();
            MessageService msgSvc = new MessageService(_repository);
            try
            {
                msgSvc.SendMessage(sendMsgReq.Subject, sendMsgReq.Body, _currentTravelerId, new Guid[] { new Guid(sendMsgReq.RecipientID) });
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;

        }

        public DeleteMessageResponse DeleteMessage(string messageID)
        {
            DeleteMessageResponse response = new DeleteMessageResponse();
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);
                TravelerMessage message = traveler.Messages.FirstOrDefault(m => m.MessageID == new Guid(messageID));
                if (message != null)
                {
                    message.FolderID = (int)FolderType.Trash;
                    _repository.Save<TravelerMessage>(message);
                    _repository.Commit();
                    response.MarkSuccess();
                }
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public ReadMessageResponse ReadMessage(string messageID)
        {
            ReadMessageResponse response = new ReadMessageResponse();
            MessageService msgSvc = new MessageService(_repository);
            try
            {
                TravelerMessage message = msgSvc.ReadMessage(new Guid(messageID), _currentTravelerId);
                response.Message = message.ConvertToMessageView();
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public UpdateProfileResponse UpdateProfile(UpdateProfileRequest updateProfileReq)
        {
            UpdateProfileResponse response = new UpdateProfileResponse();
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);
                traveler.Firstname = updateProfileReq.Firstname ?? traveler.Firstname;
                traveler.Lastname = updateProfileReq.Lastname ?? traveler.Lastname;
                traveler.StatusMessage = updateProfileReq.StatusMessage ?? traveler.StatusMessage;
                traveler.Gender = updateProfileReq.Gender ?? traveler.Gender;
                traveler.IsAvailable = updateProfileReq.IsAvailable ?? traveler.IsAvailable;
                _repository.Save<Traveler>(traveler);
                _repository.Commit();
                response.Profile = traveler.ConvertToTravelerView();
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public DisplayProfileResponse DisplayProfile()
        {
            DisplayProfileResponse response = new DisplayProfileResponse();
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);
                response.Profile = traveler.ConvertToTravelerView();
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public SearchResponse Search(bool includeOfflineTravelers, int index, int count)
        {
            SearchResponse response = new SearchResponse();
            LocationService locSvc = new LocationService(_locationDeterminator, _repository, _geoCoder);
            APIKeyService apiKeySvc = new APIKeyService();

            try
            {
                IEnumerable<Traveler> travelersAround = locSvc.GetListOfTravelersWithin(RADIUS, index, count, _currentTravelerId);
                //Loading currently online travelers from the cache
                IEnumerable<Guid> currentlyActiveTravelers = apiKeySvc.GetCurrentlyActiveTravelers();

                if (!includeOfflineTravelers)
                {
                    //Gets only travelers around who are online at this moment
                    travelersAround = travelersAround.Where(t => currentlyActiveTravelers.Contains(t.TravelerID));
                }

                //Marks which travelers are online
                travelersAround
                    .ToList()
                    .ForEach(traveler => traveler.IsOnline = currentlyActiveTravelers.Contains(traveler.TravelerID));
                
                //Converts to the suitable data contract for serialization                
                response.Travelers = travelersAround.ConvertToTravelerViewList();
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }


        public ProfilePictureUploadResponse UploadProfilePicture(Stream pictureStream)
        {
            ProfilePictureUploadResponse response = new ProfilePictureUploadResponse { ErrorMessage = R.String.ErrorMessages.InvalidImageFormatException };
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);

                ProfileService profileSvc = new ProfileService(_repository);
                MemoryStream resizedPicture = profileSvc.ResizeProfilePicture(pictureStream);
                traveler.ProfilePicture = resizedPicture.ToArray();
                
                _repository.Save<Traveler>(traveler);
                _repository.Commit();
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public Stream GetProfilePicture(string travelerID)
        {
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);
                MemoryStream bufferStream = new MemoryStream(traveler.ProfilePicture);
                return bufferStream;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return null;

        }

        public TickerResponse Tick()
        {
            TickerResponse response = new TickerResponse();
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);
                response.NewMessagesCount = traveler.Messages.Count;
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
         }
    }
}
