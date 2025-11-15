using Microsoft.Extensions.Options;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Features.User.Commands.Register;
using ReservationSys.Application.Shared.Settings;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ReservationSys.Infrastructure.Services;

public class InfobipOtpService : IOtpService
{
    private readonly InfobipSetting _settings;
    private readonly HttpClient _httpClient;

    private static readonly Dictionary<string, (string Code, DateTime Expiry, UserRegisterCommandRequest PendingUser)>
        _otpStore = new();

    public InfobipOtpService(IOptions<InfobipSetting> settings, HttpClient httpClient)
    {
        _settings = settings.Value;
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", _settings.ApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public async Task SendOtpAsync(UserRegisterCommandRequest model)
    {
        model.PhoneNumber = NormalizePhone(model.PhoneNumber);

        var otp = new Random().Next(100000, 999999).ToString();
        var expiry = DateTime.UtcNow.AddMinutes(5);

        _otpStore[model.PhoneNumber] = (otp, expiry, model);

        var requestBody = new
        {
            messages = new[]
            {
                new
                {
                    from = "InfoSMS",
                    destinations = new[]
                    {
                        new { to = model.PhoneNumber }
                    },
                    text = $"Your OTP code is {otp}"
                }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync("/sms/2/text/advanced", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception("Infobip SMS sending failed: " + error);
        }

        Console.WriteLine($"OTP Sent to: {model.PhoneNumber}, Code: {otp}");
    }
    public UserRegisterCommandRequest? ValidateOtp(string phoneNumber, string otp)
    {
        phoneNumber = NormalizePhone(phoneNumber);

        Console.WriteLine("---- VALIDATE OTP ----");
        Console.WriteLine("Input Phone: " + phoneNumber);
        Console.WriteLine("Stored Keys: " + string.Join(", ", _otpStore.Keys));

        if (!_otpStore.ContainsKey(phoneNumber))
        {
            Console.WriteLine("Phone not found in OTP store!");
            return null;
        }

        var (storedCode, expiry, user) = _otpStore[phoneNumber];

        Console.WriteLine($"Stored Code: {storedCode}, Input Code: {otp}");

        if (DateTime.UtcNow > expiry)
        {
            Console.WriteLine("OTP Expired");
            _otpStore.Remove(phoneNumber);
            return null;
        }

        if (storedCode != otp)
        {
            Console.WriteLine("OTP Mismatch!");
            return null;
        }

        Console.WriteLine("OTP VALID!");
        _otpStore.Remove(phoneNumber);
        return user;
    }
    private string NormalizePhone(string phone)
    {
        phone = phone.Trim().Replace(" ", "");

        if (!phone.StartsWith("+994"))
            phone = "+994" + phone.TrimStart('0');

        return phone;
    }
}
