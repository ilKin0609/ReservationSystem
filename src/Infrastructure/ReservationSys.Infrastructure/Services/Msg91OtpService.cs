using Microsoft.Extensions.Options;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Features.User.Commands.Register;
using ReservationSys.Application.Shared.Settings;

namespace ReservationSys.Infrastructure.Services;

public class Msg91OtpService : IOtpService
{
    private readonly Msg91Setting _settings;
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, (string Code, DateTime Expiry, UserRegisterCommandRequest PendingUser)> _otpStore = new();

    public Msg91OtpService(IOptions<Msg91Setting> settings, HttpClient httpClient)
    {
        _settings = settings.Value;
        _httpClient = httpClient;
    }

    public async Task SendOtpAsync(UserRegisterCommandRequest model)
    {
        var code = new Random().Next(100000, 999999).ToString();
        var expiry = DateTime.UtcNow.AddMinutes(5);

        _otpStore[model.PhoneNumber] = (code, expiry, model);

        var url = $"https://api.msg91.com/api/v5/otp?authkey={_settings.AuthKey}&mobile={model.PhoneNumber}&message=Your+OTP+is+{code}&sender={_settings.SenderId}&otp={code}&country={_settings.CountryCode}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to send OTP via MSG91.");
        }
    }

    public UserRegisterCommandRequest? ValidateOtp(string phoneNumber, string otp)
    {
        if (!_otpStore.ContainsKey(phoneNumber))
            return null;

        var (storedCode, expiry, user) = _otpStore[phoneNumber];

        if (DateTime.UtcNow > expiry)
        {
            _otpStore.Remove(phoneNumber);
            return null;
        }

        if (storedCode != otp)
            return null;

        _otpStore.Remove(phoneNumber);
        return user;
    }
}
