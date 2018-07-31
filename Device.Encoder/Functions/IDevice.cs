using Device.Encoder.Data;

namespace Device.Encoder.Functions
{
    public interface IDevice
    {
        string _Exception { get; set; }

        void AddNewUser();
        bool CloseDoor();
        bool ControlUser(string EnrollNumber, string BackupNumber, bool Enable);
        bool DeleteUser(int EnrollNumber);
        bool EnrollUser(uint EnrollmentNo, UserLevel Level, uint CardId, string UserName, uint Password, bool enabled);
        UserInfo GetUserByCard(int num);
        bool OpenDoor();
        bool ResetAllUsers();
        bool TestConnection();
    }
}