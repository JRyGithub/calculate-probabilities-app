// <copyright file="WeatherForecast.cs" company="Calculate Probabilities App">
// Copyright (c) Calculate Probabilities App. All rights reserved.
// </copyright>

/// <summary>
/// Represents a weather forecast.
/// </summary>
/// <param name="Date">The date of the forecast.</param>
/// <param name="TemperatureC">The temperature in Celsius.</param>
/// <param name="Summary">A summary of the weather conditions.</param>
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);
}
