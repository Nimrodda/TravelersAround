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
                    response.FriendsList = traveler.Relationships.ConvertToTravelerViewList();
                    response.MarkSuccess();
                }
                else
                {
                    response.ErrorMessage = String.Format(R.ErrorMessages.FriendListEmpty, traveler.TravelerID);
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
                response.ErrorMessage = String.Format(R.ErrorMessages.InvalidFolderName, folderName);
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
                msgSvc.SendMessage(sendMsgReq.Subject, sendMsgReq.Body, _currentTravelerId, new Guid[] { sendMsgReq.RecipientID });
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
                traveler.Firstname = updateProfileReq.Firstname;
                traveler.Lastname = updateProfileReq.Lastname;
                traveler.StatusMessage = updateProfileReq.StatusMessage;
                traveler.Gender = updateProfileReq.Gender;
                traveler.IsAvailable = updateProfileReq.IsAvailable;
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

        public SearchResponse Search(bool availabilityMark, int index, int count)
        {
            SearchResponse response = new SearchResponse();
            LocationService locSvc = new LocationService(_locationDeterminator, _repository, _geoCoder);
            try
            {
                response.Travelers = locSvc.GetListOfTravelersWithin(RADIUS, index, count, _currentTravelerId).ConvertToTravelerViewList();
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
            ProfilePictureUploadResponse response = new ProfilePictureUploadResponse();
            try
            {
                Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == _currentTravelerId);

                using (MemoryStream bufferStream = new MemoryStream())
                {
                    pictureStream.CopyTo(bufferStream);
                    traveler.ProfilePicture = bufferStream.ToArray();
                }
                _repository.Save<Traveler>(traveler);
                _repository.Commit();
                response.UploadedBytes = traveler.ProfilePicture.Length;
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
                //log
            }
            return null;

        }
    }
}
