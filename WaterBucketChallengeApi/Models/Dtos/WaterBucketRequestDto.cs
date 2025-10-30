using System;
using System.ComponentModel.DataAnnotations;
namespace WaterBucketChallengeApi.Models.Dtos;

public class WaterBucketRequestDto
{
    // X, Y, Z deben ser enteros positivos. Usamos [Range] para asegurar el valor mínimo de 1.

    [Required(ErrorMessage = "x_capacity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "x_capacity must be a positive integer.")]
    public int x_capacity { get; set; }

    [Required(ErrorMessage = "y_capacity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "y_capacity must be a positive integer.")]
    public int y_capacity { get; set; }

    [Required(ErrorMessage = "z_amount_wanted is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "z_amount_wanted must be a positive integer.")]
    public int z_amount_wanted { get; set; }
}
