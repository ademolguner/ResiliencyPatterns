namespace ResiliencyPatterns.SpinningWheels.Api.Configurations.Settings;

public class ExternalServiceSettings
{
    public ExternalServiceModel WalletService { get; set; }
}

public class ExternalServiceModel
{
    public string Host { get; set; }
    public string HealthCheck { get; set; }
    public int Timeout { get; set; }
}
