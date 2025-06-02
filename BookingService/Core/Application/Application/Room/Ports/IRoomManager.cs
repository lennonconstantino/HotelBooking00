using Application.Room.Request;
using Application.Room.Responses;

namespace Application.Room.Ports
{
    public interface IRoomManager
    {
        public Task<RoomResponse> CreateRoom(CreateRoomRequest request);
        public Task<RoomResponse> GetRoom(int roomId);
    }
}
