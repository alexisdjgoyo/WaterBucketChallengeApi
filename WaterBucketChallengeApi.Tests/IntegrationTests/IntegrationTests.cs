using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Collections.Generic;
using WaterBucketChallengeApi.Models;
using WaterBucketChallengeApi.Models.Dtos;
using Xunit;
namespace WaterBucketChallengeApi.Tests.IntegrationTests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        // Se crea un cliente HTTP para hacer peticiones simuladas
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Valid_Request_Should_Return_200_OK_And_Solution()
    {
        // Arrange
        var request = new WaterBucketRequestDto { x_capacity = 3, y_capacity = 5, z_amount_wanted = 4 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/solve", request);

        // Assert
        response.EnsureSuccessStatusCode(); // Assert 200 OK

        // Deserializamos a un DTO fuertemente tipado.
        var responseContent = await response.Content.ReadFromJsonAsync<SolutionResponse>();
 
        Assert.NotNull(responseContent);
        Assert.NotNull(responseContent.solution);
        
        // Verificar el número de pasos de la solución óptima
        Assert.Equal(6, responseContent.solution.Count); // La solución óptima tiene 6 pasos
    }

    [Fact]
    public async Task Post_Invalid_Request_Due_To_Unsolvable_Should_Return_400_BadRequest()
    {
        // Arrange: Caso irresoluble por GCD (X=6, Y=9, Z=5)
        var request = new WaterBucketRequestDto { x_capacity = 6, y_capacity = 9, z_amount_wanted = 5 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/solve", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        // Deserializamos a un DTO para el mensaje de error.
        var responseContent = await response.Content.ReadFromJsonAsync<ErrorResponse>();

        // Verificar que el mensaje de error sea el esperado del controlador
        Assert.Equal("No Solution", responseContent?.solution);
    }

    [Fact]
    public async Task Post_Invalid_Request_Due_To_Negative_Input_Should_Return_400_BadRequest()
    {
        // Arrange: Caso de validación de DTO que falla
        var request = new WaterBucketRequestDto { x_capacity = -1, y_capacity = 5, z_amount_wanted = 4 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/solve", request);

        // Assert
        // El framework automáticamente devuelve 400 por la validación de [Range] en el DTO
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

// DTO para deserializar la respuesta exitosa
public class SolutionResponse
{
    public List<SolutionStep>? solution { get; set; }
}

// DTO para deserializar la respuesta de error
public class ErrorResponse
{
    public string? solution { get; set; }
}